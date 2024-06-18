using AutoMapper;
using BusinessLogicLayer.Dtos.OrderItems;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    internal class OrderItemMappingProfile : Profile
    {
        OrderItemMappingProfile()
        {
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItem, OrderItemReadDto>();
        }
    }
}
