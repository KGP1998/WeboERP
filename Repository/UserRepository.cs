using DairyWeb.Entity;
using DairyWeb.Extension;
using DairyWeb.Models;
using Dapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyWeb.Repository
{
    public class UserRepository : BaseRepository<Users>, IUserRepository
    {
        public int UpsertUser(Users users)
        {
            var userId = 0;
            if (users.Id == 0)
            {
                users.InsertedDate = DateTime.Now;
                users.IsActive = true;
                users.LoggedInCount = 0;
                var users1 = db.Users.Add(users);
                db.SaveChanges();
                userId = users1.Id;

            }
            else
            {
                var oldUser = db.Users.Find(users.Id);
                oldUser.Name = users.Name;
                oldUser.UserName = users.UserName;
                oldUser.Password = users.Password;

                oldUser.RoleId = users.RoleId;
                oldUser.IsActive = true;
                oldUser.UpdatedDate = DateTime.Now;
                db.SaveChanges();
                userId = users.Id;
            }
            return userId;
        }

        public IEnumerable<Roles> GetRoles()
        {
            return db.Roles.ToList();
        }

        public Users GetPredecessorOfUser(Users user)
        {
            var p = new DynamicParameters();
            p.Add("@UserId", user.Id);
            p.Add("@UserRoleId", user.RoleId);
            var users = CommonOperations.Query<Users>("GetPredecessorOfUser", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return users;
        }

        public bool IsUserNameExists(string userName, int id = 0)
        {
            var p = new DynamicParameters();
            p.Add("@UserName", userName);
            p.Add("@UserId", id);
            var usersCount = CommonOperations.Query<int>("IsUserNameExists", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            if (usersCount == 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public bool SetDefualtPassword(string userName)
        {
            var p = new DynamicParameters();
            p.Add("@UserName", userName);
            var isPasswordUpdated = CommonOperations.Query<int>("SetDefualtPassword", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return isPasswordUpdated == 1;
        }

        public int UpdatePassword(UpdatePassword updatePassword, int userId)
        {
            var p = new DynamicParameters();
            p.Add("@OldPassword", updatePassword.OldPassword);
            p.Add("@NewPassword", updatePassword.NewPassword);
            p.Add("@UserId", userId);
            var isPasswordUpdated = CommonOperations.Query<int>("UpdatePassword", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return isPasswordUpdated;
        }

        public int UpdateProfile(UpdateProfileModel updateProfileModel, int userId)
        {
            var p = new DynamicParameters();
            p.Add("@Name", updateProfileModel.Name);
            p.Add("@AdharNo", updateProfileModel.AdharNo);
            p.Add("@PanNo", updateProfileModel.PanNo);
            p.Add("@PhNo1", updateProfileModel.PhNo1);
            p.Add("@PhNo2", updateProfileModel.PhNo2);
            p.Add("@Address", updateProfileModel.Address);
            p.Add("@UserId", userId);
            var isProfileUpdated = CommonOperations.Query<int>("UpdateProfile", p, commandType: System.Data.CommandType.StoredProcedure).FirstOrDefault();
            return isProfileUpdated;
        }
    }
}