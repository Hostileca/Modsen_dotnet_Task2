using BusinessLogicLayer.Dtos.Products;

namespace BusinessLogicLayer.Dtos.Categories
{
    public class CategoryReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<ProductReadDto> Products { get; set; }
    }
}
