using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    public class ProductController : Controller
    {
        public readonly IUnitOfWork _unitOfWork;
        ApplicationDbContext _dbContext;
        public ProductController(IUnitOfWork unitOfWork, ApplicationDbContext dbContext)
        {
            _unitOfWork = unitOfWork;
            _dbContext = dbContext;
        }
        public IActionResult Index()
        {
            return View(_unitOfWork.Product.GetAll());
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]


        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            IEnumerable<SelectListItem> CategoryList = _dbContext.Categories.Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            IEnumerable<SelectListItem> CoverTypeList = _unitOfWork.CoverType.GetAll().Select(
                u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString()
                });
            if (id == null || id == 0)
            {
                // create product
                ViewBag.CategoryList = CategoryList;
                ViewBag.CoverTypeList = CoverTypeList;
                return View();
            }
            else
            {
                // update product
                var getProduct = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

                if (getProduct == null)
                {
                    return NotFound();
                }
                return View(getProduct);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Product product, IFormFile file)
        {

            if (ModelState.IsValid)
            {
                _unitOfWork.Product.Update(product);
                _unitOfWork.Save();
                return RedirectToAction("Index", "Product");
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
            var getProduct = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == id);

            if (getProduct == null)
            {
                return NotFound();
            }
            return View(getProduct);
        }

        [HttpPost]
        public IActionResult Delete(Product product)
        {
            var getProduct = _unitOfWork.Product.GetFirstOrDefault(x => x.Id == product.Id);
            if (getProduct == null)
            {
                return NotFound();
            }
            _unitOfWork.Product.Remove(getProduct);
            _unitOfWork.Save();
            return RedirectToAction("Index");
        }
    }
}
