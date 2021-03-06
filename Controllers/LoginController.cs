using System;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Web.Mvc;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;
using System.Web.Security;
namespace DairyWeb.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUserService _userService;

        public LoginController()
        {
            _userService = new UserService();
        }
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost, AllowAnonymous]
        public ActionResult Index(Login model)
        {
            try
            {
                if (ModelState.IsValid)
                {

                    AppContext.DairyContext db = new AppContext.DairyContext();
                    var user = db.Users.Where(x => x.UserName.Trim() == model.UserName.Trim() && x.Password == model.Password.Trim()).FirstOrDefault();

                    if (user != null)
                    {
                        if (user.IsActive == false)
                        {
                            var usersModel = _userService.GetPredecessorOfUser(user);
                            if (usersModel != null)
                            {
                                ModelState.AddModelError("", "You can't login please contact us on" + usersModel.PhNo1 + " No.");
                            }
                            return View("Index", model);
                        }
                        FormsAuthentication.SetAuthCookie(user.UserName, true);
                        Session["LoggedInUserName"] = user.UserName;

                        //Session["UserId"] = user.Id;
                        Session["LoggedInUserId"] = user.Id;
                        Session["RoleId"] = user.RoleId;


                        if (user.RoleId == 1)
                        {
                            return RedirectToAction("UpsertAdmin", "Admin");
                        }
                        else if (user.RoleId == 2)
                        {
                            var admin = db.Admin.Where(x => x.UserId == user.Id).FirstOrDefault();
                            Session["AdminId"] = admin.Id;
                            return RedirectToAction("UpsertDistributor", "Distributor");
                        }
                        else if (user.RoleId == 3)
                        {
                            var distributor = db.Distributor.Where(x => x.UserId == user.Id).FirstOrDefault();
                            Session["DistributorId"] = distributor.Id;
                            return RedirectToAction("UpsertRetailer", "Retailer");
                        }
                        else if (user.RoleId == 4)
                        {
                            var retailerDetails = db.RetailerDetails.Where(x => x.UserId == user.Id).FirstOrDefault();
                            Session["RetailerId"] = retailerDetails.RetailerId;
                            return RedirectToAction("DailyOrdersData", "Order");
                        }
                        else
                        {
                            return View("Index", model);
                        }

                    }
                    else
                    {

                        ModelState.AddModelError("", "Invalid username or password.");
                        return View("Index", model);
                    }
                }
                else
                {
                    return View("Index", model);
                }
            }
            catch (Exception ex)
            {
                //Show Error Message- ex.Message    
                ex.Message.ToString();
                return View("Index", model);
            }
        }

        public ActionResult Language(string lan)
        {
            if (lan != null)
            {
                Session["lang"] = lan;
            }

            return Redirect(Request.UrlReferrer.OriginalString);
        }


        // [NoCache]

        public ActionResult Logout()
        {
            Session["LoggedInUserName"] = null;
            //Session["UserId"] = user.Id;
            Session["LoggedInUserId"] = null;
            Session["RoleId"] = null;
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();
            return RedirectToAction("Index");
        }
    }
}