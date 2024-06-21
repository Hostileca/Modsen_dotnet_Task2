using BusinessLogicLayer.Dtos.Categories;

namespace Tests.Profiles
{
    public class CategoryMappingProfileTests
    {
        private readonly IMapper _mapper;

        public CategoryMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new CategoryMappingProfile());
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Mapping_Configuration_IsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void CategoryCreateDto_To_Category_Mapping()
        {
            var categoryCreateDto = new CategoryCreateDto
            {
                Name = "Test Category"
            };

            var category = _mapper.Map<Category>(categoryCreateDto);

            category.Should().BeEquivalentTo(categoryCreateDto, options => options
                .ExcludingMissingMembers());
        }

        [Fact]
        public void CategoryUpdateDto_To_Category_Mapping()
        {
            var categoryUpdateDto = new CategoryUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "Updated Category Name"
            };

            var category = _mapper.Map<Category>(categoryUpdateDto);

            category.Should().BeEquivalentTo(categoryUpdateDto, options => options
                .ExcludingMissingMembers());
        }

        [Fact]
        public void Category_To_CategoryReadDto_Mapping()
        {
            var category = new Category
            {
                Id = Guid.NewGuid(),
                Name = "Test Category"
            };

            var categoryReadDto = _mapper.Map<CategoryReadDto>(category);

            categoryReadDto.Should().BeEquivalentTo(category, options => options
                .ExcludingMissingMembers());
        }
    }
}