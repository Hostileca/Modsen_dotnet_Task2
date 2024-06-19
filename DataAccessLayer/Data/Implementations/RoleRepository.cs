using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class RoleRepository : BaseRepository<Role>
    {
        public RoleRepository(AppDbContext context) : base(context)
        {
        }
    }
}
