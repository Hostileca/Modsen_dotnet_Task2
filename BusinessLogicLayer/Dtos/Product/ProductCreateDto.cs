using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Product
{
    public class ProductCreateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
    }
}
