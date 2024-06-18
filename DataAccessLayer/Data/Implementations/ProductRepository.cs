using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Data.Implementations
{
    public class ProductRepository : IRepository<Product>
    {
        private readonly AppDbContext _context;

        public ProductRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _context.Products
                .Include(c => c.Category)
                .ToListAsync();
        }

        public async Task<Product> GetByPredicateAsync(Expression<Func<Product, bool>> predicate)
        {
            return await _context.Products
                .Include(c => c.Category)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Product item)
        {
            await _context.Products.AddAsync(item);
        }

        public void Delete(Product item)
        {
            _context.Products.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
