using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BulkyBook.DataAccess.Repository
{
    public class ProductRepository : Repository<Product>, IProductRepository
    {
        private readonly ApplicationDbContext _dbContext;
        public ProductRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }
        public void Update(Product product)
        {
            var getProduct = _dbContext.Products.FirstOrDefault(x => x.Id == product.Id);
            if (getProduct != null)
            {
                getProduct.Title = product.Title;
                getProduct.ISBN = product.Title;
                getProduct.Price = product.Price;
                getProduct.Price50 = product.Price50;
                getProduct.ListPrice = product.ListPrice;
                getProduct.Price100 = product.Price100;
                getProduct.Description = product.Description;
                getProduct.CategoryId = product.CategoryId;
                getProduct.Author = product.Author;
                getProduct.CoverTypeId = product.CoverTypeId;
                if (product.ImageUrl != null)
                {
                    getProduct.ImageUrl = product.ImageUrl;
                }
            }
        }
    }
}
