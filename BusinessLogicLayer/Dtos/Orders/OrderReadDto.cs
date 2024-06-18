using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Orders
{
    public class OrderReadDto
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
