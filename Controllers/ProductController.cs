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
    public class ProductController : Controller
    {

        private readonly IProductService productService;
        public ProductController()
        {

            productService = new ProductService();
        }
        public ActionResult Products()
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                var products = productService.GetProduct(RetailerId);
                return View(products);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<CustomerModel>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        public ActionResult GetProductData()
        {
            try
            {
                var RetailerId = Convert.ToInt32(Session["RetailerId"]);
                return Json(new { Data = productService.GetProduct(RetailerId).ToList(), Success = true, Message = "Product fetched successfully." }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                return Json(new { Data = new List<ProductMaster>(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpGet]
        public ActionResult UpsertProduct(int? id)
        {

            if (id != null && id > 0)
            {
                var products = productService.GetProductById(Convert.ToInt32(id));

                var productTypeList = new List<SelectListItem>()
                                  {
                                    new SelectListItem() { Value="1",Text="Milk" ,Selected=products.ProductType==1?true:false},
                                    new SelectListItem() { Value="2",Text="Other",Selected=products.ProductType==2?true:false}
                                  };

                products.ProductTypeList = productTypeList;
                return View(products);
            }
            else
            {
                var ProductTypeList = new List<SelectListItem>()
                                  {
                                    new SelectListItem() { Value="1",Text="Milk" },
                                    new SelectListItem() { Value="2",Text="Other"}
                                  };
                ProductModel productModel = new ProductModel();
                productModel.ProductTypeList = ProductTypeList;
                return View(productModel);
            }
        }
        [HttpPost]
        public ActionResult UpsertProduct(ProductModel productModel)
        {
            try
            {

                if (productModel == null)
                {
                    throw new ArgumentNullException(nameof(productModel));
                }
                if (!ModelState.IsValid)
                {
                    return View(productModel);
                }
                var tuple = productService.UpsertProduct(productModel);
                if (tuple.Item1 == true)
                {
                    return RedirectToAction(nameof(Products));
                }
                else
                {
                    return View(productModel);
                }
            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult DeleteProduct(int id)
        {
            try
            {
                if (id <= 0)
                    return RedirectToAction(nameof(Products));

                var tuple = productService.DeleteProduct(id);
                return RedirectToAction(nameof(Products));

            }
            catch (Exception ex)
            {
                return Json(new { Success = false, Message = ex.Message });
            }
        }

        [HttpPost]
        public ActionResult ChangeProductData(int productId)
        {
            try
            {

                var product = productService.GetProductById(productId);
                return Json(new { Data = product, Success = true, Message = "Product Data fetched successfully" });
            }
            catch (Exception ex)
            {
                return Json(new { Data = new ProductModel(), Success = false, Message = ex.Message }, JsonRequestBehavior.AllowGet);
            }
        }
    }
}