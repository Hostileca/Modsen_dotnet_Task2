using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _dbContext;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(AppDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories == null)
                _repositories = new Dictionary<Type, object>();

            var entityType = typeof(TEntity);
            if (!_repositories.ContainsKey(entityType))
            {
                _repositories[entityType] = CreateRepository<TEntity>();
            }

            return (IRepository<TEntity>)_repositories[entityType];
        }

        private object CreateRepository<TEntity>() where TEntity : class
        {
            if (typeof(TEntity) == typeof(User))
            {
                return new UserRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(Role))
            {
                return new RoleRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(Product))
            {
                return new ProductRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(Category))
            {
                return new CategoryRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(Order))
            {
                return new OrderRepository(_dbContext);
            }
            else if (typeof(TEntity) == typeof(OrderItem))
            {
                return new OrderItemRepository(_dbContext);
            }
            else
            {
                throw new InvalidOperationException($"Repository for type {typeof(TEntity).Name} not found.");
            }
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            _dbContext.Dispose();
        }
    }
}