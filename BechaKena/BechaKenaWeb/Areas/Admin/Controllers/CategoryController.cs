using BechaKena.Data.Data;
using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Cryptography;

namespace BechaKena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IRepositoryWrapper _db;
        public CategoryController(IRepositoryWrapper db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Category> objCategoryList = _db.Category.GetAll();
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
                _db.Category.Add(obj);
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
            var categoryFromDbFirst = _db.Category.GetFirstOrDefault(u => u.Id == id);
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
                _db.Category.Update(obj);
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
            //var categoryFromDb = _db.Category.Categories.Find(id);
            var categoryFromDbFirst = _db.Category.GetFirstOrDefault(u => u.Id == id);
            //var categoryFromDbSingle = _db.Category.Categories.SingleOrDefault(u => u.Id == id);
            if (categoryFromDbFirst == null)
            {
                return NotFound();
            }
            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(Category obj)
        {
            if (obj == null)
            {
                return NotFound();
            }

            var field = _db.Category.GetFirstOrDefault(u => u.Id == obj.Id);
            if (field == null)
            {
                return NotFound();
            }

            _db.Category.Remove(field);
            _db.Save();

            TempData["Success"] = "Category Deleted Successfully!";

            return RedirectToAction("Index");
        }
    }
}