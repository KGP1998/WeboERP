using System.ComponentModel.DataAnnotations;

namespace DairyWeb.Models
{
    public class UsersDetailsModel
    {
        public bool? IsActive { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Phone No is required")]
        [MaxLength(10, ErrorMessage = "Enter valid number")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Not a valid phone number")]
        public string PhNo1 { get; set; }

        [MaxLength(10, ErrorMessage = "Enter valid number")]
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Not a valid phone number")]
        public string PhNo2 { get; set; }


        [RegularExpression("^([A-Za-z]){5}([0-9]){4}([A-Za-z]){1}$", ErrorMessage = "Invalid PAN Number")]
        public string PanNo { get; set; }


        [RegularExpression("[0-9]{12}", ErrorMessage = "Invalid Adhar Number")]
        public string AdharNo { get; set; }
        [Required(AllowEmptyStrings = false, ErrorMessage = "Address is required")]
        public string Address { get; set; }
    }
}