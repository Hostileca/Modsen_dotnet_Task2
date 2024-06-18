using AutoMapper;
using BusinessLogicLayer.Dtos.Users;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    internal class UserMappingProfile : Profile
    {
        UserMappingProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserReadDto>();
        }
    }
}
