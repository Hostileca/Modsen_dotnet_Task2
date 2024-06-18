using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Data.Implementations
{
    public class OrderRepository : IRepository<Order>
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _context.Orders
                .Include(c => c.OrderItems)
                .ToListAsync();
        }

        public async Task<Order> GetByPredicateAsync(Expression<Func<Order, bool>> predicate)
        {
            return await _context.Orders
                .Include(c => c.OrderItems)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Order item)
        {
            await _context.Orders.AddAsync(item);
        }

        public void Delete(Order item)
        {
            _context.Orders.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
