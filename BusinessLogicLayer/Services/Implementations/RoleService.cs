using AutoMapper;
using BusinessLogicLayer.Dtos.Roles;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Implementations;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;

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

        public async Task<RoleReadDto> CreateRoleAsync(RoleCreateDto roleCreateDto)
        {
            if (roleCreateDto == null)
                throw new ArgumentNullException(nameof(roleCreateDto));

            var role = _mapper.Map<Role>(roleCreateDto);
            await _roleRepository.AddAsync(role);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<RoleReadDto> DeleteRoleByIdAsync(Guid id)
        {
            var role = await _roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new KeyNotFoundException($"Role not found with id: {id}");
            
            _roleRepository.Delete(role);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleReadDto>(role);
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
                throw new KeyNotFoundException($"Role not found with id: {id}");
            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<IEnumerable<RoleReadDto>> GetRoleByPredicateAsync(Expression<Func<RoleReadDto, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var rolePredicate = _mapper.Map<Expression<Func<Role, bool>>>(predicate);
            var roles = await _roleRepository.GetByPredicateAsync(rolePredicate);
            if (roles == null)
                throw new KeyNotFoundException("Role not found.");

            return _mapper.Map<IEnumerable<RoleReadDto>>(roles);
        }

        public async Task<RoleReadDto> UpdateRoleAsync(RoleUpdateDto roleUpdateDto)
        {
            if (roleUpdateDto == null)
                throw new ArgumentNullException(nameof(roleUpdateDto));

            var existingRole = await _roleRepository.GetByIdAsync(roleUpdateDto.Id);
            if (existingRole == null)
                throw new KeyNotFoundException($"Role not found with id: {roleUpdateDto.Id}");

            var newRole = _mapper.Map(roleUpdateDto, existingRole);
            await _roleRepository.SaveChangesAsync();
            return _mapper.Map<RoleReadDto>(newRole);
        }
    }
}
