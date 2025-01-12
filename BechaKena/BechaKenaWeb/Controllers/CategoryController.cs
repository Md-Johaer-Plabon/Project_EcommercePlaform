using BechaKena.Data.Data;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BechaKena.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Categories;
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
                _db.Categories.Add(obj);
                _db.SaveChanges();

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
			var categoryFromDb = _db.Categories.Find(id);
			//var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Edit(Category obj)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(obj);
				_db.SaveChanges();

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
			var categoryFromDb = _db.Categories.Find(id);
			//var categoryFromDbFirst = _db.Categories.FirstOrDefault(u=>u.Id==id);
			//var categoryFromDbSingle = _db.Categories.SingleOrDefault(u => u.Id == id);
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public IActionResult Delete(Category obj)
		{
			if (obj == null)
			{
				return NotFound();
			}

			var field = _db.Categories.Find(obj.Id);
            if (field == null)
            {
                return NotFound();
            }

            _db.Categories.Remove(field);
            _db.SaveChanges();

			TempData["Success"] = "Category Deleted Successfully!";

			return RedirectToAction("Index");
        }
	}
}