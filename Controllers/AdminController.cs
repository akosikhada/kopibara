using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Kopibara.Data;
using Kopibara.Models;
using System.Net;
using System.Net.Mail;


namespace Kopibara.Controllers
{
	public class AdminController : Controller
	{
		private readonly ApplicationDbContext _context;

		public AdminController(ApplicationDbContext context)
		{
			_context = context;
		}

		public IActionResult AdminLogin()
		{
			return View();
		}

		[HttpPost]
		public async Task<IActionResult> AdminLogin(string username, string password)
		{
			if (username == "Admin" && password == "kopibara")
			{
				var claims = new List<Claim>
		{
			new Claim(ClaimTypes.Name, "Admin"),
			new Claim(ClaimTypes.Role, "Admin")
		};

				var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
				var principal = new ClaimsPrincipal(identity);

				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				HttpContext.Session.SetString("IsAdminLoggedIn", "true");

				return RedirectToAction("OrderList", "Admin");
			}
			else
			{

				ViewBag.ErrorMessage = "Invalid credentials. Please try again.";
				return View("~/Views/Auth/AdminLogin.cshtml");
			}
		}

		[HttpPost]
		public IActionResult Login(string username, string password)
		{

			if (username == "Admin" && password == "kopibara")
			{
				return RedirectToAction("OrderList", "Admin");
			}
			else
			{

				ViewBag.ErrorMessage = "Invalid username or password.";
				return View("AdminLogin");
			}
		}

		public async Task<IActionResult> Logout()
		{
			// Sign out the user
			await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

			HttpContext.Session.Clear();

			HttpContext.Session.Remove("IsAdminLoggedIn");

			return RedirectToAction("Home", "Home");
		}

		[Authorize(Roles = "Admin")]
		public IActionResult OrderList()
		{
			var orders = _context.Orders.ToList();
			ViewData["IsOrderListPage"] = true;
			return View(orders);
		}

		[HttpPost]
		public async Task<IActionResult> AddProduct(string product_name, string description, decimal price, string category, IFormFile product_image)
		{
			if (product_image == null || product_image.Length == 0)
			{
				ModelState.AddModelError("product_image", "Please upload an image.");
				return View();
			}

			if (string.IsNullOrWhiteSpace(category))
			{
				ModelState.AddModelError("category", "Please select a valid category.");
				return View();
			}

			string imagesFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
			Directory.CreateDirectory(imagesFolder);
			string filePath = Path.Combine(imagesFolder, product_image.FileName);

			using (var stream = new FileStream(filePath, FileMode.Create))
			{
				await product_image.CopyToAsync(stream);
			}

			var product = new Kopi_products
			{
				product_name = product_name,
				description = description,
				price = price,
				category = category,
				Quantity = 1, 
				product_image = "/images/" + product_image.FileName 
			};

			_context.products.Add(product);
			await _context.SaveChangesAsync();

			return RedirectToAction("ProductList", "Admin");
		}


        public IActionResult EditProduct(int id)
        {
            var product = _context.products.FirstOrDefault(p => p.Id == id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Kopi_products product, IFormFile product_image)
        {
            var existingProduct = _context.products
                .FirstOrDefault(p => p.Id == product.Id);  

            if (existingProduct != null)
            {

                existingProduct.product_name = product.product_name; 
                existingProduct.description = product.description;
                existingProduct.price = product.price;
                existingProduct.category = product.category;

                if (product_image != null)
                {
                    var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", product_image.FileName);
                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        product_image.CopyTo(stream);
                    }
                    existingProduct.product_image = "/images/" + product_image.FileName; 
                }

                _context.SaveChanges();

                return RedirectToAction("ProductList");
            }

            return NotFound();
        }

        [HttpGet]
		public IActionResult DeleteProduct(string product_name)
		{
			var product = _context.products.FirstOrDefault(p => p.product_name == product_name);

			if (product != null)
			{

				_context.products.Remove(product);
				_context.SaveChanges();

				return RedirectToAction("ProductList", "Admin");
			}

			return NotFound();
		}


		public IActionResult SendEmail(int orderId)
		{
			var order = _context.Orders.FirstOrDefault(o => o.Id == orderId);

			if (order != null)
			{
				var smtpClient = new SmtpClient("smtp.gmail.com")
				{
					Port = 587,
					Credentials = new NetworkCredential("enriquedasalla03@gmail.com", "fjsw mjha sclo ecoi"), 
					EnableSsl = true,
				};

				var mailMessage = new MailMessage
				{
					From = new MailAddress("enriquedasalla03@gmail.com"),
					Subject = "Your Order is Ready for Pickup!",
					Body = $"Dear {order.Name},\n\n" +
						   "We are excited to inform you that your order is now ready for pickup!\n\n" +
						   "Order Details:\n" +
						   $"Order ID: {order.Id}\n" +
						   $"Reference Number: {order.ReferenceNumber}\n" +
						   $"Item Name: {order.ItemName}\n" +
						   $"Quantity: {order.Quantity}\n" +
						   $"Size: {order.Size}\n" +
						   $"Brew Style: {order.BrewStyle}\n" +
						   $"Sugar Level: {order.SugarLevel}\n" +
						   $"Total Amount: {order.Total}\n\n" +
						   "Please feel free to come by and claim your order at your convenience. If you have any questions or concerns, don't hesitate to contact us.\n\n" +
						   "We sincerely thank you for choosing our service and look forward to serving you again in the future.\n\n" +
						   "Best regards,\n" +
						   "The Kopibara Team\n" +
						   "Your Order Number: " + order.Id,
					IsBodyHtml = false,
				};

				mailMessage.To.Add(order.Email);

				smtpClient.Send(mailMessage);

                TempData["SuccessMessage"] = "Send receipt successful!";
            }

			return RedirectToAction("OrderList"); 
		}

        [HttpPost]
        public ActionResult UpdateOrderStatus(Dictionary<string, string> status)
        {
            if (status != null)
            {
                foreach (var item in status)
                {

                    var order = _context.Orders.FirstOrDefault(o => o.ItemName == item.Key);
                    if (order != null)
                    {
                        // Update the order's status
                        order.Status = item.Value;
                    }
                }

                // Save changes to the database
                _context.SaveChanges();

                TempData["SuccessMessage"] = "Save successful!";
            }

            return RedirectToAction("OrderList", "Admin");
        }





        public ActionResult AddProduct()
		{
			return View();
		}

        /*public ActionResult EditProduct()
        {
            return View();
        }*/

        public async Task<IActionResult> ProductList()
		{
			var products = await _context.products.ToListAsync();
			return View(products);
		}
	}
}
