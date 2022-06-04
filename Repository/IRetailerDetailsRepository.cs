using DairyWeb.Entity;
using DairyWeb.Models;
using System.Collections.Generic;

namespace DairyWeb.Repository
{
    public interface IRetailerDetailsRepository : IBaseRepository<RetailerDetails>
    {
        IEnumerable<RetailerDetailsModel> GetRetailerDetails();
        RetailerDetailsModel GetRetailerDetailsById(int id);
        int GetRetailerDetailsByRetailerId(int id);
    }
        


}
