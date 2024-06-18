using BusinessLogicLayer.Dtos.Products;

namespace BusinessLogicLayer.Dtos.Categories
{
    public class CategoryCreateDto
    {
        public string Name { get; set; }
        public ICollection<ProductCreateDto> Products { get; set; }
    }
}
