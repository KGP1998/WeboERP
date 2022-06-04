using System;

namespace DairyWeb.Entity
{
    public class ProductMaster
    {
        public int Id { get; set; }
        public int RetailerId { get; set; }
        public string Name { get; set; }
        public int ProductType { get; set; }
        public decimal Price { get; set; }
        public string CompanyName { get; set; }
        public string MeasurementUnit { get; set; }

        public DateTime? InsertedDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public int? InsertedByUserId { get; set; }
        public int? UpdatedByUserId { get; set; }
    }  
}      
       