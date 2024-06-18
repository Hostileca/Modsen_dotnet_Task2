using AutoMapper;
using BusinessLogicLayer.Dtos.Categories;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    internal class CategoryMappingProfile : Profile
    {
        CategoryMappingProfile()
        {
            CreateMap<CategoryReadDto, Category>();
            CreateMap<CategoryUpdateDto, Category>();
            CreateMap<Category, CategoryReadDto>();
        }
    }
}
