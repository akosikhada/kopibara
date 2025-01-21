using Kopibara.Data;
using Kopibara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Kopibara.Controllers
{
    public class OrderController : Controller
    {

        private readonly ApplicationDbContext _context;

        public OrderController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public ActionResult SubmitOrder(CoffeeOrder order)
        {

            order.ItemName = Request.Form["ItemName"];
            order.Size = Request.Form["Size"];
            order.BrewStyle = Request.Form["BrewStyle"];
            order.SugarLevel = Request.Form["SugarLevel"];
            order.Quantity = int.Parse(Request.Form["Quantity"]);
            order.product_image = Request.Form["ProductImage"];

            decimal pricePerItem = decimal.Parse(Request.Form["Price"]);
            order.Price = pricePerItem;

            decimal total = pricePerItem * order.Quantity;
            order.TotalPrice = total; 
            order.TotalCost = "₱" + total.ToString("F2"); 

            try
            {
                _context.Cart.Add(order);
                _context.SaveChanges();
                return RedirectToAction("Cart");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error saving to database: {ex.Message}");
            }

            return View("Modify", order);
        }

        [HttpPost]
        public IActionResult UpdateCart([FromBody] List<CoffeeOrder> updatedCart)
        {
            if (updatedCart == null || !updatedCart.Any())
                return BadRequest(new { message = "No items to update." });

            try
            {
                foreach (var updatedItem in updatedCart)
                {
                    // Find the item by ItemName (since it's unique)
                    var item = _context.Cart.FirstOrDefault(x => x.ItemName == updatedItem.ItemName);

                    if (item != null)
                    {
                        item.Quantity = updatedItem.Quantity;
                        item.TotalPrice = updatedItem.Quantity * item.Price;
                        item.TotalCost = $"₱{(updatedItem.Quantity * item.Price):F2}";
                    }
                }

                _context.SaveChanges();
                return Ok(new { message = "Cart updated successfully." });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error updating cart: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while updating the cart." });
            }
        }

        [HttpPost]
        public IActionResult RemoveItem([FromBody] RemoveItemRequest request)
        {
            if (string.IsNullOrEmpty(request?.ItemName))
            {
                return BadRequest(new { message = "Invalid item name." });
            }

            try
            {
                // Find the item by ItemName (ensure it exists in the cart)
                var item = _context.Cart.FirstOrDefault(x => x.ItemName == request.ItemName);

                if (item != null)
                {
                    _context.Cart.Remove(item);
                    _context.SaveChanges();
                    return Ok(new { message = "Item removed successfully." });
                }
                else
                {
                    return NotFound(new { message = "Item not found." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing item: {ex.Message}");
                return StatusCode(500, new { message = "An error occurred while removing the item." });
            }
        }

        public class RemoveItemRequest
        {
            public string ItemName { get; set; }
        }

        public ActionResult Modify(string product_name)
        {
            
            var product = _context.products.FirstOrDefault(p => p.product_name == product_name);
            if (product == null)
            {
                return NotFound(); 
            }

            return View(product); 
        }

        public ActionResult Cart()
        {

            var cartItems = _context.Cart.ToList();

            return View(cartItems);
        }
    }
}