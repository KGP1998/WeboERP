using DairyWeb.Entity;
using DairyWeb.Models;
using System.Collections.Generic;

namespace DairyWeb.Repository
{
    public interface IAdminRepository : IBaseRepository<Admin>
    {
        IEnumerable<AdminModel> GetAdmins();
        AdminModel GetAdminById(int id);

    }


}
