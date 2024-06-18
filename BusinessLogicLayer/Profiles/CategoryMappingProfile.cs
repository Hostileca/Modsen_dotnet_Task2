using AutoMapper;
using BusinessLogicLayer.Dtos.Categories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class CategoryMappingProfile : Profile
    {
        public CategoryMappingProfile()
        {
            CreateMap<CategoryCreateDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
        }
    }
}
