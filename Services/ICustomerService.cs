using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DairyWeb.Models;
namespace DairyWeb.Services
{
    public interface ICustomerService
    {
        IEnumerable<CustomerModel> GetCustomerData(int retailerId);
        Tuple<bool, string> UpsertCustomer(CustomerModel customerModel);
        Tuple<bool, string> DeleteCustomer(int customerId);
        CustomerModel GetCustomerDataById(int id);
    }
}
