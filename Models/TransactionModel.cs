using System;
using System.ComponentModel.DataAnnotations;

namespace DairyWeb.Models
{
    public class TransactionModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int SerialNo { get; set; }
        [Required(ErrorMessage = "Please select customer")]
        public int CustomerId { get; set; }
        public int PaymentType { get; set; }
        public string PaymentTypeText { get; set; }
        [Required(ErrorMessage = "Please enter amount", AllowEmptyStrings = false)]
        public decimal Amount { get; set; }
        public string Notes { get; set; }
        public DateTime TransactionDate { get; set; }
        public int InsertedByUserId { get; set; }
        public int UpdatedByUserId { get; set; }
        public DateTime InsertedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}