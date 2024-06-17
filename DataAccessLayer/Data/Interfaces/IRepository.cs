using System.Linq.Expressions;

namespace DataAccessLayer.Data.Interfaces
{
    internal interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        TEntity GetByPredicate(Expression<Func<TEntity, bool>> predicate);
        Task Add(TEntity item);
        Task Update(TEntity item);
        Task Delete(TEntity item);
        void SaveChanges();
    }
}
