using DairyWeb.Entity;
using DairyWeb.Extension;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyWeb.Repository
{
    public class ProductRepository : BaseRepository<ProductMaster>, IProductRepository
    {

        public Tuple<bool, string> UpsertProduct(ProductMaster productMaster)
        {
            var RetailerId = Convert.ToInt32(HttpContext.Current.Session["RetailerId"]);
            productMaster.RetailerId = RetailerId;
            string message = string.Empty;
            bool success = false;
            if (productMaster.Id == 0)
            {
                productMaster.InsertedDate = DateTime.Now;
                productMaster.InsertedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                db.ProductMaster.Add(productMaster);
                db.SaveChanges();
                message = "Product added successfully";
                success = true;
            }
            else
            {
                var product = db.ProductMaster.Find(productMaster.Id);
                if (product != null)
                {
                    product.Id = productMaster.Id;
                    product.Name = productMaster.Name;
                    product.ProductType = productMaster.ProductType;
                    product.Price = productMaster.Price;
                    product.CompanyName = productMaster.CompanyName;
                    product.MeasurementUnit = productMaster.MeasurementUnit;
                    product.UpdatedDate = DateTime.Now;
                    product.UpdatedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                    product.RetailerId = productMaster.RetailerId;
                    // db.ProductMaster.Update(product);
                    db.SaveChanges();
                    message = "Product updated successfully";
                    success = true;
                }
            }
            return new Tuple<bool, string>(success, message);
        }

        public Tuple<bool, string> DeleteProduct(int productId)
        {
            var message = string.Empty;
            var success = false;
            var p = new DynamicParameters();
            p.Add("@Id", productId);
            p.Add("@TableName", "product");
            var msg = CommonOperations.Query<string>("DeleteById", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            if (msg==null)
            {
                message = "Product deleted successfully.";
                success = true;
            }
            else
            {
                message = "Product not found.";
                success = false;
            }

            return new Tuple<bool, string>(success, message);
        }
        public IEnumerable<ProductMaster> GetProducts(int retailerId)
        {
            var p = new DynamicParameters();
            p.Add("@retailerId", retailerId);
            var products = CommonOperations.Query<ProductMaster>("GetProducts", p, commandType: System.Data.CommandType.StoredProcedure).ToList();
            return products;
        }
        public ProductMaster GetProductById(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            var productMaster = CommonOperations.Query<ProductMaster>("GetProductById", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return productMaster;
        }
    }
}
