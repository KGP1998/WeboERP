using DairyWeb.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DairyWeb.Repository
{
    public interface IProductRepository : IBaseRepository<ProductMaster>
    {
        Tuple<bool, string> UpsertProduct(ProductMaster productMaster);
        Tuple<bool, string> DeleteProduct(int productId);
        IEnumerable<ProductMaster> GetProducts(int retailerId);
        ProductMaster GetProductById(int id);
    }
}
