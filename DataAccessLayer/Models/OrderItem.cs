namespace DataAccessLayer.Models
{
    public class OrderItem
    {
        public Guid Id { get; set; }
        public Guid OrderId { get; set; }
        public Guid ProductId { get; set; }
        public int Amount { get; set; }
        public Order Order { get; set; }
        public Product Product { get; set; }
    }
}
