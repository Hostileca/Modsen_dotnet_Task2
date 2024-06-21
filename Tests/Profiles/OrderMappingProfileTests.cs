using BusinessLogicLayer.Dtos.OrderItems;
using BusinessLogicLayer.Dtos.Orders;

namespace Tests.Profiles
{
    public class OrderMappingProfileTests
    {
        private readonly IMapper _mapper;

        public OrderMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderMappingProfile());
                cfg.AddProfile(new OrderItemMappingProfile());
                cfg.AddProfile(new ProductMappingProfile());
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Mapping_Configuration_IsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void OrderCreateDto_To_Order_Mapping()
        {
            var orderItemCreateDto = new OrderItemCreateDto
            {
                Amount = 5,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            var orderCreateDto = new OrderCreateDto
            {
                OrderItems = new List<OrderItemCreateDto> { orderItemCreateDto }
            };

            var order = _mapper.Map<Order>(orderCreateDto);

            order.OrderItems.Should().HaveCount(1);
            order.OrderItems.First().Should().BeEquivalentTo(orderItemCreateDto, options => options
                .ExcludingMissingMembers());
        }

        [Fact]
        public void Order_To_OrderReadDto_Mapping()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Price = 10.0f
            };

            var orderItem = new OrderItem
            {
                Id = Guid.NewGuid(),
                OrderId = Guid.NewGuid(),
                ProductId = product.Id,
                Amount = 5,
                Product = product
            };

            var order = new Order
            {
                Id = Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                OrderItems = new List<OrderItem> { orderItem }
            };

            var orderReadDto = _mapper.Map<OrderReadDto>(order);

            orderReadDto.Should().BeEquivalentTo(order, options => options
                .ExcludingMissingMembers()
                .Excluding(dto => dto.OrderItems));

            orderReadDto.OrderItems.Should().HaveCount(1);
            orderReadDto.OrderItems.First().Should().BeEquivalentTo(orderItem, options => options
                .ExcludingMissingMembers()
                .Excluding(dto => dto.Product));

            orderReadDto.OrderItems.First().Product.Should().BeEquivalentTo(product, options => options
                .ExcludingMissingMembers());
        }
    }
}