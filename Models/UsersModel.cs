using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DairyWeb.Models
{
    public class UsersModel:UsersDetailsModel
    {
        public int Id { get; set; }
        //public int RetailerId { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings =false)]
        
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        public string Password { get; set; }

        [Required]
        public int? RoleId { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        

    }
}