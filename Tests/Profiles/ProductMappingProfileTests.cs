using BusinessLogicLayer.Dtos.Products;

namespace Tests.Profiles
{
    public class ProductMappingProfileTests
    {
        private readonly IMapper _mapper;

        public ProductMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
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
        public void ProductCreateDto_To_Product_Mapping()
        {
            var productCreateDto = new ProductCreateDto
            {
                Name = "Test Product",
                Description = "Test Description",
                Price = 100.0f,
                CategoryId = Guid.NewGuid()
            };

            var product = _mapper.Map<Product>(productCreateDto);

            product.Should().BeEquivalentTo(productCreateDto, options => options
                .ExcludingMissingMembers());
        }

        [Fact]
        public void ProductUpdateDto_To_Product_Mapping()
        {
            var productUpdateDto = new ProductUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Updated Product",
                Description = "Updated Description",
                Price = 150.0f,
                CategoryId = Guid.NewGuid()
            };

            var product = _mapper.Map<Product>(productUpdateDto);

            product.Should().BeEquivalentTo(productUpdateDto, options => options
                .ExcludingMissingMembers());
        }

        [Fact]
        public void Product_To_ProductReadDto_Mapping()
        {
            var product = new Product
            {
                Id = Guid.NewGuid(),
                Name = "Test Product",
                Description = "Test Description",
                Price = 100.0f,
                CategoryId = Guid.NewGuid()
            };

            var productReadDto = _mapper.Map<ProductReadDto>(product);

            productReadDto.Should().BeEquivalentTo(product, options => options
                .ExcludingMissingMembers());
        }
    }
}