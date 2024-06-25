using AutoMapper;
using BusinessLogicLayer.Dtos.Products;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Algorithms;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Implementations
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public ProductService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<ProductDetailedReadDto> CreateProductAsync(ProductCreateDto productCreateDto)
        {
            if (productCreateDto == null)
                throw new ArgumentNullException(nameof(productCreateDto));

            var productRepository = _unitOfWork.GetRepository<Product>();
            var categoryRepository = _unitOfWork.GetRepository<Category>();

            var product = _mapper.Map<Product>(productCreateDto);

            var existingCategory = await categoryRepository.GetByIdAsync(productCreateDto.CategoryId);
            if (existingCategory == null)
                throw new NotFoundException($"Category not found with id: {productCreateDto.CategoryId}");

            product.Category = existingCategory;

            await productRepository.AddAsync(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDetailedReadDto>(product);
        }

        public async Task<ProductDetailedReadDto> DeleteProductByIdAsync(Guid id)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();

            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product not found with id: {id}");

            productRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDetailedReadDto>(product);
        }

        public async Task<IEnumerable<ProductReadDto>> GetAllProductsAsync()
        {
            var productRepository = _unitOfWork.GetRepository<Product>();

            var products = await productRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }

        public async Task<ProductDetailedReadDto> GetProductByIdAsync(Guid id)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();

            var product = await productRepository.GetByIdAsync(id);
            if (product == null)
                throw new NotFoundException($"Product not found with id: {id}");

            return _mapper.Map<ProductDetailedReadDto>(product);
        }

        public async Task<ProductDetailedReadDto> UpdateProductAsync(ProductUpdateDto productUpdateDto)
        {
            if (productUpdateDto == null)
                throw new ArgumentNullException(nameof(productUpdateDto));

            var productRepository = _unitOfWork.GetRepository<Product>();
            var categoryRepository = _unitOfWork.GetRepository<Category>();

            var existingProduct = await productRepository.GetByIdAsync(productUpdateDto.Id);
            if (existingProduct == null)
                throw new NotFoundException($"Product not found with id: {productUpdateDto.Id}");

            var existingCategory = await categoryRepository.GetByIdAsync(productUpdateDto.CategoryId);
            if (existingCategory == null)
                throw new NotFoundException($"Category not found with id: {productUpdateDto.CategoryId}");

            existingProduct.Category = existingCategory;

            var updatedProduct = _mapper.Map(productUpdateDto, existingProduct);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<ProductDetailedReadDto>(updatedProduct);
        }

        public async Task<IEnumerable<ProductReadDto>> GetProductsByFilter(ProductQuery productQuery)
        {
            var productRepository = _unitOfWork.GetRepository<Product>();

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

            if (productQuery.MinPrice > 0)
            {
                predicate = predicate.And(product => product.Price > productQuery.MinPrice);
            }

            if (productQuery.CategoryId != Guid.Empty)
            {
                predicate = predicate.And(product => product.CategoryId == productQuery.CategoryId);
            }

            var products = await productRepository.GetByPredicateAsync(predicate);

            return _mapper.Map<IEnumerable<ProductReadDto>>(products);
        }
    }
}