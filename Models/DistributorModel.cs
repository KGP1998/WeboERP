using System;
using System.ComponentModel.DataAnnotations;

namespace DairyWeb.Models
{
    public class DistributorModel : UsersDetailsModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        [RegularExpression(@"^.*\S.*$", ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Username is required")]
        [RegularExpression(@"^.*\S.*$", ErrorMessage = "UserName is required")]
        public string UserName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [DataType(DataType.Password)]
        [MinLength(6, ErrorMessage = "Password length must be more than 6 character")]
        [RegularExpression(@"^.*\S.*$", ErrorMessage = "Password is required")]
        public string Password { get; set; }
        public int AdminId { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}