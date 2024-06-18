using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Data.Implementations
{
    public class CategoryRepository : IRepository<Category>
    {
        private readonly AppDbContext _context;

        public CategoryRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Category>> GetAllAsync()
        {
            return await _context.Categories
                .Include(c => c.Products)
                .ToListAsync();
        }

        public async Task<Category> GetByPredicateAsync(Expression<Func<Category, bool>> predicate)
        {
            return await _context.Categories
                .Include(c => c.Products)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Category item)
        {
            await _context.Categories.AddAsync(item);
        }

        public void Delete(Category item)
        {
            _context.Categories.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
