using AutoMapper;
using BusinessLogicLayer.Dtos.Roles;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    internal class RoleMappingProfile : Profile
    {
        RoleMappingProfile()
        {
            CreateMap<RoleCreateDto, Role>();
            CreateMap<RoleUpdateDto, Role>();
            CreateMap<Role, RoleReadDto>();
        }
    }
}
