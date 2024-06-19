using BusinessLogicLayer.Dtos.Categories;

namespace Tests.Profiles
{
    public class CategoryMappingProfileTests
    {
        private IMapper _mapper;

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

            Assert.Equal(categoryCreateDto.Name, category.Name);
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
            
            Assert.Equal(categoryUpdateDto.Id, category.Id);
            Assert.Equal(categoryUpdateDto.Name, category.Name);
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

            Assert.Equal(category.Id, categoryReadDto.Id);
            Assert.Equal(category.Name, categoryReadDto.Name);
        }
    }
}