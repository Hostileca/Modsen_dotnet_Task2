using AutoMapper;
using BusinessLogicLayer.Dtos.Orders;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class OrderMappingProfile : Profile
    {
        public OrderMappingProfile()
        {
            CreateMap<OrderCreateDto, Order>();
            CreateMap<Order, OrderReadDto>();
        }
    }
}
