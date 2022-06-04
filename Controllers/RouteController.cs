using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using DairyWeb.Entity;
using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;


namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Retailer)]
    public class RouteController : Controller
    {
        public ActionResult Routes()
        {
            var RetailerId = Convert.ToInt32(Session["RetailerId"]);
            RouteService routeService = new RouteService();
            var route = routeService.GetRouteModels(RetailerId);
            return View(route);
        }
        
        public ActionResult GetRoueData()
        {
            try
            {
                var loggedInUserId = Convert.ToInt32(Session["LoggedInUserId"]);
                RouteService routeService = new RouteService();

                var route = routeService.GetRoueData(loggedInUserId);
                return Json(new { Data = route, Success = true, Message = "Route fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<RouteMaster>(), Success = false, Message = ex.InnerException }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult UpsertRoute(int? id)
        {
            try
            {
                if (id != null && id > 0)
                {
                    RouteService routeService = new RouteService();
                    var routeMaster = routeService.GetRouteById(Convert.ToInt32(id));
                    return View(routeMaster);
                }
                return View();
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult UpsertRoute(RouteModel routeModel)
        {
            try
            {
                RouteService routeService = new RouteService();

                if (routeModel == null)
                {
                    throw new ArgumentNullException(nameof(routeModel));
                }

                if (!ModelState.IsValid)
                {
                    return View(routeModel);
                }

                var tuple = routeService.UpsertRoute(routeModel);
                if (tuple.Item1 == true)
                {
                    return RedirectToAction(nameof(Routes));
                }
                else
                {
                    return View(routeModel);
                }

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteRoute(int id)
        {
            RouteService routeService = new RouteService();
            try
            {
                if (id <= 0)
                    return RedirectToAction(nameof(Routes));


                routeService.DeleteRoute(id);
                return RedirectToAction(nameof(Routes));
            }
            catch (Exception ex)
            {
                return Json(new { Success = true, Message = ex.Message });
            }
        }
    }
}