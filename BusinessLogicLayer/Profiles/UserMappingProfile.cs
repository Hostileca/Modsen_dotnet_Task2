using AutoMapper;
using BusinessLogicLayer.Dtos.Users;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class UserMappingProfile : Profile
    {
        public UserMappingProfile()
        {
            CreateMap<UserCreateDto, User>();
            CreateMap<UserUpdateDto, User>();
            CreateMap<User, UserReadDto>();
        }
    }
}
