using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using Microsoft.AspNetCore.Mvc;

namespace BechaKena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _db;
        public ProductController(IRepositoryWrapper db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            IEnumerable<CoverType> objCoverTypeList = _db.CoverType.GetAll();
            return View(objCoverTypeList);
        }
        //GET
        public IActionResult Create()
        {
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _db.CoverType.Add(obj);
                _db.Save();
                TempData["success"] = "CoverType created successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        //GET
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFrom_dbFirst = _db.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFrom_dbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFrom_dbFirst);
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {

            if (ModelState.IsValid)
            {
                _db.CoverType.Update(obj);
                _db.Save();
                TempData["success"] = "CoverType updated successfully";
                return RedirectToAction("Index");
            }
            return View(obj);
        }
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var CoverTypeFrom_dbFirst = _db.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (CoverTypeFrom_dbFirst == null)
            {
                return NotFound();
            }
            return View(CoverTypeFrom_dbFirst);
        }
        //POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePOST(int? id)
        {
            var obj = _db.CoverType.GetFirstOrDefault(u => u.Id == id);
            if (obj == null)
            {
                return NotFound();
            }
            _db.CoverType.Remove(obj);
            _db.Save();
            TempData["success"] = "CoverType deleted successfully";
            return RedirectToAction("Index");

        }
    }
}
