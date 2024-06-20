using BusinessLogicLayer.Dtos.Products;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProductsAsync();
        Task<ProductReadDto> GetProductByIdAsync(Guid id);
        public Task<IEnumerable<ProductReadDto>> GetProductsByFilter(ProductQuery productQuery);
        Task<ProductReadDto> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ProductReadDto> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<ProductReadDto> DeleteProductByIdAsync(Guid id);
    }
}
