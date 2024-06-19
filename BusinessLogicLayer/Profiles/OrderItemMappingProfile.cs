using AutoMapper;
using BusinessLogicLayer.Dtos.OrderItems;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItemCreateDto, OrderItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Order, opt => opt.Ignore()) // ??? из OrderId нужно получить полноценный Order
                .ForMember(dest => dest.Product, opt => opt.Ignore()); // ??? из ProductId нужно получить полноценный Product

            CreateMap<OrderItem, OrderItemReadDto>()
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => src.Product));
        }
    }
}