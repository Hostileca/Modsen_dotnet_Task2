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
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            var category = _mapper.Map<Category>(categoryCreateDto);
            await _categoryRepository.AddAsync(category);
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryReadDto>(category);
        }

        public Task<CategoryReadDto> DeleteCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public Task<CategoryReadDto> GetCategoryByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryReadDto> GetCategoryByPredicateAsync(Expression<Func<CategoryReadDto, bool>> predicate)
        {
            throw new NotImplementedException();
        }

        public Task<CategoryReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            throw new NotImplementedException();
        }
    }
}
