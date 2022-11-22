using BulkyBook.DataAccess.Data;
using BulkyBook.DataAccess.Repository.IRepository;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace BulkyBook.DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;
        internal DbSet<T> _dbSet;
        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }
        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public bool CheckIfDataExists(Expression<Func<T, bool>> filter)
        {
            return _dbSet.Any(filter);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet.ToList();
        }
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter)
        {
            return _dbSet.FirstOrDefault(filter);
        }
        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entity)
        {
            _dbSet.RemoveRange(entity);
        }
    }
}
