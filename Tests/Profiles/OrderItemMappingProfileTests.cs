using BusinessLogicLayer.Dtos.OrderItems;

namespace Tests.Profiles
{
    public class OrderItemMappingProfileTests
    {
        private IMapper _mapper;

        public OrderItemMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
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
        public void OrderItemCreateDto_To_OrderItem_Mapping()
        {
            var orderItemCreateDto = new OrderItemCreateDto
            {
                Amount = 5,
                OrderId = Guid.NewGuid(),
                ProductId = Guid.NewGuid()
            };

            var orderItem = _mapper.Map<OrderItem>(orderItemCreateDto);

            Assert.Equal(orderItemCreateDto.Amount, orderItem.Amount);
            Assert.Equal(orderItemCreateDto.OrderId, orderItem.OrderId);
            Assert.Equal(orderItemCreateDto.ProductId, orderItem.ProductId);
        }

        [Fact]
        public void OrderItem_To_OrderItemReadDto_Mapping()
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

            var orderItemReadDto = _mapper.Map<OrderItemReadDto>(orderItem);

            Assert.Equal(orderItem.Amount, orderItemReadDto.Amount);
            Assert.NotNull(orderItemReadDto.Product);
            Assert.Equal(orderItem.Product.Name, orderItemReadDto.Product.Name);
            Assert.Equal(orderItem.Product.Price, orderItemReadDto.Product.Price);
        }
    }
}