using BusinessLogicLayer.Dtos.Categories;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync();
        Task<CategoryReadDto> GetCategoryByIdAsync(Guid id);
        Task<CategoryReadDto> GetCategoryByPredicateAsync(Expression<Func<CategoryReadDto, bool>> predicate);
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto);
        Task<CategoryReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto);
        Task<CategoryReadDto> DeleteCategoryByIdAsync(Guid id);
    }
}
