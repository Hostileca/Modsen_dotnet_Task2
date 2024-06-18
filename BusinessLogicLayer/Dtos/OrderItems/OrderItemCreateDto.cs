using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.OrderItems
{
    public class OrderItemCreateDto
    {
        public int Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
