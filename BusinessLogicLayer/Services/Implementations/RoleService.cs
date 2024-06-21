using AutoMapper;
using BusinessLogicLayer.Dtos.Roles;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;

namespace BusinessLogicLayer.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public RoleService(IRoleRepository roleRepository, IMapper mapper)
        {
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllRolesAsync()
        {
            var roles = await _roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleReadDto>>(roles);
        }

        public async Task<RoleReadDto> GetRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new NotFoundException($"Role not found with id: {id}");
            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersWithRole(string role)
        {
            var founded_role = (await _roleRepository.GetByPredicateAsync(r => r.Name == role)).First();
            if (founded_role == null)
            {
                throw new NotFoundException($"Role with name {role} not found");
            }
            return _mapper.Map<IEnumerable<UserReadDto>>(founded_role.Users);
        }
    }
}
