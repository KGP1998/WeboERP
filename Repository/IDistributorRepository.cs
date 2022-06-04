using DairyWeb.Entity;
using DairyWeb.Models;
using System.Collections.Generic;

namespace DairyWeb.Repository
{
    public interface IDistributorRepository : IBaseRepository<Distributor>
    {
        IEnumerable<DistributorModel> GetDistributors();
        DistributorModel GetDistributorById(int id);
    }
        


}
