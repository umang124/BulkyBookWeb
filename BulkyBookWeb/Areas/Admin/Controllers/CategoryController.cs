using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CategoryController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {

            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.Category.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category category)
        {
            if (_unitOfWork.Category.CheckIfDataExists(x => x.Name == category.Name))
            {
                ModelState.AddModelError("name", "Category already exists!");
                return View();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Add(category);
                _unitOfWork.Save();
                TempData["success"] = "Category added successfully";
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getCategory = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (getCategory == null)
            {
                return NotFound();
            }
            return View(getCategory);
        }

        [HttpPost]
        public IActionResult Edit(Category category)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Category.Update(category);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Category");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            var getCategory = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == id);

            if (getCategory == null)
            {
                return NotFound();
            }
            return View(getCategory);
        }

        [HttpPost]
        public IActionResult Delete(Category category)
        {
            var getCategory = _unitOfWork.Category.GetFirstOrDefault(x => x.Id == category.Id);
            if (getCategory == null)
            {
                return NotFound();
            }
            _unitOfWork.Category.Remove(getCategory);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
