using System.ComponentModel.DataAnnotations;

namespace Kopibara.Models
{
    public class Kopi_products
    {
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string product_name { get; set; } 

        [Required]
        [StringLength(250)]
        public string description { get; set; }

		[Required]
		[StringLength(250)]
		public string category { get; set; } 

		[Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal price { get; set; } 

        [Required]
        public string product_image { get; set; } 

        public decimal TotalCost => price * Quantity; // Calculate total cost based on quantity and price
    }
}