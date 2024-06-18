namespace DataAccessLayer.Models
{
    public class Category
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
