using BusinessLogicLayer.Dtos.Products;
using BusinessLogicLayer.Dtos.Categories;
using System.Linq.Expressions;

namespace Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<ICategoryRepository> _mockCategoryRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IProductService _productService;

        public ProductServiceTests()
        {
            _mockProductRepository = new Mock<IProductRepository>();
            _mockCategoryRepository = new Mock<ICategoryRepository>();
            _mockMapper = new Mock<IMapper>();
            _productService = new ProductService(_mockProductRepository.Object, _mockCategoryRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateProductAsync_ValidDto_ReturnsProductDetailedReadDto()
        {
            var createDto = new ProductCreateDto { Name = "Test Product", Description = "Test Description", Price = 10.5f, CategoryId = Guid.NewGuid() };
            var product = new Product { Id = Guid.NewGuid(), Name = createDto.Name, Description = createDto.Description, Price = createDto.Price, CategoryId = createDto.CategoryId };
            var expectedReadDto = new ProductDetailedReadDto { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, Category = new CategoryReadDto { Id = createDto.CategoryId, Name = "Test Category" } };

            _mockMapper.Setup(m => m.Map<Product>(createDto)).Returns(product);
            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(createDto.CategoryId)).ReturnsAsync(new Category { Id = createDto.CategoryId, Name = "Test Category" });
            _mockProductRepository.Setup(repo => repo.AddAsync(product)).Returns(Task.CompletedTask);
            _mockProductRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<ProductDetailedReadDto>(product)).Returns(expectedReadDto);

            var result = await _productService.CreateProductAsync(createDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(product.Id);
            result.Name.Should().Be(createDto.Name);
            result.Category.Should().NotBeNull();
            result.Category.Id.Should().Be(createDto.CategoryId);
        }

        [Fact]
        public async Task DeleteProductByIdAsync_ExistingId_ReturnsProductDetailedReadDto()
        {
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product", Description = "Test Description", Price = 10.5f, CategoryId = Guid.NewGuid() };
            var expectedReadDto = new ProductDetailedReadDto { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, Category = new CategoryReadDto { Id = product.CategoryId, Name = "Test Category" } };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockProductRepository.Setup(repo => repo.Delete(product));
            _mockProductRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<ProductDetailedReadDto>(product)).Returns(expectedReadDto);

            var result = await _productService.DeleteProductByIdAsync(productId);

            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Name.Should().Be("Test Product");
        }

        [Fact]
        public async Task GetAllProductsAsync_NoConditions_ReturnsListOfProductReadDto()
        {
            var products = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 10.5f, CategoryId = Guid.NewGuid() },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 15.2f, CategoryId = Guid.NewGuid() }
            };
            var expectedReadDtos = new List<ProductReadDto>
            {
                new ProductReadDto { Id = products[0].Id, Name = products[0].Name, Price = products[0].Price },
                new ProductReadDto { Id = products[1].Id, Name = products[1].Name, Price = products[1].Price }
            };

            _mockProductRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(products);
            _mockMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(products)).Returns(expectedReadDtos);

            var result = await _productService.GetAllProductsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().ContainItemsAssignableTo<ProductReadDto>();
        }

        [Fact]
        public async Task GetProductByIdAsync_ExistingId_ReturnsProductDetailedReadDto()
        {
            var productId = Guid.NewGuid();
            var product = new Product { Id = productId, Name = "Test Product", Description = "Test Description", Price = 10.5f, CategoryId = Guid.NewGuid() };
            var expectedReadDto = new ProductDetailedReadDto { Id = product.Id, Name = product.Name, Description = product.Description, Price = product.Price, Category = new CategoryReadDto { Id = product.CategoryId, Name = "Test Category" } };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(product);
            _mockMapper.Setup(m => m.Map<ProductDetailedReadDto>(product)).Returns(expectedReadDto);

            var result = await _productService.GetProductByIdAsync(productId);

            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Name.Should().Be("Test Product");
        }

        [Fact]
        public async Task UpdateProductAsync_ValidDto_ReturnsProductDetailedReadDto()
        {
            var productId = Guid.NewGuid();
            var existingProduct = new Product { Id = productId, Name = "Existing Product", Description = "Existing Description", Price = 20.5f, CategoryId = Guid.NewGuid() };
            var updatedDto = new ProductUpdateDto { Id = productId, Name = "Updated Product", Description = "Updated Description", Price = 25.3f, CategoryId = Guid.NewGuid() };
            var updatedProduct = new Product { Id = productId, Name = updatedDto.Name, Description = updatedDto.Description, Price = updatedDto.Price, CategoryId = updatedDto.CategoryId };
            var expectedReadDto = new ProductDetailedReadDto { Id = updatedProduct.Id, Name = updatedProduct.Name, Description = updatedProduct.Description, Price = updatedProduct.Price, Category = new CategoryReadDto { Id = updatedDto.CategoryId, Name = "Updated Category" } };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync(existingProduct);
            _mockCategoryRepository.Setup(repo => repo.GetByIdAsync(updatedDto.CategoryId)).ReturnsAsync(new Category { Id = updatedDto.CategoryId, Name = "Updated Category" });
            _mockMapper.Setup(m => m.Map(updatedDto, existingProduct)).Returns(updatedProduct);
            _mockProductRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<ProductDetailedReadDto>(updatedProduct)).Returns(expectedReadDto);

            var result = await _productService.UpdateProductAsync(updatedDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(productId);
            result.Name.Should().Be("Updated Product");
        }

        [Fact]
        public async Task CreateProductAsync_NullDto_ThrowsArgumentNullException()
        {
            Func<Task> action = async () => await _productService.CreateProductAsync(null);

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteProductByIdAsync_NonExistingId_ThrowsNotFoundException()
        {
            var productId = Guid.NewGuid();
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            Func<Task> action = async () => await _productService.DeleteProductByIdAsync(productId);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetProductByIdAsync_NonExistingId_ThrowsNotFoundException()
        {
            var productId = Guid.NewGuid();
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(productId)).ReturnsAsync((Product)null);

            Func<Task> action = async () => await _productService.GetProductByIdAsync(productId);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateProductAsync_NullDto_ThrowsArgumentNullException()
        {
            Func<Task> action = async () => await _productService.UpdateProductAsync(null);

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task GetProductsByFilter_FilterByCriteria_ReturnsFilteredProductList()
        {
            var productQuery = new ProductQuery
            {
                Name = "Product",
                Description = "Description",
                MaxPrice = 100,
                MinPrice = 10,
                CategoryId = Guid.NewGuid()
            };

            var filteredProducts = new List<Product>
            {
                new Product { Id = Guid.NewGuid(), Name = "Product 1", Description = "Description 1", Price = 50, CategoryId = productQuery.CategoryId },
                new Product { Id = Guid.NewGuid(), Name = "Product 2", Description = "Description 2", Price = 80, CategoryId = productQuery.CategoryId }
            };

            _mockProductRepository.Setup(repo => repo.GetByPredicateAsync(It.IsAny<Expression<Func<Product, bool>>>()))
                .ReturnsAsync(filteredProducts);

            var expectedFilteredDtos = new List<ProductReadDto>
            {
                new ProductReadDto { Id = filteredProducts[0].Id, Name = filteredProducts[0].Name, Price = filteredProducts[0].Price },
                new ProductReadDto { Id = filteredProducts[1].Id, Name = filteredProducts[1].Name, Price = filteredProducts[1].Price }
            };

            _mockMapper.Setup(m => m.Map<IEnumerable<ProductReadDto>>(filteredProducts))
                .Returns(expectedFilteredDtos);

            var result = await _productService.GetProductsByFilter(productQuery);

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().ContainItemsAssignableTo<ProductReadDto>();
        }
    }
}