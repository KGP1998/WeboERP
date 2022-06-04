using DairyWeb.Entity;
using DairyWeb.AppContext;
using System;
using System.Web;
using System.Linq;
using Dapper;
using DairyWeb.Extension;
using System.Collections.Generic;

namespace DairyWeb.Repository
{
    public class CustomerRepository : BaseRepository<Customer>, ICustomerRepository
    {


        public Tuple<bool, string> UpsertCustomer(Customer customer)
        {
            var RetailerId = Convert.ToInt32(HttpContext.Current.Session["RetailerId"]);
            var customerCount = 0;
            if (db.Customer.Where(x => x.RetailerId == RetailerId).Count() > 0)
            {
                customerCount = db.Customer.Where(x => x.RetailerId == RetailerId).Max(x => x.SerialNo);
            }

            customer.RetailerId = RetailerId;
            string message = string.Empty;
            bool success = false;
            if (customer.Id == 0)
            {
                customer.SerialNo = customerCount + 1;
                customer.InsertedDate = DateTime.Now;
                customer.InsertedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                db.Customer.Add(customer);
                db.SaveChanges();
                message = "Customer added successfully";
                success = true;
            }
            else
            {
                var oldCustomer = db.Customer.Find(customer.Id);
                if (oldCustomer != null)
                {
                    oldCustomer.Id = customer.Id;
                    oldCustomer.FirstName = customer.FirstName;
                    oldCustomer.LastName = customer.LastName;
                    oldCustomer.MiddleName = customer.MiddleName;
                    oldCustomer.MorningPrice = customer.MorningPrice;
                    oldCustomer.MorningQuantity = customer.MorningQuantity;
                    oldCustomer.EveningPrice = customer.EveningPrice;
                    oldCustomer.EveningQuantity = customer.EveningQuantity;
                    oldCustomer.Address = customer.Address;
                    oldCustomer.CustomerType = customer.CustomerType;
                    oldCustomer.PhoneNo = customer.PhoneNo;
                    oldCustomer.RouteId = customer.RouteId;
                    oldCustomer.UpdatedDate = DateTime.Now;
                    oldCustomer.UpdatedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                    oldCustomer.RetailerId = customer.RetailerId;
                    oldCustomer.IsActive = customer.IsActive;
                    //db.Customer.Update(oldCustomer);
                    db.SaveChanges();
                    message = "Customer updated successfully";
                    success = true;
                }
            }
            return new Tuple<bool, string>(success, message);
        }
        public Tuple<bool, string> DeleteCustomer(int customerId)
        {
            var message = string.Empty;
            var success = false;
            var p = new DynamicParameters();
            p.Add("@Id", customerId);
            p.Add("@TableName", "customer");
            var msg = CommonOperations.Query<string>("DeleteById", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            if (msg == null)
            {
                success = true;
                message = "Customer deleted successfully.";
            }
            else
            {
                success = true;
                message = "Customer not found.";
            }
            return new Tuple<bool, string>(success, message);
        }

        public IEnumerable<Customer> GetCustomers(int retailerId)
        {
            var p = new DynamicParameters();
            p.Add("@retailerId", retailerId);
            var customers = CommonOperations.Query<Customer>("GetCustomers", p, commandType: System.Data.CommandType.StoredProcedure).ToList();
            return customers;
        }

        public Customer GetCustomerById(int id)
        {
            var p = new DynamicParameters();
            p.Add("@Id", id);
            var customer = CommonOperations.Query<Customer>("GetCustomerById", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return customer;

        }
    }
}
