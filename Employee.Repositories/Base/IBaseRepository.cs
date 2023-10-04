using Employees.Data.Entities;

namespace Employees.Repositories.Base
{
    public interface IBaseRepository<TEntity> where TEntity : BaseEntity
    {
        Task<TEntity> Create(TEntity item);
        Task<TEntity> FindById(int id);
        IQueryable<TEntity> Get();
        IQueryable<TEntity> Get(Func<TEntity, bool> predicate);
        IQueryable<TEntity> Get(string predicate);
        void Remove(TEntity item);
        Task Remove(int id);
        TEntity Update(TEntity item);
        Task Commit();
    }
}
