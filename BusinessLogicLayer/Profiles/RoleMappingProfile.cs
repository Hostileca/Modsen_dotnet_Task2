using AutoMapper;
using BusinessLogicLayer.Dtos.Roles;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Profiles
{
    public class RoleMappingProfile : Profile
    {
        public RoleMappingProfile()
        {
            CreateMap<RoleCreateDto, Role>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<RoleUpdateDto, Role>()
                .ForMember(dest => dest.Users, opt => opt.Ignore());

            CreateMap<Role, RoleReadDto>()
                .ForMember(dest => dest.Role, opt => opt.MapFrom(src => src.Name));
        }
    }
}
