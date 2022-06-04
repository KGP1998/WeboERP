using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DairyWeb.Entity;

namespace DairyWeb.Repository
{
    public interface ICustomerRepository : IBaseRepository<Customer>
    {
        Tuple<bool, string> UpsertCustomer(Customer customer);
        Tuple<bool, string> DeleteCustomer(int customerId);
        IEnumerable<Customer> GetCustomers(int retailerId);
        Customer GetCustomerById(int id);
    }
}
