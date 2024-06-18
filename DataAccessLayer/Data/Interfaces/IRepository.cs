using System.Linq.Expressions;

namespace DataAccessLayer.Data.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAllAsync();
        Task<TEntity> GetByPredicateAsync(Expression<Func<TEntity, bool>> predicate);
        Task AddAsync(TEntity item);
        void Delete(TEntity item);
        Task SaveChangesAsync();
    }
}
