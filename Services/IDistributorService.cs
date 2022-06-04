using DairyWeb.Models;
using System.Collections.Generic;

namespace DairyWeb.Services
{
    public interface IDistributorService
    {
        IEnumerable<DistributorModel> GetDistributors();
        void UpsertDistributor(DistributorModel distributorModel);
        DistributorModel GetDistributorById(int distributorId);
        void DeleteDistributor(int distributorId, bool isActive);
    }
}
