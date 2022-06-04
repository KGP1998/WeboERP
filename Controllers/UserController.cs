using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.SuperAdmin)]
    public class UserController : Controller
    {
        private readonly IUserService _userService;

        public UserController()
        {
            _userService = new UserService();
        }
        // GET: User
        public ActionResult Users()
        {
            var users = _userService.GetUsers();
            return View(users);
        }

        [HttpGet]
        public ActionResult UpsertUser(int? id)
        {
            var roles = _userService.GetRoles();
          //  ViewBag.RoleId = new SelectList(roles.ToList(), "Id", "Name");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach(var item in roles)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.RoleId = list;
            if (id != 0)
            {
                var user = _userService.GetUsersById(id);
                return View(user);
            }

            return View();
        }

        [HttpPost]
        public ActionResult UpsertUser(UsersModel userModel)
        {
            var roles = _userService.GetRoles();
            //  ViewBag.RoleId = new SelectList(roles.ToList(), "Id", "Name");
            List<SelectListItem> list = new List<SelectListItem>();
            foreach (var item in roles)
            {
                list.Add(new SelectListItem() { Text = item.Name, Value = item.Id.ToString() });
            }
            ViewBag.RoleId = list;
            try
            {
                if (!ModelState.IsValid)
                {
                    return View("UpsertUser", userModel);
                }

                _userService.UpsertUser(userModel);
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return View("UpsertUser", userModel);
            }
        }

        [HttpPost]
        public ActionResult DeleteUser(int id)
        {

            try
            {
                if (id <= 0)
                    return RedirectToAction("Users");


                _userService.DeleteUser(id);
                return RedirectToAction("Users");
            }
            catch (Exception ex)
            {
                ex.Message.ToString();
                return RedirectToAction("Users");
            }
        }

    }
}