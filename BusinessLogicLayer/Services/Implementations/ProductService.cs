using AutoMapper;
using BusinessLogicLayer.Dtos.Products;
using BusinessLogicLayer.Services.Algorithms;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository, ICategoryRepository categoryRepository, IMapper mapper)
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

            existingProduct.Category = existingCategory;

            var newProduct = _mapper.Map(productUpdateDto, existingProduct);
            await _productRepository.SaveChangesAsync();
            return _mapper.Map<ProductReadDto>(newProduct);
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsByFilter(ProductQuery productQuery)
        {
            Expression<Func<Product, bool>> predicate = product => true;

            if (!string.IsNullOrEmpty(productQuery.Name))
            {
                predicate = predicate.And(product => product.Name.Contains(productQuery.Name));
            }

            if (!string.IsNullOrEmpty(productQuery.Description))
            {
                predicate = predicate.And(product => product.Description.Contains(productQuery.Description));
            }

            if (productQuery.MaxPrice > 0)
            {
                predicate = predicate.And(product => product.Price < productQuery.MaxPrice);
            }

            if (productQuery.MinPrice < 0)
            {
                predicate = predicate.And(product => product.Price > productQuery.MinPrice);
            }

            if (productQuery.CategoryId != Guid.Empty)
            {
                predicate = predicate.And(product => product.CategoryId == productQuery.CategoryId);
            }

            var products = await _productRepository.GetByPredicateAsync(predicate);

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
    }
}
