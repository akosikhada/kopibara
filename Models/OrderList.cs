using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Kopibara.Models
{
    public class OrderList
    {
        public int Id { get; set; } 
        public string ReferenceNumber { get; set; }
        public string Email { get; set; }
        public string Name { get; set; }
        public DateTime OrderDate { get; set; }
        public TimeSpan OrderTime { get; set; }
        public int Quantity { get; set; }
        public string ItemName { get; set; }
        public string Size { get; set; }
        public string BrewStyle { get; set; }
        public string SugarLevel { get; set; }
        public decimal TotalPrice { get; set; }
        public string Total { get; set; }
        public string PaymentMethod { get; set; }
        public string Status { get; set; }
    }
}
