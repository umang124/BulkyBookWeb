using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class CoverTypeController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.CoverType.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType coverType)
        {
            if (_unitOfWork.CoverType.CheckIfDataExists(x => x.Name == coverType.Name))
            {
                ModelState.AddModelError("name", "CoverType already exists!");
                return View();
            }
            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Add(coverType);
                _unitOfWork.Save();
                TempData["success"] = "CoverType added successfully";
                return RedirectToAction("Index", "CoverType");
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
            var getcoverType = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (getcoverType == null)
            {
                return NotFound();
            }
            return View(getcoverType);
        }

        [HttpPost]
        public IActionResult Edit(CoverType coverType)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.CoverType.Update(coverType);
                _unitOfWork.Save();
                return RedirectToAction("Index", "CoverType");
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
            var getcoverType = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == id);

            if (getcoverType == null)
            {
                return NotFound();
            }
            return View(getcoverType);
        }

        [HttpPost]
        public IActionResult Delete(CoverType coverType)
        {
            var getcoverType = _unitOfWork.CoverType.GetFirstOrDefault(x => x.Id == coverType.Id);
            if (getcoverType == null)
            {
                return NotFound();
            }
            _unitOfWork.CoverType.Remove(getcoverType);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
