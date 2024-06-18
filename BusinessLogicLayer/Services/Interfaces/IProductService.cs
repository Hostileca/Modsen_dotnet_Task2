using BusinessLogicLayer.Dtos.Products;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDto>> GetAllProductsAsync();
        Task<ProductReadDto> GetProductByIdAsync(Guid id);
        Task<ProductReadDto> GetProductByPredicateAsync(Expression<Func<ProductReadDto, bool>> predicate);
        Task<ProductReadDto> CreateProductAsync(ProductCreateDto productCreateDto);
        Task<ProductReadDto> UpdateProductAsync(ProductUpdateDto productUpdateDto);
        Task<ProductReadDto> DeleteProductByIdAsync(Guid id);
    }
}
