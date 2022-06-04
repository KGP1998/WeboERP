using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DairyWeb.Entity
{
    public class Users
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public int RoleId { get; set; }

        public string PhNo1 { get; set; }
        public string PhNo2 { get; set; }
        public string PanNo { get; set; }
        public string AdharNo { get; set; }
        public int LoggedInCount { get; set; }
        public DateTime? LastLoggedInTime { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool? IsActive { get; set; }
        public string Address { get; set; }
    }
}
