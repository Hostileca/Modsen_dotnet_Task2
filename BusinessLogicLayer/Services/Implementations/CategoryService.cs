using AutoMapper;
using BusinessLogicLayer.Dtos.Categories;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Implementations
{
    public class CategoryService : ICategoryService
    {
        private readonly IRepository<Category> _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(IRepository<Category> categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository ?? throw new ArgumentNullException(nameof(categoryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
                throw new ArgumentNullException(nameof(categoryCreateDto));

            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> DeleteCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category not found with id: {id}");

            _categoryRepository.Delete(category);
            await _categoryRepository.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(Guid id)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new KeyNotFoundException($"Category not found with id: {id}");

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> GetCategoryByPredicateAsync(Expression<Func<CategoryReadDto, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var categoryPredicate = _mapper.Map<Expression<Func<Category, bool>>>(predicate);
            var category = await _categoryRepository.GetByPredicateAsync(categoryPredicate);
            if (category == null)
                throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null)
                throw new ArgumentNullException(nameof(categoryUpdateDto));

            var existingCategory = await _categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
            if (existingCategory == null)
                throw new KeyNotFoundException($"Category not found with id: {categoryUpdateDto.Id}");

            var newCategory = _mapper.Map(categoryUpdateDto, existingCategory);
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(newCategory);
        }
    }
}
