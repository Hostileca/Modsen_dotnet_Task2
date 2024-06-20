using BusinessLogicLayer.Dtos.Products;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProductsAsync();
        Task<ProductDetailedReadDto> GetProductByIdAsync(Guid id);
        Task<IEnumerable<ProductReadDto>> GetProductsByFilter(ProductQuery productQuery);
        Task<ProductDetailedReadDto> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ProductDetailedReadDto> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<ProductDetailedReadDto> DeleteProductByIdAsync(Guid id);
    }
}
