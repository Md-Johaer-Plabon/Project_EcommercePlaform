using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BechaKena.Areas.Admin.Controllers
{
	[Area("Admin")]
	public class CompanyController : Controller
	{
		private readonly IRepositoryWrapper _db;

		public CompanyController(IRepositoryWrapper db)
		{
			_db = db;
		}

		public IActionResult Index()
		{
			return View();
		}

		//GET
		public IActionResult Upsert(int? id)
		{
			Company company = new();

			if (id == null || id == 0)
			{
				return View(company);
			}
			else
			{
				company = _db.Company.GetFirstOrDefault(u => u.Id == id);
				return View(company);
			}
		}

		//POST
		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Upsert(Company obj, IFormFile? file)
		{

			if (ModelState.IsValid)
			{

				if (obj.Id == 0)
				{
					_db.Company.Add(obj);
					TempData["success"] = "Company created successfully";
				}
				else
				{
					_db.Company.Update(obj);
					TempData["success"] = "Company updated successfully";
				}
				_db.Save();

				return RedirectToAction("Index");
			}
			return View(obj);
		}



		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var companyList = _db.Company.GetAll();
			return Json(new { data = companyList });
		}

		//POST
		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var obj = _db.Company.GetFirstOrDefault(u => u.Id == id);
			if (obj == null)
			{
				return Json(new { success = false, message = "Error while deleting" });
			}

			_db.Company.Remove(obj);
			_db.Save();
			return Json(new { success = true, message = "Delete Successful" });

		}
		#endregion
	}
}
