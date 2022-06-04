using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DairyWeb.Entity
{
    public class Admin
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int UserId { get; set; }
        public DateTime? InsertedDate { get; set; }
        public int ? InsertedByUserId { get; set; }
        public int ? UpdatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}