using DairyWeb.Entity;
using DairyWeb.Models;
using DairyWeb.Repository;
using System;
using System.Collections.Generic;
using System.Web;

namespace DairyWeb.Services
{
    public class DistributorService : BaseService, IDistributorService
    {
        private readonly IDistributorRepository _distributorRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRetailerRepository _retailerRepository;
        private readonly IRetailerDetailsRepository _retailerDetailsRepository;
        private HttpContextBase Context { get; set; }
        public DistributorService()
        {

            _distributorRepository = new DistributorRepository();
            _userRepository = new UserRepository();
            _retailerDetailsRepository = new RetailerDetailsRepository();
            _retailerRepository = new RetailerRepository();

        }
        public IEnumerable<DistributorModel> GetDistributors()
        {
            var distributors = _distributorRepository.GetDistributors();
            return distributors;
        }
        public DistributorModel GetDistributorById(int distributorId)
        {
            var distributor = _distributorRepository.GetDistributorById(distributorId);
            return distributor;
        }
        public void UpsertDistributor(DistributorModel distributorModel)
        {
            if (distributorModel.Id == 0)
            {
                Users user = new Users()
                {
                    Name = distributorModel.Name,
                    UserName = distributorModel.UserName,
                    Password = distributorModel.Password,
                    RoleId = 3,
                    IsActive = true,
                    Address = distributorModel.Address,
                    PhNo1 = distributorModel.PhNo1,
                    PhNo2 = distributorModel.PhNo2,
                    PanNo = distributorModel.PanNo,
                    AdharNo = distributorModel.AdharNo,
                    InsertedDate = DateTime.Now,
                    LoggedInCount = 0
                };
                var userId = _userRepository.UpsertUser(user);
                if (userId > 0)
                {
                    Distributor distributor = new Distributor()
                    {
                        Name = distributorModel.Name,
                        InsertedDate = DateTime.Now,
                        InsertedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]),
                        UserId = userId,
                        AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminId"])
                    };
                    _distributorRepository.Create(distributor);
                }

            }
            else
            {
                var distributor = _distributorRepository.GetById(distributorModel.Id);
                if (distributor != null)
                {
                    var user = _userRepository.GetById(distributor.UserId);
                    if (user != null)
                    {
                        user.Name = distributorModel.Name;
                        user.Password = distributorModel.Password;
                        user.IsActive = true;
                        user.Address = distributorModel.Address;
                        user.PhNo1 = distributorModel.PhNo1;
                        user.PhNo2 = distributorModel.PhNo2;
                        user.PanNo = distributorModel.PanNo;
                        user.AdharNo = distributorModel.AdharNo;
                        user.UpdatedDate = DateTime.Now;
                        _userRepository.UpsertUser(user);
                    }

                    distributor.Name = distributorModel.Name;
                    distributor.UpdatedDate = DateTime.Now;
                    distributor.UpdatedByUserId = Convert.ToInt32(HttpContext.Current.Session["LoggedInUserId"]);
                    distributor.AdminId = Convert.ToInt32(HttpContext.Current.Session["AdminId"]);
                    _distributorRepository.SaveChanges();
                }

            }


        }

        public void DeleteDistributor(int distributorId, bool isActive)
        {
            var distributor = _distributorRepository.GetById(distributorId);
            if (distributor != null)
            {
                var user = _userRepository.GetById(distributor.UserId);
                if (user != null)
                {
                    user.IsActive = isActive;
                    _userRepository.SaveChanges();
                }

                if (isActive == false)
                {
                    var retailers = _retailerRepository.GetAll(x => x.DistributorId == distributor.Id);
                    if (retailers != null)
                    {
                        foreach (var retailer in retailers)
                        {
                            var retailerDetails = _retailerDetailsRepository.GetAll(x => x.RetailerId == retailer.Id);
                            if (retailerDetails != null)
                            {
                                foreach (var retailerDetails1 in retailerDetails)
                                {

                                    var users = _userRepository.GetById(retailerDetails1.UserId);
                                    if (users != null)
                                    {
                                        users.IsActive = false;
                                        _userRepository.SaveChanges();
                                    }
                                }
                            }
                        }
                    }
                }
                //_distributorRepository.Delete(distributor.Id);
            }
        }
    }
}
