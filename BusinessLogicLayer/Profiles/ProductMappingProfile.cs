using AutoMapper;
using BusinessLogicLayer.Dtos.Products;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateDto, Product>();
            CreateMap<ProductUpdateDto, Product>();
            CreateMap<Product, ProductReadDto>();
        }
    }
}
