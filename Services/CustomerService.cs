using DairyWeb.Entity;
using DairyWeb.Models;
using DairyWeb.Repository;
using System;
using System.Collections.Generic;

namespace DairyWeb.Services
{
    public class CustomerService : BaseService, ICustomerService
    {

        private readonly ICustomerRepository _customerRepository;
        public Mapper.MapperConfig mapper;
        public CustomerService()
        {

            _customerRepository = new CustomerRepository();
            mapper = new Mapper.MapperConfig();
        }


        public IEnumerable<CustomerModel> GetCustomerData(int retailerId)
        {
            var customers = _customerRepository.GetCustomers(retailerId);
            return AutoMapper.Mapper.Map<IEnumerable<CustomerModel>>(customers);
        }
        public CustomerModel GetCustomerDataById(int id)
        {
            var customer = _customerRepository.GetCustomerById(id);
            return AutoMapper.Mapper.Map<CustomerModel>(customer);
        }

        public Tuple<bool, string> UpsertCustomer(CustomerModel customerModel)
        {

            var customer = AutoMapper.Mapper.Map<Customer>(customerModel);
            return _customerRepository.UpsertCustomer(customer);

        }

        public Tuple<bool, string> DeleteCustomer(int customerId)
        {
            return _customerRepository.DeleteCustomer(customerId);
        }
    }
}
