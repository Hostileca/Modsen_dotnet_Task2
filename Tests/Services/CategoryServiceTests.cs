using BusinessLogicLayer.Dtos.Categories;

namespace Tests.Services
{
    public class CategoryServiceTests
    {
        private readonly Mock<ICategoryService> _mockCategoryService;

        public CategoryServiceTests()
        {
            _mockCategoryService = new Mock<ICategoryService>();
        }

        [Fact]
        public async Task CreateCategoryAsync_ValidCategoryCreateDto_CreateCategoryAndReturnDto()
        {
            var categoryCreateDto = new CategoryCreateDto { Name = "TestCategory" };
            var categoryReadDto = new CategoryReadDto { Name = categoryCreateDto.Name };

            _mockCategoryService.Setup(s => s.CreateCategoryAsync(categoryCreateDto))
                                .ReturnsAsync(categoryReadDto);

            var result = await _mockCategoryService.Object.CreateCategoryAsync(categoryCreateDto);

            Assert.NotNull(result);
            Assert.Equal(categoryReadDto, result);
        }

        [Fact]
        public async Task DeleteCategoryByIdAsync_ExistingCategoryId_DeleteCategoryAndReturnDto()
        {
            var categoryId = Guid.NewGuid();
            var categoryReadDto = new CategoryReadDto { Id = categoryId, Name = "TestCategory" };

            _mockCategoryService.Setup(s => s.DeleteCategoryByIdAsync(categoryId))
                                .ReturnsAsync(categoryReadDto);

            var result = await _mockCategoryService.Object.DeleteCategoryByIdAsync(categoryId);

            Assert.NotNull(result);
            Assert.Equal(categoryReadDto, result);
        }

        [Fact]
        public async Task GetAllCategoriesAsync_ReturnsAllCategories()
        {
            var categories = new List<CategoryReadDto>
            {
                new CategoryReadDto { Id = Guid.NewGuid(), Name = "Category1" },
                new CategoryReadDto { Id = Guid.NewGuid(), Name = "Category2" }
            };

            _mockCategoryService.Setup(s => s.GetAllCategoriesAsync())
                                .ReturnsAsync(categories);

            var result = await _mockCategoryService.Object.GetAllCategoriesAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(categories[0], item),
                item => Assert.Equal(categories[1], item));
        }

        [Fact]
        public async Task GetCategoryByIdAsync_ExistingCategoryId_ReturnsCategory()
        {
            var categoryId = Guid.NewGuid();
            var categoryReadDto = new CategoryReadDto { Id = categoryId, Name = "TestCategory" };

            _mockCategoryService.Setup(s => s.GetCategoryByIdAsync(categoryId))
                                .ReturnsAsync(categoryReadDto);

            var result = await _mockCategoryService.Object.GetCategoryByIdAsync(categoryId);

            Assert.NotNull(result);
            Assert.Equal(categoryReadDto, result);
        }

        [Fact]
        public async Task UpdateCategoryAsync_ValidCategoryUpdateDto_UpdateCategoryAndReturnDto()
        {
            var categoryUpdateDto = new CategoryUpdateDto { Id = Guid.NewGuid(), Name = "UpdatedCategory" };
            var updatedCategoryReadDto = new CategoryReadDto { Id = categoryUpdateDto.Id, Name = categoryUpdateDto.Name };

            _mockCategoryService.Setup(s => s.UpdateCategoryAsync(categoryUpdateDto))
                                .ReturnsAsync(updatedCategoryReadDto);

            var result = await _mockCategoryService.Object.UpdateCategoryAsync(categoryUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(updatedCategoryReadDto, result);
        }


        [Fact]
        public async Task CreateCategoryAsync_NullCategoryCreateDto_ThrowsArgumentNullException()
        {
            CategoryCreateDto categoryCreateDto = null;

            _mockCategoryService.Setup(s => s.CreateCategoryAsync(null))
                               .ThrowsAsync(new ArgumentNullException());

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockCategoryService.Object.CreateCategoryAsync(categoryCreateDto));
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        }
    }
}
