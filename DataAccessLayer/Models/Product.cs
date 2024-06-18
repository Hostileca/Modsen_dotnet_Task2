namespace DataAccessLayer.Models
{
    public class Product
    {
        public Guid Guid { get; set; }
        public Guid CategoryGuid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
    }
}
