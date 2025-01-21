using Kopibara.Data;
using Kopibara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using RestSharp;
using Newtonsoft.Json;
using System.Text;
using System.Text.Json;


namespace Kopibara.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult ProceedToCheckout()
        {
            try
            {
                var cartItems = _context.Cart.ToList();

                foreach (var item in cartItems)
                {
                    // Check if an item with the same ItemName already exists in the Checkout table
                    var existingItem = _context.Checkout
                        .FirstOrDefault(c => c.ItemName == item.ItemName && c.Size == item.Size && c.BrewStyle == item.BrewStyle && c.SugarLevel == item.SugarLevel);

                    if (existingItem == null)  
                    {
                        var checkoutItem = new Checkout
                        {
                            ItemName = item.ItemName,
                            Size = item.Size,
                            BrewStyle = item.BrewStyle,
                            SugarLevel = item.SugarLevel,
                            Quantity = item.Quantity,
                            Price = item.Price,
                            TotalPrice = item.Quantity * item.Price,
                            TotalCost = $"₱{(item.Quantity * item.Price):F2}",
                            product_image = item.product_image
                        };

                        _context.Checkout.Add(checkoutItem);
                    }
                    else
                    {

                        return RedirectToAction("Cart", "Order");
                    }
                }

                // Save the changes to the database
                _context.SaveChanges();


                return RedirectToAction("Checkout", "Payment");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during checkout: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }

        public IActionResult ClearCheckout()
        {
            try
            {

                var checkoutItems = _context.Checkout.ToList();
                _context.Checkout.RemoveRange(checkoutItems);

                _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Checkout', RESEED, 0)");

                _context.SaveChanges();

                return RedirectToAction("Cart", "Order");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during clearing checkout: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }



        [HttpPost]
        public async Task<IActionResult> ProceedToPayment(string paymentMethod)
        {
            try
            {
                var checkoutItems = _context.Checkout.ToList();
                if (checkoutItems == null || !checkoutItems.Any())
                {
                    return RedirectToAction("Method", "Payment", new { message = "No items in the cart." });
                }

                var referenceNumber = Guid.NewGuid().ToString("N").Substring(0, 12).ToUpper();

                // Retrieve full name and email from cookies
                var userName = HttpContext.Request.Cookies["UserName"] ?? "Unknown User";
                var userEmail = HttpContext.Request.Cookies["UserEmail"] ?? "unknown@example.com";

                var lineItemsJson = string.Join(",", checkoutItems.Select(item =>
                    $"{{\"currency\":\"PHP\",\"images\":[\"https://localhost:44370/{item.product_image}\"]," +
                    $"\"amount\":{item.Price * 100}," +
                    $"\"name\":\"{item.ItemName}\",\"quantity\":{item.Quantity}}}"));
                 
                var jsonBody = $"{{\"data\":{{\"attributes\":{{\"send_email_receipt\":true," +
                    $"\"show_description\":true,\"show_line_items\":true,\"split_payment\":{{\"transfer_to\":\"Mr. Pogi\"}}," +
                    $"\"cancel_url\":\"https://localhost:44370/Payment/Method\"," +
                    $"\"description\":\"Thank you for choosing our cafe! We’re thrilled to have you with us. Before you go, please review your order and select your preferred payment method. " +
                    $"We appreciate your support and hope you enjoy your time with us! " +
                    $"Please allow 10-15 minutes for the preparation of your order. We appreciate your patience and thank you for your understanding.\"," +
                    $"\"line_items\":[{lineItemsJson}],\"payment_method_types\":[\"{paymentMethod}\"]," +
                    $"\"reference_number\":\"{referenceNumber}\",\"statement_descriptor\":\"Please allow 10-15 minutes for the preparation of your order. We appreciate your patience and thank you for your understanding.\"," +
                    $"\"success_url\":\"https://localhost:44370/Payment/ThankYou\"}}}}}}";

                // Set up the API client and request headers
                var options = new RestClientOptions("https://api.paymongo.com/v1/checkout_sessions");
                var client = new RestClient(options);
                var request = new RestRequest("");
                request.AddHeader("accept", "application/json");
                request.AddHeader("authorization", "Basic c2tfdGVzdF96TUNha3g4R2M3Vm05YkxoZERVQnd3SnM6");
                request.AddJsonBody(jsonBody, false);

                var response = await client.PostAsync(request);

                if (response != null && response.IsSuccessStatusCode)
                {
                    var responseJson = System.Text.Json.JsonSerializer.Deserialize<JsonElement>(response.Content!);

                    var checkoutUrl = responseJson
                        .GetProperty("data")
                        .GetProperty("attributes")
                        .GetProperty("checkout_url")
                        .GetString();

                    if (!string.IsNullOrEmpty(checkoutUrl))
                    {
                        // Insert data into Orders table
                        foreach (var item in checkoutItems)
                        {
                            var order = new OrderList
                            {
                                ReferenceNumber = referenceNumber,
                                Email = userEmail,
                                Name = userName,
                                OrderDate = DateTime.Now.Date,
                                OrderTime = DateTime.Now.TimeOfDay,
                                Quantity = item.Quantity,
                                ItemName = item.ItemName,
                                Size = item.Size, 
                                BrewStyle = item.BrewStyle, 
                                SugarLevel = item.SugarLevel, 
                                TotalPrice = item.TotalPrice,
                                Total = item.TotalCost.ToString(), 
                                PaymentMethod = paymentMethod, 
                                Status = "Paid - Unclaimed" 
                            };

                            _context.Orders.Add(order);
                        }

                        await _context.SaveChangesAsync();

                        // Redirect the user to the checkout URL
                        return Redirect(checkoutUrl);
                    }
                    else
                    {
                        Console.WriteLine("Checkout URL is missing in the response.");
                        return RedirectToAction("Method", "Payment", new { message = "There was an error creating the checkout session." });
                    }
                }
                else
                {
                    Console.WriteLine($"Error: {response.StatusCode} - {response.Content}");
                    return RedirectToAction("Method", "Payment", new { message = "There was an issue with the payment gateway." });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error during API call: {ex.Message}");
                return RedirectToAction("Method", "Payment", new { message = "An error occurred while processing your payment." });
            }
        }


        public ActionResult Checkout()
        {
            try
            {

                var checkoutItems = _context.Checkout.ToList();

                if (checkoutItems == null || !checkoutItems.Any())
                {
                    return RedirectToAction("Checkout", "Payment");
                }

                foreach (var checkoutItem in checkoutItems)
                {
                    var cartItem = _context.Cart
                        .FirstOrDefault(c => c.ItemName == checkoutItem.ItemName &&
                                             c.Size == checkoutItem.Size &&
                                             c.BrewStyle == checkoutItem.BrewStyle &&
                                             c.SugarLevel == checkoutItem.SugarLevel);

                    if (cartItem != null)
                    {

                        checkoutItem.Quantity = cartItem.Quantity;
                        checkoutItem.TotalPrice = cartItem.Quantity * cartItem.Price;
                        checkoutItem.TotalCost = $"₱{(cartItem.Quantity * cartItem.Price):F2}";
                    }
                }

                _context.SaveChanges();

                var totalAmount = checkoutItems.Sum(x => x.TotalPrice);

                ViewBag.TotalAmount = totalAmount;

                return View(checkoutItems);
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Error during checkout: {ex.Message}");
                return StatusCode(500, "Internal server error");
            }
        }


        public ActionResult Method()
        {
            return View();
        }

        public ActionResult ThankYou()
        {
            var cartItems = _context.Cart.ToList();

            _context.Cart.RemoveRange(cartItems);
            _context.SaveChanges();
            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Cart', RESEED, 0)");

            var checkoutItems = _context.Checkout.ToList();

            _context.Checkout.RemoveRange(checkoutItems);

            _context.Database.ExecuteSqlRaw("DBCC CHECKIDENT ('Checkout', RESEED, 0)");

            _context.SaveChanges();

            return View();
        }

        public ActionResult Cart()
        {
            var cartItems = _context.Cart.ToList();
            return View(cartItems);
        }
    }
}