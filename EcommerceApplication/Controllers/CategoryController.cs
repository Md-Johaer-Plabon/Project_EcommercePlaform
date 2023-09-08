using EcommerceApplication.Data;
using EcommerceApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace EcommerceApplication.Controllers
{
    public class CategoryController : Controller
    {
        public readonly ApplicationDBContext _db;
        public CategoryController(ApplicationDBContext db) { 
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objResult = _db.Categories.ToList();
            return View(objResult);
        }
        public IActionResult CategoryList()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CategoryList(Category obj)
        {
            if(obj.Order == 0)
            {
                ModelState.AddModelError("Order", "This field can't be empty");
            }
            else if(ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();  
        }

        public IActionResult Edit()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {
            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? Id)
        {
            if(Id == null)
            {
                return NotFound(); 
            }
            Category? obj = _db.Categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            return View(obj);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? Id)
        {
            if (Id == null)
            {
                return NotFound();
            }
            Category? obj = _db.Categories.Find(Id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.Categories.Remove(obj);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
