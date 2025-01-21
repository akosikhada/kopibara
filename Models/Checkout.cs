namespace Kopibara.Models
{
    public class Checkout
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string Size { get; set; }
        public string BrewStyle { get; set; }
        public string SugarLevel { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal TotalPrice { get; set; }
        public string TotalCost { get; set; }
        public string product_image { get; set; }
    }
}
