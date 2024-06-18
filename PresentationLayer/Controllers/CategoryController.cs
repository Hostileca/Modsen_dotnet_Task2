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
        public async Task<IActionResult> GetAllProducts()
        {
            var products = await _categoryService.GetAllCategoriesAsync();
            return Ok(products);
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct(CategoryCreateDto categoryCreateDto)
        {
            var product = await _categoryService.CreateCategoryAsync(categoryCreateDto);
            return Ok(product);
        }
    }
}
