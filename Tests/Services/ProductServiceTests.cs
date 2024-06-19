using BusinessLogicLayer.Dtos.Products;

namespace Tests.Services
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductService> _mockProductService;

        public ProductServiceTests()
        {
            _mockProductService = new Mock<IProductService>();
        }

        [Fact]
        public async Task CreateProductAsync_ValidProductCreateDto_CreateProductAndReturnDto()
        {
            var productCreateDto = new ProductCreateDto
            {
                Name = "TestProduct",
                Description = "Test Description",
                Price = 99.99f,
                CategoryId = Guid.NewGuid()
            };

            var productReadDto = new ProductReadDto
            {
                Name = productCreateDto.Name,
                Description = productCreateDto.Description,
                Price = productCreateDto.Price,
                CategoryId = productCreateDto.CategoryId,
                Id = Guid.NewGuid()
            };

            _mockProductService.Setup(s => s.CreateProductAsync(productCreateDto))
                               .ReturnsAsync(productReadDto);

            var result = await _mockProductService.Object.CreateProductAsync(productCreateDto);

            Assert.NotNull(result);
            Assert.Equal(productReadDto, result);
        }


        [Fact]
        public async Task DeleteProductByIdAsync_ExistingProductId_DeleteProductAndReturnDto()
        {
            var productId = Guid.NewGuid();
            var productReadDto = new ProductReadDto
            {
                Id = productId,
                Name = "TestProduct",
                Description = "Test Description",
                Price = 99.99f,
                CategoryId = Guid.NewGuid()
            };

            _mockProductService.Setup(s => s.DeleteProductByIdAsync(productId))
                               .ReturnsAsync(productReadDto);

            var result = await _mockProductService.Object.DeleteProductByIdAsync(productId);

            Assert.NotNull(result);
            Assert.Equal(productReadDto, result);
        }

        [Fact]
        public async Task GetAllProductsAsync_ReturnsAllProducts()
        {
            var products = new List<ProductReadDto>
            {
                new ProductReadDto { Id = Guid.NewGuid(), Name = "Product1", Description = "Desc1", Price = 10.0f, CategoryId = Guid.NewGuid() },
                new ProductReadDto { Id = Guid.NewGuid(), Name = "Product2", Description = "Desc2", Price = 20.0f, CategoryId = Guid.NewGuid() }
            };

            _mockProductService.Setup(s => s.GetAllProductsAsync())
                               .ReturnsAsync(products);

            var result = await _mockProductService.Object.GetAllProductsAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(products[0], item),
                item => Assert.Equal(products[1], item));
        }

        [Fact]
        public async Task GetProductByIdAsync_ExistingProductId_ReturnsProduct()
        {
            var productId = Guid.NewGuid();
            var productReadDto = new ProductReadDto
            {
                Id = productId,
                Name = "TestProduct",
                Description = "Test Description",
                Price = 99.99f,
                CategoryId = Guid.NewGuid()
            };

            _mockProductService.Setup(s => s.GetProductByIdAsync(productId))
                               .ReturnsAsync(productReadDto);

            var result = await _mockProductService.Object.GetProductByIdAsync(productId);

            Assert.NotNull(result);
            Assert.Equal(productReadDto, result);
        }

        [Fact]
        public async Task UpdateProductAsync_ValidProductUpdateDto_UpdateProductAndReturnDto()
        {
            var productUpdateDto = new ProductUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedProduct",
                Description = "Updated Description",
                Price = 199.99f,
                CategoryId = Guid.NewGuid()
            };

            var updatedProductReadDto = new ProductReadDto
            {
                Id = productUpdateDto.Id,
                Name = productUpdateDto.Name,
                Description = productUpdateDto.Description,
                Price = productUpdateDto.Price,
                CategoryId = productUpdateDto.CategoryId
            };

            _mockProductService.Setup(s => s.UpdateProductAsync(productUpdateDto))
                               .ReturnsAsync(updatedProductReadDto);

            var result = await _mockProductService.Object.UpdateProductAsync(productUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(updatedProductReadDto, result);
        }

        [Fact]
        public async Task CreateProductAsync_NullProductCreateDto_ThrowsArgumentNullException()
        {
            ProductCreateDto productCreateDto = null;

            _mockProductService.Setup(s => s.CreateProductAsync(null))
                               .ThrowsAsync(new ArgumentNullException());

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockProductService.Object.CreateProductAsync(productCreateDto));
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        }

        [Fact]
        public async Task CreateProductAsync_NonExistingCategoryId_ThrowsKeyNotFoundException()
        {
            var productCreateDto = new ProductCreateDto
            {
                Name = "TestProduct",
                Description = "Test Description",
                Price = 99.99f,
                CategoryId = Guid.NewGuid()
            };

            _mockProductService.Setup(s => s.CreateProductAsync(productCreateDto))
                               .ThrowsAsync(new KeyNotFoundException());

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockProductService.Object.CreateProductAsync(productCreateDto));

            Assert.IsType<KeyNotFoundException>(exception);
        }
    }
}
