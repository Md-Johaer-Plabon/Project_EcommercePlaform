using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BechaKena.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IRepositoryWrapper _db;

        public HomeController(ILogger<HomeController> logger, IRepositoryWrapper db)
        {
            _logger = logger;
            _db = db;
        }

        public IActionResult Index()
        {
			IEnumerable<Product> productList = _db.Product.GetAll(includeProperties: "Category,CoverType");
			return View(productList);
		}

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

		public IActionResult Details(int id)
		{
			ShoppingCart cartObj = new()
			{
				Count = 1,
				Product = _db.Product.GetFirstOrDefault(u => u.Id == id, includeProperties: "Category,CoverType"),
			};
			return View(cartObj);
		}

	}
}