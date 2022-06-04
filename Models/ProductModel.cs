using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace DairyWeb.Models
{
    public class ProductModel
    {
        public int Id { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Name is required")]
        public string Name { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Type is required")]
        public int ProductType { get; set; }

        [Required(AllowEmptyStrings =false,ErrorMessage ="Price is required")]
        [Range(1,10000000,ErrorMessage ="Please enter valid price")]
        public decimal Price { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Company is required")]
        public string CompanyName { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Measurement Unit is required")]
        public string MeasurementUnit { get; set; }

        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }

        public List<SelectListItem> ProductTypeList { get; set; }
    }
}
