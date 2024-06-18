using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Orders
{
    public class OrderUpdateDto
    {
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
