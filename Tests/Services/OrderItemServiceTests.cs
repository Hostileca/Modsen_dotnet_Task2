using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Dtos.Products;

namespace Tests.Services
{
    public class OrderItemServiceTests
    {
        private readonly Mock<IOrderItemService> _mockOrderItemService;

        public OrderItemServiceTests()
        {
            _mockOrderItemService = new Mock<IOrderItemService>();
        }

        [Fact]
        public async Task CreateOrderItemAsync_ValidOrderItemCreateDto_CreateOrderItemAndReturnDto()
        {
            var orderItemCreateDto = new OrderItemCreateDto
            {
                Amount = 2,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            var orderItemReadDto = new OrderItemReadDto
            {
                Amount = 2,
                Product = new ProductReadDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Product",
                    Description = "Test Description",
                    Price = 10.99f,
                    CategoryId = Guid.NewGuid()
                }
            };

            _mockOrderItemService.Setup(s => s.CreateOrderItemAsync(orderItemCreateDto))
                            .ReturnsAsync(orderItemReadDto);

            var result = await _mockOrderItemService.Object.CreateOrderItemAsync(orderItemCreateDto);

            Assert.NotNull(result);
            Assert.Equal(orderItemReadDto, result);
        }

        [Fact]
        public async Task DeleteOrderItemByIdAsync_ExistingOrderItemId_DeleteOrderItemAndReturnDto()
        {
            var orderItemId = Guid.NewGuid();
            var orderItemReadDto = new OrderItemReadDto
            {
                Amount = 1,
                Product = new ProductReadDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Product",
                    Description = "Test Description",
                    Price = 10.99f,
                    CategoryId = Guid.NewGuid()
                }
            };

            _mockOrderItemService.Setup(s => s.DeleteOrderItemByIdAsync(orderItemId))
                            .ReturnsAsync(orderItemReadDto);

            var result = await _mockOrderItemService.Object.DeleteOrderItemByIdAsync(orderItemId);

            Assert.NotNull(result);
            Assert.Equal(orderItemReadDto, result);
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_ReturnsAllOrderItems()
        {
            var orderItemReadDtos = new List<OrderItemReadDto>
            {
                new OrderItemReadDto
                {
                    Amount = 1,
                    Product = new ProductReadDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test Product 1",
                        Description = "Test Description 1",
                        Price = 10.99f,
                        CategoryId = Guid.NewGuid()
                    }
                },
                new OrderItemReadDto
                {
                    Amount = 2,
                    Product = new ProductReadDto
                    {
                        Id = Guid.NewGuid(),
                        Name = "Test Product 2",
                        Description = "Test Description 2",
                        Price = 20.99f,
                        CategoryId = Guid.NewGuid()
                    }
                }
            };

            _mockOrderItemService.Setup(s => s.GetAllOrderItemsAsync())
                            .ReturnsAsync(orderItemReadDtos);

            var result = await _mockOrderItemService.Object.GetAllOrderItemsAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(orderItemReadDtos[0], item),
                item => Assert.Equal(orderItemReadDtos[1], item));
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_ExistingOrderItemId_ReturnsOrderItem()
        {
            var orderItemId = Guid.NewGuid();
            var orderItemReadDto = new OrderItemReadDto
            {
                Amount = 1,
                Product = new ProductReadDto
                {
                    Id = Guid.NewGuid(),
                    Name = "Test Product",
                    Description = "Test Description",
                    Price = 10.99f,
                    CategoryId = Guid.NewGuid()
                }
            };

            _mockOrderItemService.Setup(s => s.GetOrderItemByIdAsync(orderItemId))
                            .ReturnsAsync(orderItemReadDto);

            var result = await _mockOrderItemService.Object.GetOrderItemByIdAsync(orderItemId);

            Assert.NotNull(result);
            Assert.Equal(orderItemReadDto, result);
        }

        [Fact]
        public async Task CreateOrderItemAsync_NullOrderItemCreateDto_ThrowsArgumentNullException()
        {
            OrderItemCreateDto orderItemCreateDto = null;

            _mockOrderItemService.Setup(s => s.CreateOrderItemAsync(null))
                            .ThrowsAsync(new ArgumentNullException());

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockOrderItemService.Object.CreateOrderItemAsync(orderItemCreateDto));
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        }

        [Fact]
        public async Task CreateOrderItemAsync_NonExistingProductId_ThrowsKeyNotFoundException()
        {
            var orderItemCreateDto = new OrderItemCreateDto
            {
                Amount = 2,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            _mockOrderItemService.Setup(s => s.CreateOrderItemAsync(orderItemCreateDto))
                            .ThrowsAsync(new KeyNotFoundException($"Product not found with id: {orderItemCreateDto.ProductId}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockOrderItemService.Object.CreateOrderItemAsync(orderItemCreateDto));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task CreateOrderItemAsync_NonExistingOrderId_ThrowsKeyNotFoundException()
        {
            var orderItemCreateDto = new OrderItemCreateDto
            {
                Amount = 2,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            _mockOrderItemService.Setup(s => s.CreateOrderItemAsync(orderItemCreateDto))
                            .ThrowsAsync(new KeyNotFoundException($"Order not found with id: {orderItemCreateDto.OrderId}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockOrderItemService.Object.CreateOrderItemAsync(orderItemCreateDto));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task DeleteOrderItemByIdAsync_NonExistingOrderItemId_ThrowsKeyNotFoundException()
        {
            var orderItemId = Guid.NewGuid();

            _mockOrderItemService.Setup(s => s.DeleteOrderItemByIdAsync(orderItemId))
                            .ThrowsAsync(new KeyNotFoundException($"Order item not found with id: {orderItemId}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockOrderItemService.Object.DeleteOrderItemByIdAsync(orderItemId));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_NonExistingOrderItemId_ThrowsKeyNotFoundException()
        {
            var orderItemId = Guid.NewGuid();

            _mockOrderItemService.Setup(s => s.GetOrderItemByIdAsync(orderItemId))
                            .ThrowsAsync(new KeyNotFoundException($"Order item not found with id: {orderItemId}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockOrderItemService.Object.GetOrderItemByIdAsync(orderItemId));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }
    }
}