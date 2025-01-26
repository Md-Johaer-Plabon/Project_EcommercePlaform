using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

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

		public IActionResult Details(int productId)
		{
			ShoppingCart cartObj = new()
			{
				Count = 1,
				ProductId = productId,
				Product = _db.Product.GetFirstOrDefault(u => u.Id == productId, includeProperties: "Category,CoverType"),
			};
			return View(cartObj);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		[Authorize]
		public IActionResult Details(ShoppingCart shoppingCart)
		{
			var claimsIdentity = (ClaimsIdentity)User.Identity;
			var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
			shoppingCart.ApplicationUserId = claim.Value;

			ShoppingCart cartFromDb = _db.ShoppingCart.GetFirstOrDefault(
				u => u.ApplicationUserId == claim.Value && u.ProductId == shoppingCart.ProductId);


			if (cartFromDb == null)
			{
				_db.ShoppingCart.Add(shoppingCart);
			}
			else
			{
				_db.ShoppingCart.IncrementContents(cartFromDb, shoppingCart.Count);
			}
			_db.Save();

			return RedirectToAction(nameof(Index));
		}

	}
}