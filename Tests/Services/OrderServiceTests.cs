//using BusinessLogicLayer.Dtos.OrderItems;
//using BusinessLogicLayer.Dtos.Orders;
//using BusinessLogicLayer.Dtos.Products;

//namespace Tests.Services
//{
//    public class OrderServiceTests
//    {
//        private readonly Mock<IOrderService> _mockOrderService;

//        public OrderServiceTests()
//        {
//            _mockOrderService = new Mock<IOrderService>();
//        }

//        [Fact]
//        public async Task CreateOrderAsync_ValidOrderCreateDto_CreateOrderAndReturnDto()
//        {
//            var orderCreateDto = new OrderCreateDto
//            {
//                OrderItems = new List<OrderItemCreateDto>
//                {
//                    new OrderItemCreateDto
//                    {
//                        Amount = 2,
//                        OrderId = Guid.NewGuid(),
//                        ProductId = Guid.NewGuid()
//                    }
//                }
//            };

//            var orderReadDto = new OrderReadDto
//            {
//                Id = Guid.NewGuid(),
//                OrderItems = new List<OrderItemReadDto>
//                {
//                    new OrderItemReadDto
//                    {
//                        Amount = 2,
//                        Product = new ProductReadDto
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "Test Product",
//                            Description = "Test Description",
//                            Price = 10.99f,
//                            CategoryId = Guid.NewGuid()
//                        }
//                    }
//                }
//            };

//            _mockOrderService.Setup(s => s.CreateOrderAsync(orderCreateDto))
//                            .ReturnsAsync(orderReadDto);

//            var result = await _mockOrderService.Object.CreateOrderAsync(orderCreateDto);

//            Assert.NotNull(result);
//            Assert.Equal(orderReadDto, result);
//        }

//        [Fact]
//        public async Task DeleteOrderByIdAsync_ExistingOrderId_DeleteOrderAndReturnDto()
//        {
//            var orderId = Guid.NewGuid();
//            var orderReadDto = new OrderReadDto
//            {
//                Id = orderId,
//                OrderItems = new List<OrderItemReadDto>
//                {
//                    new OrderItemReadDto
//                    {
//                        Amount = 1,
//                        Product = new ProductReadDto
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "Test Product",
//                            Description = "Test Description",
//                            Price = 10.99f,
//                            CategoryId = Guid.NewGuid()
//                        }
//                    }
//                }
//            };

//            _mockOrderService.Setup(s => s.DeleteOrderByIdAsync(orderId))
//                            .ReturnsAsync(orderReadDto);

//            var result = await _mockOrderService.Object.DeleteOrderByIdAsync(orderId);

//            Assert.NotNull(result);
//            Assert.Equal(orderReadDto, result);
//        }

//        [Fact]
//        public async Task GetAllOrdersAsync_ReturnsAllOrders()
//        {
//            var orderReadDtos = new List<OrderReadDto>
//            {
//                new OrderReadDto
//                {
//                    Id = Guid.NewGuid(),
//                    OrderItems = new List<OrderItemReadDto>
//                    {
//                        new OrderItemReadDto
//                        {
//                            Amount = 1,
//                            Product = new ProductReadDto
//                            {
//                                Id = Guid.NewGuid(),
//                                Name = "Test Product 1",
//                                Description = "Test Description 1",
//                                Price = 10.99f,
//                                CategoryId = Guid.NewGuid()
//                            }
//                        }
//                    }
//                },
//                new OrderReadDto
//                {
//                    Id = Guid.NewGuid(),
//                    OrderItems = new List<OrderItemReadDto>
//                    {
//                        new OrderItemReadDto
//                        {
//                            Amount = 2,
//                            Product = new ProductReadDto
//                            {
//                                Id = Guid.NewGuid(),
//                                Name = "Test Product 2",
//                                Description = "Test Description 2",
//                                Price = 20.99f,
//                                CategoryId = Guid.NewGuid()
//                            }
//                        }
//                    }
//                }
//            };

//            _mockOrderService.Setup(s => s.GetAllOrdersAsync())
//                            .ReturnsAsync(orderReadDtos);

//            var result = await _mockOrderService.Object.GetAllOrdersAsync();

//            Assert.NotNull(result);
//            Assert.Collection(result,
//                item => Assert.Equal(orderReadDtos[0], item),
//                item => Assert.Equal(orderReadDtos[1], item));
//        }

//        [Fact]
//        public async Task GetOrderByIdAsync_ExistingOrderId_ReturnsOrder()
//        {
//            var orderId = Guid.NewGuid();
//            var orderReadDto = new OrderReadDto
//            {
//                Id = orderId,
//                OrderItems = new List<OrderItemReadDto>
//                {
//                    new OrderItemReadDto
//                    {
//                        Amount = 1,
//                        Product = new ProductReadDto
//                        {
//                            Id = Guid.NewGuid(),
//                            Name = "Test Product",
//                            Description = "Test Description",
//                            Price = 10.99f,
//                            CategoryId = Guid.NewGuid()
//                        }
//                    }
//                }
//            };

//            _mockOrderService.Setup(s => s.GetOrderByIdAsync(orderId))
//                            .ReturnsAsync(orderReadDto);

//            var result = await _mockOrderService.Object.GetOrderByIdAsync(orderId);

//            Assert.NotNull(result);
//            Assert.Equal(orderReadDto, result);
//        }

//        [Fact]
//        public async Task CreateOrderAsync_NullOrderCreateDto_ThrowsArgumentNullException()
//        {
//            OrderCreateDto orderCreateDto = null;

//            _mockOrderService.Setup(s => s.CreateOrderAsync(null))
//                            .ThrowsAsync(new ArgumentNullException());

//            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockOrderService.Object.CreateOrderAsync(orderCreateDto));
//            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
//        }

//        [Fact]
//        public async Task DeleteOrderByIdAsync_NonExistingOrderId_ThrowsKeyNotFoundException()
//        {
//            var orderId = Guid.NewGuid();

//            _mockOrderService.Setup(s => s.DeleteOrderByIdAsync(orderId))
//                            .ThrowsAsync(new KeyNotFoundException($"Order not found with id: {orderId}"));

//            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockOrderService.Object.DeleteOrderByIdAsync(orderId));
//            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
//        }

//        [Fact]
//        public async Task GetOrderByIdAsync_NonExistingOrderId_ThrowsKeyNotFoundException()
//        {
//            var orderId = Guid.NewGuid();

//            _mockOrderService.Setup(s => s.GetOrderByIdAsync(orderId))
//                            .ThrowsAsync(new KeyNotFoundException($"Order not found with id: {orderId}"));

//            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockOrderService.Object.GetOrderByIdAsync(orderId));
//            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
//        }
//    }
//}