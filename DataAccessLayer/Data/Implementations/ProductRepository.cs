using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class ProductRepository : BaseRepository<Product>
    {
        public ProductRepository(AppDbContext context) : base(context)
        {
        }
    }
}
