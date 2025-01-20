using BechaKena.Data.Repository.Interface;
using BechaKena.Model.Models;
using BechaKena.Model.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Build.Tasks.Deployment.Bootstrapper;

namespace BechaKena.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IRepositoryWrapper _db;
		private readonly IWebHostEnvironment _hostEnvironment;
		public ProductController(IRepositoryWrapper db, IWebHostEnvironment hostEnvironment)
        {
            _db = db;
			_hostEnvironment = hostEnvironment;
		}
        public IActionResult Index()
        {
            return View();
        }
		//GET
        public IActionResult Upsert(int? id)
        {
			ProductViewModel viewModel = new()
			{
				Product = new(),
				CategoryList = _db.Category.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				}),
				CoverTypeList = _db.CoverType.GetAll().Select(i => new SelectListItem
				{
					Text = i.Name,
					Value = i.Id.ToString()
				}),
			};

			if (id == null || id == 0)
			{
				//create product
				//ViewBag.CategoryList = CategoryList;
				//ViewData["CoverTypeList"] = CoverTypeList;
				return View(viewModel);
			}
			else
			{
				//update product
			}


			return View(viewModel);
		}
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(ProductViewModel obj, IFormFile? file)
        {

            if (ModelState.IsValid)
            {
				string wwwRootPath = _hostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString();
					var uploads = Path.Combine(wwwRootPath, @"images\products");
					var extension = Path.GetExtension(file.FileName);
					using (var fileStreams = new FileStream(Path.Combine(uploads, fileName + extension), FileMode.Create))
					{
						file.CopyTo(fileStreams);
					}
					obj.Product.ImageUrl = @"\images\products\" + fileName + extension;
				}
				_db.Product.Add(obj.Product);
				_db.Save();
				TempData["success"] = "Product created successfully";
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

		#region API CALLS
		[HttpGet]
		public IActionResult GetAll()
		{
			var productList = _db.Product.GetAll(includeProperties: "Category,CoverType");
			JsonResult res =  Json(new { data = productList });

            return res;
		}
		#endregion
	}
}
