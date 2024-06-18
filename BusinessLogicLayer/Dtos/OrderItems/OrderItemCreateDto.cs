using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.OrderItems
{
    public class OrderItemCreateDto
    {
        public int Amount { get; set; }
        public Guid ProductId { get; set; }
    }
}
