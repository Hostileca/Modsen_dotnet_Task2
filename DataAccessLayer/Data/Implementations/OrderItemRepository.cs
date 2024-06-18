using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Data.Implementations
{
    public class OrderItemRepository : IRepository<OrderItem>
    {
        private readonly AppDbContext _context;

        public OrderItemRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<OrderItem>> GetAllAsync()
        {
            return await _context.OrderItems
                .Include(c => c.Order)
                .Include(c => c.Product)
                .ToListAsync();
        }

        public async Task<OrderItem> GetByPredicateAsync(Expression<Func<OrderItem, bool>> predicate)
        {
            return await _context.OrderItems
                .Include(c => c.Order)
                .Include(c => c.Product)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(OrderItem item)
        {
            await _context.OrderItems.AddAsync(item);
        }

        public void Delete(OrderItem item)
        {
            _context.OrderItems.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
