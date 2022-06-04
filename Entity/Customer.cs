using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace DairyWeb.Entity
{
    public class Customer
    {
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public int SerialNo { get; set; }
        public int ProductId { get; set; }
        public int CustomerType { get; set; }

        [NotMapped]
        public string RouteName { get; set; }
        public int RouteId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MiddleName { get; set; }
        public string Address { get; set; }
        public string PhoneNo { get; set; }
        public decimal? MorningQuantity { get; set; }
        public decimal? MorningPrice { get; set; }
        public decimal? EveningQuantity { get; set; }
        public decimal? EveningPrice { get; set; }
        public int ? InsertedByUserId { get; set; }
        public int ? UpdatedByUserId { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public bool IsActive { get; set; }

    }
}
