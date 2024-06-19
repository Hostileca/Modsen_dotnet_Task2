using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class UserRepository : BaseRepository<User>
    {
        public UserRepository(AppDbContext context) : base(context)
        {
        }
    }
}
