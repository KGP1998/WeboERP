using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DairyWeb.Models
{
    public class AdminModel : UsersDetailsModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^.*\S.*$", ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false)]
        [RegularExpression(@"^.*\S.*$", ErrorMessage = "User Name is required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false)]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password length must be more than 6 character")]
        [RegularExpression(@"^.*\S.*$", ErrorMessage = "Password is required")]
        public string Password { get; set; }

        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }


    }
}