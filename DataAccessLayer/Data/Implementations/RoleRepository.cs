using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataAccessLayer.Data.Implementations
{
    internal class RoleRepository : IRepository<Role>
    {
        private readonly AppDbContext _context;

        public RoleRepository(AppDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<IEnumerable<Role>> GetAllAsync()
        {
            return await _context.Roles
                .Include(c => c.Users)
                .ToListAsync();
        }

        public async Task<Role> GetByPredicateAsync(Expression<Func<Role, bool>> predicate)
        {
            return await _context.Roles
                .Include(c => c.Users)
                .FirstOrDefaultAsync(predicate);
        }

        public async Task AddAsync(Role item)
        {
            await _context.Roles.AddAsync(item);
        }

        public void Delete(Role item)
        {
            _context.Roles.Remove(item);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
