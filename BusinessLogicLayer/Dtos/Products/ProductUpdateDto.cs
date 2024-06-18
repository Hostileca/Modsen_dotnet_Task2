using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Products
{
    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public Category Category { get; set; }
    }
}
