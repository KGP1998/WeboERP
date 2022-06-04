using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DairyWeb.Models
{
    public class CustomerModel
    {
        public int Id { get; set; }
        [Required]
        public int CustomerType { get; set; }
        public int SerialNo { get; set; }

        [Required(ErrorMessage ="Route is required")]
        public int RouteId { get; set; }

        public string RouteName { get; set; }

        [Required(ErrorMessage = "Product is required")]
        public int ProductId { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "First name is required")]

        public string FirstName { get; set; }

       
        public string LastName { get; set; }

     
        public string MiddleName { get; set; }

        
        public string Address { get; set; }

        
        [RegularExpression(@"[0-9]{10}", ErrorMessage = "Not a valid phone number")]
        public string PhoneNo { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Morning Qty is required")]
        [Range(0, 10000000, ErrorMessage = "Please enter valid M Qty")]
        public decimal? MorningQuantity { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Morning Rate is required")]
        [Range(0, 10000000, ErrorMessage = "Please enter valid M Rate")]
        public decimal? MorningPrice { get; set; }

        //[Required(AllowEmptyStrings = false, ErrorMessage = "Evening Qty is required")]
        [Range(0, 10000000, ErrorMessage = "Please enter valid E Qty")]
        public decimal? EveningQuantity { get; set; }


        //[Required(AllowEmptyStrings = false, ErrorMessage = "Evening Rate is required")]
        [Range(0, 10000000, ErrorMessage = "Please enter valid E Rate")]
        public decimal? EveningPrice { get; set; }

        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public SelectList Routes { get; set; }
        public SelectList Products { get; set; }

        public bool IsActive { get; set; }

    }
}
