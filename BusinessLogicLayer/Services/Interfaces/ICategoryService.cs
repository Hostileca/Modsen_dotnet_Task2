using BusinessLogicLayer.Dtos.Categories;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<CategoryReadDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<CategoryReadDto> DeleteCategoryByIdAsync(Guid id);
    }
}
