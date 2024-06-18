using AutoMapper;
using BusinessLogicLayer.Dtos.Products;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IRepository<Product> productRepository, IRepository<Category> categoryRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            var product = _mapper.Map<Product>(productCreateDto);
            var existingCategory = await _categoryRepository.GetByIdAsync(productCreateDto.CategoryId);
            if (existingCategory is null)
            {
                throw new KeyNotFoundException($"Category not found with id:{productCreateDto.CategoryId}");
            }
            product.Category = existingCategory;

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> DeleteProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{id}");
            }
            _productRepository.Delete(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync()
        {
            var products = await _productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductReadDto> GetProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product is null)
            {
                throw new KeyNotFoundException($"Product not found with id:{id}");
            }
            return _mapper.Map<ProductReadDto>(product);
        }

        public Task<ProductReadDto> GetProductByPredicateAsync(Expression<Func<ProductReadDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<ProductReadDto> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
