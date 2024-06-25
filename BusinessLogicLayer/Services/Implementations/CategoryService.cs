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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<CategoryDetailedReadDto> CreateCategoryAsync(CategoryCreateDto categoryCreateDto)
        {
            if (categoryCreateDto == null)
                throw new ArgumentNullException(nameof(categoryCreateDto));

            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var category = _mapper.Map<Category>(categoryCreateDto);
            await categoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> DeleteCategoryByIdAsync(Guid id)
        {
            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category not found with id: {id}");

            categoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync()
        {
            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var categories = await categoryRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryDetailedReadDto> GetCategoryByIdAsync(Guid id)
        {
            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var category = await categoryRepository.GetByIdAsync(id);
            if (category == null)
                throw new NotFoundException($"Category not found with id: {id}");

            return _mapper.Map<CategoryDetailedReadDto>(category);
        }

        public async Task<CategoryDetailedReadDto> UpdateCategoryAsync(CategoryUpdateDto categoryUpdateDto)
        {
            if (categoryUpdateDto == null)
                throw new ArgumentNullException(nameof(categoryUpdateDto));

            var categoryRepository = _unitOfWork.GetRepository<Category>();
            var existingCategory = await categoryRepository.GetByIdAsync(categoryUpdateDto.Id);
            if (existingCategory == null)
                throw new NotFoundException($"Category not found with id: {categoryUpdateDto.Id}");

            var updatedCategory = _mapper.Map(categoryUpdateDto, existingCategory);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<CategoryDetailedReadDto>(updatedCategory);
        }
    }
}