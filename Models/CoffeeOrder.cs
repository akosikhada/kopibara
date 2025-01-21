using System.ComponentModel.DataAnnotations;

namespace Kopibara.Models
{
    public class CoffeeOrder
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string ItemName { get; set; }

        [Required]
        [StringLength(200)]
        public string Size { get; set; }

        [Required]
        [StringLength(200)]
        public string BrewStyle { get; set; }

        [Required]
        [StringLength(100)]
        public string SugarLevel { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int Quantity { get; set; }

        [Required]
        public decimal Price { get; set; }

        [Required]
        public decimal TotalPrice { get; set; }

        [Required]
        [StringLength(200)]
        public string product_image { get; set; }

        [Required]
        public string TotalCost { get; set; } 
    }

}
