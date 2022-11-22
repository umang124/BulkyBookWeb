
using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;

namespace BulkyBook.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _dbContext;
        public ICategoryRepository Category { get; private set; }

        public ICoverTypeRepository CoverType { get; private set; }

        public IProductRepository Product { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            Category = new CategoryRepository(_dbContext);
            CoverType = new CoverTypeRepository(_dbContext);
            Product = new ProductRepository(_dbContext);
        }


        public void Save()
        {
            _dbContext.SaveChanges();
        }
    }
}
