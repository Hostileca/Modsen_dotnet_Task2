using BusinessLogicLayer.Dtos.Categories;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<CategoryDetailedReadDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryDetailedReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryDetailedReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<CategoryDetailedReadDto> DeleteCategoryByIdAsync(Guid id);
    }
}
