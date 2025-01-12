using BechaKena.Data.Data;
using BechaKena.Data.Repository;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace BechaKena.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ICategoryRepository _db;
        public CategoryController(ICategoryRepository db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Add(obj);
                _db.Save();

				TempData["Success"] = "Category Created Successfully!";

                return RedirectToAction("Index");
            }

            TempData["Error"] = "Failed To Create Category!";
            return View(obj);
        }

		public IActionResult Edit(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			//var categoryFromDb = _db.Categories.Find(id);
			var categoryFromDbFirst = _db.GetFirstOrDefault(u=>u.Id==id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}
			return View(categoryFromDbFirst);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Update(obj);
				_db.Save();

				TempData["Success"] = "Category Updated Successfully!";

				return RedirectToAction("Index");
			}

            TempData["Error"] = "Failed To Update Category!";
            return View(obj);
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			//var categoryFromDb = _db.Categories.Find(id);
			var categoryFromDbFirst = _db.GetFirstOrDefault(u=>u.Id==id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
			if (categoryFromDbFirst == null)
			{
				return NotFound();
			}
			return View(categoryFromDbFirst);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult DeletePOST(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

            //var field = _db.Categories.Find(obj.Id);
            var categoryFromDbFirst = _db.GetFirstOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }

            _db.Remove(categoryFromDbFirst);
            _db.Save();

			TempData["Success"] = "Category Deleted Successfully!";

			return RedirectToAction("Index");
        }
	}
}