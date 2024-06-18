using AutoMapper;
using BusinessLogicLayer.Dtos.Orders;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    internal class OrderMappingProfile : Profile
    {
        OrderMappingProfile()
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderReadDto>();
        }
    }
}
