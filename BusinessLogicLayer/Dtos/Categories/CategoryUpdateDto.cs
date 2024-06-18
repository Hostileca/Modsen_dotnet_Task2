using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Categories
{
    public class CategoryUpdateDto
    {
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
