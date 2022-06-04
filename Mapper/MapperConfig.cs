using AutoMapper;
using DairyWeb.Entity;
using DairyWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DairyWeb.Mapper
{
    public class MapperConfig : Profile
    {
        public MapperConfig()
        {
            //Route
            CreateMap<RouteMaster, RouteModel>();
            CreateMap<RouteModel, RouteMaster>();

            //Product
            CreateMap<ProductMaster, ProductModel>();
            CreateMap<ProductModel, ProductMaster>();

            //Customer
            CreateMap<Customer, CustomerModel>();
            CreateMap<CustomerModel, Customer>();

            //Order
            CreateMap<OrderMaster, OrderModels>();
            CreateMap<OrderModels, OrderMaster>();

            //User
            CreateMap<Users, UsersModel>();
            CreateMap<UsersModel, Users>();

            //Admin
            CreateMap<Admin, AdminModel>();
            CreateMap<AdminModel, Admin>();
            
            //Distributor
            CreateMap<Distributor, DistributorModel>();
            CreateMap<DistributorModel, Distributor>();

            //Retailer
            CreateMap<Retailer, RetailerModel>();
            CreateMap<RetailerModel, Retailer>();

            //RetailerDetails
            CreateMap<RetailerDetails, RetailerDetailsModel>();
            CreateMap<RetailerDetailsModel, RetailerDetails>(); 

            //Transaction
            CreateMap<TransactionModel, TransactionMaster>();
            CreateMap<TransactionMaster, TransactionModel>();
        }


    }
}
