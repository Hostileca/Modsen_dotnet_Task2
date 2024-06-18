using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Categories
{
    public class CategoryReadDto
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
