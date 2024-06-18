using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Product
{
    public class ProductReadDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
    }
}
