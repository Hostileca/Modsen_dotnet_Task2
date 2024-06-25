using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Dtos.Products;

namespace Tests.Services
{
    public class OrderItemServiceTests
    {
        private readonly Mock<IOrderItemRepository> _mockOrderItemRepository;
        private readonly Mock<IProductRepository> _mockProductRepository;
        private readonly Mock<IOrderRepository> _mockOrderRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IOrderItemService _orderItemService;

        public OrderItemServiceTests()
        {
            _mockOrderItemRepository = new Mock<IOrderItemRepository>();
            _mockProductRepository = new Mock<IProductRepository>();
            _mockOrderRepository = new Mock<IOrderRepository>();
            _mockMapper = new Mock<IMapper>();
            _orderItemService = new OrderItemService(_mockOrderItemRepository.Object, _mockProductRepository.Object, _mockOrderRepository.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task CreateOrderItemAsync_ValidDto_ReturnsOrderItemReadDto()
        {
            var createDto = new OrderItemCreateDto { Amount = 5, OrderId = Guid.NewGuid(), ProductId = Guid.NewGuid() };
            var orderItem = new OrderItem { Id = Guid.NewGuid(), Amount = createDto.Amount, OrderId = createDto.OrderId, ProductId = createDto.ProductId };
            var product = new Product { Id = createDto.ProductId, Name = "Test Product" };
            var order = new Order { Id = createDto.OrderId };
            var expectedReadDto = new OrderItemReadDto { Amount = orderItem.Amount, Product = new ProductReadDto { Id = product.Id, Name = product.Name } };

            _mockProductRepository.Setup(repo => repo.GetByIdAsync(createDto.ProductId)).ReturnsAsync(product);
            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(createDto.OrderId)).ReturnsAsync(order);
            _mockMapper.Setup(m => m.Map<OrderItem>(createDto)).Returns(orderItem);
            _mockOrderItemRepository.Setup(repo => repo.AddAsync(orderItem)).Returns(Task.CompletedTask);
            _mockOrderItemRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(orderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.CreateOrderItemAsync(createDto);

            result.Should().NotBeNull();
            result.Amount.Should().Be(createDto.Amount);
            result.Product.Id.Should().Be(createDto.ProductId);
        }

        [Fact]
        public async Task DeleteOrderItemByIdAsync_ExistingId_ReturnsOrderItemReadDto()
        {
            var orderItemId = Guid.NewGuid();
            var orderItem = new OrderItem { Id = orderItemId, Amount = 5, Product = new Product { Id = Guid.NewGuid(), Name = "Test Product" } };
            var expectedReadDto = new OrderItemReadDto { Amount = orderItem.Amount, Product = new ProductReadDto { Id = orderItem.Product.Id, Name = orderItem.Product.Name } };

            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderItemId)).ReturnsAsync(orderItem);
            _mockOrderItemRepository.Setup(repo => repo.Delete(orderItem));
            _mockOrderItemRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(orderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.DeleteOrderItemByIdAsync(orderItemId);

            result.Should().NotBeNull();
            result.Amount.Should().Be(orderItem.Amount);
            result.Product.Id.Should().Be(orderItem.Product.Id);
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_NoConditions_ReturnsListOfOrderItemReadDto()
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = Guid.NewGuid(), Amount = 5, Product = new Product { Id = Guid.NewGuid(), Name = "Product 1" } },
                new OrderItem { Id = Guid.NewGuid(), Amount = 10, Product = new Product { Id = Guid.NewGuid(), Name = "Product 2" } }
            };
            var expectedReadDtos = new List<OrderItemReadDto>
            {
                new OrderItemReadDto { Amount = orderItems[0].Amount, Product = new ProductReadDto { Id = orderItems[0].Product.Id, Name = orderItems[0].Product.Name } },
                new OrderItemReadDto { Amount = orderItems[1].Amount, Product = new ProductReadDto { Id = orderItems[1].Product.Id, Name = orderItems[1].Product.Name } }
            };

            _mockOrderItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderItems);
            _mockMapper.Setup(m => m.Map<IEnumerable<OrderItemReadDto>>(orderItems)).Returns(expectedReadDtos);

            var result = await _orderItemService.GetAllOrderItemsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().ContainItemsAssignableTo<OrderItemReadDto>();
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_ExistingId_ReturnsOrderItemReadDto()
        {
            var orderItemId = Guid.NewGuid();
            var orderItem = new OrderItem { Id = orderItemId, Amount = 5, Product = new Product { Id = Guid.NewGuid(), Name = "Test Product" } };
            var expectedReadDto = new OrderItemReadDto { Amount = orderItem.Amount, Product = new ProductReadDto { Id = orderItem.Product.Id, Name = orderItem.Product.Name } };

            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderItemId)).ReturnsAsync(orderItem);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(orderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.GetOrderItemByIdAsync(orderItemId);

            result.Should().NotBeNull();
            result.Amount.Should().Be(orderItem.Amount);
            result.Product.Id.Should().Be(orderItem.Product.Id);
        }

        [Fact]
        public async Task UpdateOrderItemAsync_ValidDto_ReturnsUpdatedOrderItemReadDto()
        {
            var orderItemId = Guid.NewGuid();
            var existingOrderItem = new OrderItem { Id = orderItemId, Amount = 5, Product = new Product { Id = Guid.NewGuid(), Name = "Test Product" } };
            var updateDto = new OrderItemUpdateDto { Id = orderItemId, Amount = 10 };
            var updatedOrderItem = new OrderItem { Id = orderItemId, Amount = updateDto.Amount, Product = existingOrderItem.Product };
            var expectedReadDto = new OrderItemReadDto { Amount = updatedOrderItem.Amount, Product = new ProductReadDto { Id = updatedOrderItem.Product.Id, Name = updatedOrderItem.Product.Name } };

            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderItemId)).ReturnsAsync(existingOrderItem);
            _mockMapper.Setup(m => m.Map(updateDto, existingOrderItem)).Returns(updatedOrderItem);
            _mockOrderItemRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(updatedOrderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.UpdateOrderItemAsync(updateDto);

            result.Should().NotBeNull();
            result.Amount.Should().Be(updateDto.Amount);
            result.Product.Id.Should().Be(existingOrderItem.Product.Id);
        }

        [Fact]
        public async Task CreateOrderItemAsync_NullDto_ThrowsArgumentNullException()
        {
            Func<Task> action = async () => await _orderItemService.CreateOrderItemAsync(null);

            await action.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task DeleteOrderItemByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            var orderItemId = Guid.NewGuid();
            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderItemId)).ReturnsAsync((OrderItem)null);

            Func<Task> action = async () => await _orderItemService.DeleteOrderItemByIdAsync(orderItemId);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            var orderItemId = Guid.NewGuid();
            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderItemId)).ReturnsAsync((OrderItem)null);

            Func<Task> action = async () => await _orderItemService.GetOrderItemByIdAsync(orderItemId);

            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task UpdateOrderItemAsync_NullDto_ThrowsArgumentNullException()
        {
            Func<Task> action = async () => await _orderItemService.UpdateOrderItemAsync(null);

            await action.Should().ThrowAsync<ArgumentNullException>();
        }
    }
}
