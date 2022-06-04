using DairyWeb.Extension;
using DairyWeb.Filters;
using DairyWeb.Models;
using DairyWeb.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;

namespace DairyWeb.Controllers
{
    [CustomAuthorize(Role.Retailer)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ICustomerService _customerService;
        private readonly IProductService _productService;
        private readonly IRouteService _routeService;

        public OrderController()
        {

            _orderService = new OrderService();
            _customerService = new CustomerService();
            _productService = new ProductService();
            _routeService = new RouteService();
        }

        public ActionResult Orders()
        {
            try
            {
                //var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                //var orders = _orderService.GetOrderData(RetailerId).ToList();
                //return View(orders);
                return View();
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<OrderModels>(), Success = false, Message = ex.Message });
            }
        }
        [HttpPost]
        public ActionResult GetOrderData(DateTime? fromDate, DateTime? toDate)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var orders = _orderService.GetOrderData(RetailerId, fromDate, toDate).ToList();
                return Json(new { Data = orders, Success = true, Message = "Order fetched successfully." });
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<OrderModels>(), Success = false, Message = ex.Message });
            }
        }
        [HttpGet]

        public ActionResult GetOrdersData(int? id)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var products = _productService.GetProduct(RetailerId);
                var customers = _customerService.GetCustomerData(RetailerId);
                var routes = _routeService.GetRoueData(RetailerId);
                OrderModels order = new OrderModels();
                if (id != null && id > 0)
                {
                    order = _orderService.GetOrderById(Convert.ToInt32(id));
                }
                return Json(new { products = products, customers = customers, routes = routes, order = order, Success = true, Message = "Filters data fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<OrderModels>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpsertOrder(int? id)
        {
            ViewBag.OrderId = id;
            return View();
        }

        [HttpPost]
        public ActionResult UpsertOrder(OrderModels orderModels)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var tuple = _orderService.UpsertOrder(orderModels);

                    if (orderModels.CustomerId != null)
                    {
                        try
                        {
                            CustomerModel customerModel = _customerService.GetCustomerDataById(orderModels.CustomerId.Value);
                            ProductModel productModel = _productService.GetProductById(orderModels.ProductId.Value);

                            StringBuilder sb = new StringBuilder();
                            sb.AppendLine("Apex Computer ");
                            sb.AppendLine($"Date: {orderModels.OrderDate} {orderModels.ShiftName}");
                            sb.AppendLine($"{customerModel.FirstName + " " + customerModel.LastName} ");
                            sb.AppendLine($"{productModel.Name}");
                            sb.AppendLine($"QTY:{orderModels.Quantity}{productModel.MeasurementUnit} ");
                            sb.AppendLine($"Rate:{orderModels.Price.Value.ToString("0.0")} ");
                            sb.AppendLine($"Amount:{orderModels.Total.Value.ToString("0.00")} ");
                            sb.Append($"(Apex)");

                            CommonOperations.SendSms(sb.ToString(), customerModel.PhoneNo);
                        }
                        catch (Exception Ex)
                        {
                            Ex.Message.ToString();
                        }
                    }

                    return Json(new { Success = tuple.Item1, Message = tuple.Item2 });
                }
                else
                {
                    return Json(new { Success = false, Message = "Please enter required fields." });
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ChangeCustomerDetails(int customerId, int shift)
        {
            try
            {
                decimal? price = 0, quantity = 0, total = 0;
                if (customerId <= 0)
                    return Json(new { price = price, quantity = quantity, total = total, Success = true, Message = "Customer not found." });


                var keyValuePairs = _orderService.ChangeCustomerDetails(customerId, shift);
                if (keyValuePairs.Count > 0)
                {
                    return Json(new
                    {
                        price = keyValuePairs.Where(x => x.Key == "price").FirstOrDefault().Value,
                        quantity = keyValuePairs.Where(x => x.Key == "quantity").FirstOrDefault().Value,
                        total = keyValuePairs.Where(x => x.Key == "total").FirstOrDefault().Value,
                        productId = keyValuePairs.Where(x => x.Key == "productId").FirstOrDefault().Value,

                        Success = true,
                        Message = "Customer details found."
                    });
                }
                else
                {
                    return Json(new { price = price, quantity = quantity, Success = true, Message = "Customer not found." });
                }

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteOrder(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction(nameof(Orders));

                var tuple = _orderService.DeleteOrder(id);
                return RedirectToAction(nameof(Orders));
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpGet]
        public ActionResult DailyOrdersData()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetDailyOrdersData(int routeId, int shift, DateTime saleDate)
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var orders = _orderService.GetDailyOrdersData(RetailerId, routeId, shift, saleDate).ToList();
                return Json(new { Data = orders, Success = true, Message = "Order fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<OrderModels>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult GetRoutes()
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var routes = _routeService.GetRoueData(RetailerId);

                return Json(new { routes = routes, Success = true, Message = "Filters data fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<OrderModels>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}