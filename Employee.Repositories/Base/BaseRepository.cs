using Employees.Data;
using Employees.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq.Dynamic.Core;

namespace Employees.Repositories.Base
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        private readonly AppDbContext _context;
        private readonly DbSet<TEntity> _dbSet;
        public BaseRepository(AppDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }
        public IQueryable<TEntity> Get()
        {
            return _dbSet.AsQueryable();
        }

        public IQueryable<TEntity> Get(Func<TEntity, bool> predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }
        public IQueryable<TEntity> Get(string predicate)
        {
            return _dbSet.Where(predicate).AsQueryable();
        }

        public async Task<TEntity> FindById(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<TEntity> Create(TEntity item)
        {
            var entity = await _dbSet.AddAsync(item);
            return entity.Entity;
        }
        public TEntity Update(TEntity item)
        {
            return _dbSet.Update(item).Entity;
        }
        public void Remove(TEntity item)
        {
            _dbSet.Remove(item);
        }

        public async Task Remove(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                _dbSet.Update(entity);
            }
        }

        public async Task Commit()
        {
            await _context.SaveChangesAsync();
        }
    }
}
