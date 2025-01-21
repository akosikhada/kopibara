using Kopibara.Data;
using Kopibara.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Kopibara.Controllers
{
    public class HomeController : Controller
    {
		private readonly ApplicationDbContext _context;

		public HomeController(ApplicationDbContext context)
		{
			_context = context;
		}

		private readonly ILogger<HomeController> _logger;

        public ActionResult Home()
        {
            return View();
        }
		public async Task<IActionResult> Product()
		{
			var products = await _context.products.ToListAsync();
			return View(products);
		}

[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
