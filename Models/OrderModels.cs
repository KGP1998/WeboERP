using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DairyWeb.Models
{
    public class OrderModels
    {
        public int Id { get; set; }

        [Required]
        public int? RouteId { get; set; }

        [Required]
        public int? ProductId { get; set; }

        [Required]
        public int? CustomerId { get; set; }

        [Required]
        public int? Shift { get; set; }
        
        public string RouteName { get; set; }
       
        public string ProductName { get; set; }
       
        public string CustomerName { get; set; }
        public int SerialNo { get; set; }
      
        public string ShiftName { get; set; }

        [Required]
        public decimal? Quantity { get; set; }

        [Required]
        public decimal? Price { get; set; }
       
        public decimal? Total { get; set; }
        public DateTime? OrderDate { get; set; }

    }
}
