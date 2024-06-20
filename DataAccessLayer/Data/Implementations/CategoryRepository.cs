using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class CategoryRepository : BaseRepository<Category>
    {
        public CategoryRepository(AppDbContext context) : base(context)
        {
        }
    }
}
