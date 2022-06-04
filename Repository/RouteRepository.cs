using DairyWeb.AppContext;
using DairyWeb.Entity;
using System.Collections.Generic;
using System;

using System.Linq;
using System.Web;
using Dapper;
using DairyWeb.Extension;

namespace DairyWeb.Repository
{
    public class RouteRepository /*: BaseRepository<RouteMaster>, IRouteRepository*/
    {


        public List<RouteMaster> GetRoueData(int retailerId)
        {
            
            var p = new DynamicParameters();
            p.Add("@retailerId", retailerId);
            var routeMasters = CommonOperations.Query<RouteMaster>("GetRoutes", p, commandType: System.Data.CommandType.StoredProcedure).ToList();
            return routeMasters;
            
        }
        public Tuple<bool, string> UpsertRoute(RouteMaster routeMaster)
        {
            var RetailerId = Convert.ToInt32(HttpContext.Current.Session["RetailerId"]);
            routeMaster.RetailerId = RetailerId;

            DairyContext db = new DairyContext();
            string message = string.Empty;
            bool success = false;
            if (routeMaster.Id == 0)
            {
                routeMaster.InsertedDate = DateTime.Now;
                routeMaster.InsertedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                db.RouteMaster.Add(routeMaster);
                db.SaveChanges();
                message = "Route added successfully";
                success = true;
            }
            else
            {
                var route = db.RouteMaster.Find(routeMaster.Id);
                if (route != null)
                {
                    route.Id = routeMaster.Id;
                    route.RouteName = routeMaster.RouteName;
                    route.SocietyName = routeMaster.SocietyName;
                    route.AreaName = routeMaster.AreaName;
                    route.UpdatedDate = DateTime.Now;
                    route.RetailerId = routeMaster.RetailerId;
                    route.UpdatedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                    // db.RouteMaster.Update(route);
                    db.SaveChanges();
                    message = "Route updated successfully";
                    success = true;
                }
            }
            return new Tuple<bool, string>(success, message);
        }

        public void DeleteRoute(int routeId)
        {
            var p = new DynamicParameters();
            p.Add("@Id", routeId);
            p.Add("@TableName", "route");
            var routeMasters = CommonOperations.Query<string>("DeleteById", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            
        }

        public RouteMaster GetRouteById(int id)
        {

            var p = new DynamicParameters();
            p.Add("@Id", id);
            var adminModel = CommonOperations.Query<RouteMaster>("GetRouteById", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return adminModel;
        }
    }
}
