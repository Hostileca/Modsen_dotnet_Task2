using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.OrderItems
{
    public class OrderItemReadDto
    {
        public Guid OrderGuid { get; set; }
        public Guid ProductGuid { get; set; }
        public int Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
