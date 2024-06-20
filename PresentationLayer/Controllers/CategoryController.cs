using BusinessLogicLayer.Dtos.Categories;
using BusinessLogicLayer.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Route("api/v1/categories")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var categories = await _categoryService.GetAllCategoriesAsync();
            return Ok(categories);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var category = await _categoryService.GetCategoryByIdAsync(id);
            return Ok(category);
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory(CategoryCreateDto categoryCreateDto)
        {
            var category = await _categoryService.CreateCategoryAsync(categoryCreateDto);
            return Ok(category);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateCategory(Guid id, CategoryUpdateDto categoryUpdateDto)
        {
            categoryUpdateDto.Id = id;
            var category = await _categoryService.UpdateCategoryAsync(categoryUpdateDto);
            return Ok(category);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var deletedCategory = await _categoryService.DeleteCategoryByIdAsync(id);
            return Ok(deletedCategory);
        }
    }
}
