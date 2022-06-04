using DairyWeb.Models;
using System.Collections.Generic;

namespace DairyWeb.Services
{
    public interface IAdminService
    {
        IEnumerable<AdminModel> GetAdmins();
        void UpsertAdmin(AdminModel adminModel);
        AdminModel GetAdminsById(int adminModel);
        void DeleteAdmin(int adminId,bool isActive);
    }
}
