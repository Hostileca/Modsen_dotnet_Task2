using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Data.Implementations
{
    public class UserRepository : IRepository<User>
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users
                .Include(c => c.Role)
                .Include(c => c.Orders)
                .ToListAsync();
        }

        public async Task<User> GetByPredicateAsync(Expression<Func<User, bool>> predicate)
        {
            return await _context.Users
                .Include(c => c.Role)
                .Include(c => c.Orders)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(User item)
        {
            await _context.Users.AddAsync(item);
        }

        public void Delete(User item)
        {
            _context.Users.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
