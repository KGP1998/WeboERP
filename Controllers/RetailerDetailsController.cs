using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;
using System;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Distributor)]
    public class RetailerDetailsController : Controller
    {
        private readonly IRetailerDetailsService _retailerDetailsService;
        private readonly IRetailerService _retailerService;
        private readonly IUserService _userService;
        public RetailerDetailsController()
        {
            _retailerDetailsService = new RetailerDetailsService();
            _retailerService = new RetailerService();
            _userService = new UserService();
        }
        // GET: Retailer
        public ActionResult RetailerDetails()
        {
            var retailersDetails = _retailerDetailsService.GetRetailerDetails();
            return View(retailersDetails);
        }
        [HttpGet]
        public ActionResult UpsertRetailerDetails(int? id)
        {
            var retailers = _retailerService.GetRetailers();
            if (id != null && id > 0)
            {
                var retailerDetail = _retailerDetailsService.GetRetailerDetailsById(Convert.ToInt32(id));

                retailerDetail.Retailers = new SelectList(retailers, "Id", "Name", retailerDetail.RetailerId);
                return View(retailerDetail);
            }
            else
            {
                RetailerDetailsModel retailerDetailsModel = new RetailerDetailsModel();
                retailerDetailsModel.Retailers = new SelectList(retailers, "Id", "Name");
                return View(retailerDetailsModel);
            }

        }
        [HttpPost]
        public ActionResult UpsertRetailerDetails(RetailerDetailsModel retailerDetailsModel)
        {
            var retailers = _retailerService.GetRetailers();
            if (!ModelState.IsValid)
            {
                retailerDetailsModel.Retailers = new SelectList(retailers, "Id", "Name");
                 View(retailerDetailsModel);
            }
            if (retailerDetailsModel.Id == 0)
            {
                if (_userService.IsUserNameExists(retailerDetailsModel.UserName, 0))
                {
                    retailerDetailsModel.Retailers = new SelectList(retailers, "Id", "Name");
                    ModelState.AddModelError("UserName", "Username already exists");
                    return View(retailerDetailsModel);
                }

            }

            var count = _retailerDetailsService.GetRetailerDetailsByRetailerId(retailerDetailsModel.RetailerId);
            if (count < 3)
            {
                _retailerDetailsService.UpsertRetailerDetails(retailerDetailsModel);
                return RedirectToAction(nameof(RetailerDetails));
            }
            else
            {
                retailerDetailsModel.Retailers = new SelectList(retailers, "Id", "Name");
                ModelState.AddModelError("", "Maximum 3 logins allowed for retail.");
                return View(retailerDetailsModel);
            }

        }

        [HttpPost]
        public ActionResult DeleteRetailerDetails(int id, bool isActive)
        {
            if (id <= 0)
                return RedirectToAction(nameof(RetailerDetails));

            _retailerDetailsService.DeleteRetailerDetails(id, isActive);
            return RedirectToAction(nameof(RetailerDetails));
        }
    }
}