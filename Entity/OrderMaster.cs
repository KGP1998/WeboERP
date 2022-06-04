using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DairyWeb.Entity
{
    public class OrderMaster
    {
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public int? RouteId { get; set; }
        public int? ProductId { get; set; }
        public int? CustomerId { get; set; }
        public int? Shift { get; set; }
        public decimal? Quantity { get; set; }
        public decimal? Price { get; set; }
        public decimal? Total { get; set; }
        public DateTime?  OrderDate { get; set; }
        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? InsertedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }

    }
}
