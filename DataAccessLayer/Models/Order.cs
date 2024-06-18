namespace DataAccessLayer.Models
{
    public class Order
    {
        public Guid Guid { get; set; }
        public Guid UserGuid { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
