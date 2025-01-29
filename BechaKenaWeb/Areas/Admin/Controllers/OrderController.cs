using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BechaKena.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class OrderController : Controller
	{
		private readonly IRepositoryWrapper _db;
		public OrderController(IRepositoryWrapper db)
		{
			_db = db;
		}
		public IActionResult Index()
		{
			return View();
		}
		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			IEnumerable<OrderHeader> orderHeaders;
			orderHeaders = _db.OrderHeader.GetAll(includeProperties: "ApplicationUser");
			return Json(new { data = orderHeaders });
		}
		#endregion
	}
}
