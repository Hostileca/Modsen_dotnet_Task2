using AutoMapper;
using BusinessLogicLayer.Dtos.Products;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    internal class ProductMappingProfile : Profile
    {
        ProductMappingProfile() 
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductReadDto>();
        }
    }
}
