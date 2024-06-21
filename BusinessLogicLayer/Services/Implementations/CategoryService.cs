using AutoMapper;
using BusinessLogicLayer.Dtos.Categories;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CategoryDetailedReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
                throw new ArgumentNullException(nameof(categoryCreateDto));

            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> DeleteCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category not found with id: {id}");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryDetailedReadDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category not found with id: {id}");

            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null)
                throw new ArgumentNullException(nameof(categoryUpdateDto));

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
            if (existingCategory == null)
                throw new NotFoundException($"Category not found with id: {categoryUpdateDto.Id}");

            var newCategory = _mapper.Map(categoryUpdateDto, existingCategory);
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDetailedReadDto>(newCategory);
        }
    }
}
