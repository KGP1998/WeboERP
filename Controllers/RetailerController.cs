using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Distributor)]
    public class RetailerController : Controller
    {
        private readonly IRetailerService _retailerService;
        private readonly IRetailerDetailsService _retailerDetailsService;
        private readonly IUserService _userService;
        public RetailerController()
        {
            _retailerService = new RetailerService();
            _retailerDetailsService = new RetailerDetailsService();
            _userService = new UserService();
        }
        // GET: Retailer
        public ActionResult Retailers()
        {
            var retailerDetails = _retailerDetailsService.GetRetailerDetails();
            return View(retailerDetails);
        }
        [HttpGet]
        public ActionResult UpsertRetailer(int? id)
        {
            if (id != null && id > 0)
            {
                var retailer = _retailerDetailsService.GetRetailerDetailsById(Convert.ToInt32(id));
                return View(retailer);
            }
            return View();
        }
        [HttpPost]
        public ActionResult UpsertRetailer(RetailerDetailsModel retailerDetailsModel)
        {
            if (!ModelState.IsValid)
            {
                View(retailerDetailsModel);
            }
            if (retailerDetailsModel.Id == 0)
            {
                if (_userService.IsUserNameExists(retailerDetailsModel.UserName, 0))
                {

                    ModelState.AddModelError("UserName", "Username already exists");
                    return View(retailerDetailsModel);
                }

            }
            var count = _retailerDetailsService.GetRetailerDetailsByRetailerId(retailerDetailsModel.RetailerId);
            if (count < 3)
            {
                _retailerService.UpsertRetailerDetails(retailerDetailsModel);
                return RedirectToAction(nameof(Retailers));
            }
            else
            {
                ModelState.AddModelError("", "Maximum 3 logins allowed for retail.");
                return View(retailerDetailsModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteRetailer(int id, bool isActive)
        {
            if (id <= 0)
                return RedirectToAction(nameof(Retailers));

            _retailerDetailsService.DeleteRetailerDetails(id, isActive);
            return RedirectToAction(nameof(Retailers));

        }
    }
}