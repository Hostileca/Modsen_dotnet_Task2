using BusinessLogicLayer.Dtos.OrderItems;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Orders
{
    public class OrderCreateDto
    {
        public ICollection<OrderItemCreateDto> OrderItems { get; set; }
    }
}
