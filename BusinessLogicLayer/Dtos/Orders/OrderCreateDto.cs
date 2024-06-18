using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Orders
{
    public class OrderCreateDto
    {
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
