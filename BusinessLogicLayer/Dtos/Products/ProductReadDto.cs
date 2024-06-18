using BusinessLogicLayer.Dtos.Categories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Products
{
    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public float Price { get; set; }
        public CategoryReadDto Category { get; set; }
    }
}
