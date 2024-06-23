using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Dtos.Products;

namespace Tests.Services
{
    public class OrderItemServiceTests
    {
        private readonly Mock<IRepository<OrderItem>> _mockOrderItemRepository;
        private readonly Mock<IRepository<Product>> _mockProductRepository;
        private readonly Mock<IRepository<Order>> _mockOrderRepository;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IOrderItemService _orderItemService;

        public OrderItemServiceTests()
        {
            _mockOrderItemRepository = new Mock<IRepository<OrderItem>>();
            _mockProductRepository = new Mock<IRepository<Product>>();
            _mockOrderRepository = new Mock<IRepository<Order>>();
            _mockMapper = new Mock<IMapper>();
            _orderItemService = new OrderItemService(
                _mockOrderItemRepository.Object,
                _mockProductRepository.Object,
                _mockOrderRepository.Object,
                _mockMapper.Object);
        }

        [Fact]
        public async Task CreateOrderItemAsync_ValidDto_ReturnsOrderItemReadDto()
        {
            var createDto = new OrderItemCreateDto
            {
                Amount = 5,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            var product = new Product { Id = createDto.ProductId, Name = "Test Product" };
            var order = new Order { Id = createDto.OrderId };

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                Amount = createDto.Amount,
                OrderId = createDto.OrderId,
                ProductId = createDto.ProductId,
                Product = product,
                Order = order
            };

            var expectedReadDto = new OrderItemReadDto
            {
                Amount = orderItem.Amount,
                Product = new ProductReadDto { Id = product.Id, Name = product.Name }
            };

            _mockMapper.Setup(m => m.Map<OrderItem>(createDto)).Returns(orderItem);
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(createDto.ProductId)).ReturnsAsync(product);
            _mockOrderRepository.Setup(repo => repo.GetByIdAsync(createDto.OrderId)).ReturnsAsync(order);
            _mockOrderItemRepository.Setup(repo => repo.AddAsync(orderItem)).Returns(Task.CompletedTask);
            _mockOrderItemRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(orderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.CreateOrderItemAsync(createDto);

            result.Should().NotBeNull();
            result.Amount.Should().Be(createDto.Amount);
            result.Product.Should().NotBeNull();
            result.Product.Id.Should().Be(product.Id);
            result.Product.Name.Should().Be(product.Name);
        }

        [Fact]
        public async Task DeleteOrderItemByIdAsync_ExistingId_ReturnsOrderItemReadDto()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem { Id = orderId, Amount = 5, ProductId = Guid.NewGuid(), OrderId = orderId };
            var expectedReadDto = new OrderItemReadDto { Amount = orderItem.Amount };

            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(orderItem);
            _mockOrderItemRepository.Setup(repo => repo.SaveChangesAsync()).Returns(Task.CompletedTask);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(orderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.DeleteOrderItemByIdAsync(orderId);

            result.Should().NotBeNull();
            result.Amount.Should().Be(5);
        }

        [Fact]
        public async Task GetAllOrderItemsAsync_NoConditions_ReturnsListOfOrderItemReadDto()
        {
            var orderItems = new List<OrderItem>
            {
                new OrderItem { Id = Guid.NewGuid(), Amount = 5, ProductId = Guid.NewGuid(), OrderId = Guid.NewGuid() },
                new OrderItem { Id = Guid.NewGuid(), Amount = 10, ProductId = Guid.NewGuid(), OrderId = Guid.NewGuid() }
            };

            var expectedReadDtos = new List<OrderItemReadDto>
            {
                new OrderItemReadDto { Amount = orderItems[0].Amount },
                new OrderItemReadDto { Amount = orderItems[1].Amount }
            };

            _mockOrderItemRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(orderItems);
            _mockMapper.Setup(m => m.Map<IEnumerable<OrderItemReadDto>>(orderItems)).Returns(expectedReadDtos);

            var result = await _orderItemService.GetAllOrderItemsAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.Should().BeEquivalentTo(expectedReadDtos);
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_ExistingId_ReturnsOrderItemReadDto()
        {
            var orderId = Guid.NewGuid();
            var orderItem = new OrderItem { Id = orderId, Amount = 5, ProductId = Guid.NewGuid(), OrderId = orderId };
            var expectedReadDto = new OrderItemReadDto { Amount = orderItem.Amount };

            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync(orderItem);
            _mockMapper.Setup(m => m.Map<OrderItemReadDto>(orderItem)).Returns(expectedReadDto);

            var result = await _orderItemService.GetOrderItemByIdAsync(orderId);

            result.Should().NotBeNull();
            result.Amount.Should().Be(5);
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
            var orderId = Guid.NewGuid();
            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync((OrderItem)null);

            Func<Task> action = async () => await _orderItemService.DeleteOrderItemByIdAsync(orderId);

            await action.Should().ThrowAsync<KeyNotFoundException>();
        }

        [Fact]
        public async Task GetOrderItemByIdAsync_NonExistingId_ThrowsKeyNotFoundException()
        {
            var orderId = Guid.NewGuid();
            _mockOrderItemRepository.Setup(repo => repo.GetByIdAsync(orderId)).ReturnsAsync((OrderItem)null);

            Func<Task> action = async () => await _orderItemService.GetOrderItemByIdAsync(orderId);

            await action.Should().ThrowAsync<KeyNotFoundException>();
        }
    }
}
