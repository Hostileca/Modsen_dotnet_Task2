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
            _productRepository = productRepository ?? throw new ArgumentNullException(nameof(productRepository));
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductReadDto> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
                throw new ArgumentNullException(nameof(productCreateDto));

            var product = _mapper.Map<Product>(productCreateDto);
            
            var existingCategory = await _categoryRepository.GetByIdAsync(productCreateDto.CategoryId);
            if (existingCategory == null)
                throw new KeyNotFoundException($"Category not found with id: {productCreateDto.CategoryId}");
            
            product.Category = existingCategory;

            await _productRepository.AddAsync(product);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> DeleteProductByIdAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
                throw new KeyNotFoundException($"Product not found with id: {id}");
            
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
            if (product == null)
                throw new KeyNotFoundException($"Product not found with id: {id}");
            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> GetProductByPredicateAsync(Expression<Func<ProductReadDto, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var productPredicate = _mapper.Map<Expression<Func<Product, bool>>>(predicate);
            var product = await _productRepository.GetByPredicateAsync(productPredicate);
            if (product == null)
                throw new KeyNotFoundException("Product not found.");

            return _mapper.Map<ProductReadDto>(product);
        }

        public async Task<ProductReadDto> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
                throw new ArgumentNullException(nameof(productUpdateDto));

            var existingProduct = await _productRepository.GetByIdAsync(productUpdateDto.Id);
            if (existingProduct == null)
                throw new KeyNotFoundException($"Product not found with id: {productUpdateDto.Id}");

            var existingCategory = await _categoryRepository.GetByIdAsync(productUpdateDto.CategoryId);
            if (existingCategory == null)
                throw new KeyNotFoundException($"Category not found with id: {productUpdateDto.CategoryId}");

            existingProduct.Name = productUpdateDto.Name;
            existingProduct.Description = productUpdateDto.Description;
            existingProduct.Price = productUpdateDto.Price;
            existingProduct.Category = existingCategory;

            await _productRepository.SaveChangesAsync();

            return _mapper.Map<ProductReadDto>(existingProduct);
        }
    }
}
