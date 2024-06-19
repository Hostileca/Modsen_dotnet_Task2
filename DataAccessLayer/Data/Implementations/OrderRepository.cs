using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class OrderRepository : BaseRepository<Order>
    {
        public OrderRepository(AppDbContext context) : base(context)
        {
        }
    }
}
