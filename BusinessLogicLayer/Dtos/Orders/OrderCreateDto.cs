using BusinessLogicLayer.Dtos.OrderItems;

namespace BusinessLogicLayer.Dtos.Orders
{
    public class OrderCreateDto
    {
        public ICollection<OrderItemCreateDto> OrderItems { get; set; }
    }
}
