using DataAccessLayer.Models;

namespace DataAccessLayer.Data.Implementations
{
    public class OrderItemRepository : BaseRepository<OrderItem>
    {
        public OrderItemRepository(AppDbContext context) : base(context)
        {
        }
    }
}
