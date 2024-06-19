using AutoMapper;
using BusinessLogicLayer.Dtos.Products;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class ProductMappingProfile : Profile
    {
        public ProductMappingProfile()
        {
            CreateMap<ProductCreateDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Category, opt => opt.Ignore());  // ??? из CategoryId нужно получить полноценный Category

            CreateMap<ProductUpdateDto, Product>()
                .ForMember(dest => dest.Category, opt => opt.Ignore());  // ??? из CategoryId нужно получить полноценный Category

            CreateMap<Product, ProductReadDto>();
        }
    }
}

/*  ТЕСТ

public class ProductMappingProfile : Profile
{
    private readonly ICategoryService _categoryService;

    public ProductMappingProfile(ICategoryService categoryService)
    {
        _categoryService = categoryService ?? throw new ArgumentNullException(nameof(categoryService));

        CreateMap<ProductCreateDto, Product>()
            .ForMember(dest => dest.Id, opt => opt.Ignore())
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => _categoryService.GetCategoryByIdAsync(src.CategoryId).Result));

        CreateMap<ProductUpdateDto, Product>()
            .ForMember(dest => dest.Category, opt => opt.MapFrom(src => _categoryService.GetCategoryByIdAsync(src.CategoryId).Result));

        CreateMap<Product, ProductReadDto>();
    }
}
*/