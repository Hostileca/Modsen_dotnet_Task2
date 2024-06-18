using AutoMapper;
using BusinessLogicLayer.Dtos.OrderItems;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class OrderItemMappingProfile : Profile
    {
        public OrderItemMappingProfile()
        {
            CreateMap<OrderItemCreateDto, OrderItem>();
            CreateMap<OrderItem, OrderItemReadDto>();
        }
    }
}
