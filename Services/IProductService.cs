using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DairyWeb.Models;

namespace DairyWeb.Services
{
    public interface IProductService
    {
        IEnumerable<ProductModel> GetProduct(int retailerId);

        Tuple<bool, string> UpsertProduct(ProductModel productModel);
        Tuple<bool, string> DeleteProduct(int productId);
        ProductModel GetProductById(int id);
    }
}
