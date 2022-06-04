using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DairyWeb.Entity;
using DairyWeb.Extension;
using DairyWeb.Filters;

using DairyWeb.Services;
using DairyWeb.Models;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Retailer)]
    public class CustomerController : Controller
    {

        private readonly ICustomerService _customerService;
        private readonly IRouteService _routeService;
        private readonly IProductService _productService;
        public CustomerController()
        {
            _productService = new ProductService();
            _customerService = new CustomerService();
            _routeService = new RouteService();
        }

        public ActionResult Customers()
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var customers = _customerService.GetCustomerData(RetailerId);
                return View(customers);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<CustomerModel>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpGet]
        public ActionResult GetCustomerData()
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);

                return Json(new { Data = _customerService.GetCustomerData(RetailerId).ToList(), Success = true, Message = "Customer fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<CustomerModel>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpsertCustomer(int? id)
        {
            var RetailerId = Convert.ToInt32(Session["RetailerId"]);

            var Route = _routeService.GetRouteModels(RetailerId);
            var product = _productService.GetProduct(RetailerId);
            var productsWithTypeMilk = product.Where(x => x.ProductType == 1).ToList();
            if (id != null && id > 0)
            {
                var customer = _customerService.GetCustomerDataById(Convert.ToInt32(id));
                customer.Routes = new SelectList(Route, "Id", "RouteName", customer.RouteId);
                customer.Products = new SelectList(productsWithTypeMilk, "Id", "Name", customer.RouteId);
                return View(customer);
            }
            else
            {

                var RouteId = new SelectList(Route, "Id", "RouteName");
                var ProductId = new SelectList(productsWithTypeMilk, "Id", "Name");
                CustomerModel customer = new CustomerModel();
                customer.Routes = RouteId;
                customer.Products = ProductId;
                return View(customer);
            }


        }

        [HttpPost]
        public ActionResult UpsertCustomer(CustomerModel customerModel)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);

                var Route = _routeService.GetRouteModels(RetailerId);

                if (!ModelState.IsValid)
                {
                    return View(customerModel);
                }
                var tuple = _customerService.UpsertCustomer(customerModel);
                if (tuple.Item1 == true)
                {
                    return RedirectToAction(nameof(Customers));
                }
                else
                {
                    return View(customerModel);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteCustomer(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction(nameof(Customers));


                var tuple = _customerService.DeleteCustomer(id);
                return RedirectToAction(nameof(Customers));

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }
    }
}