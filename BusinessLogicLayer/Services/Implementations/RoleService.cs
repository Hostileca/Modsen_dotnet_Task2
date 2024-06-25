using AutoMapper;
using BusinessLogicLayer.Dtos.Roles;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.Implementations
{
    public class RoleService : IRoleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public RoleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<RoleReadDto>> GetAllRolesAsync()
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            var roles = await roleRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<RoleReadDto>>(roles);
        }

        public async Task<RoleReadDto> GetRoleByIdAsync(Guid id)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            var role = await roleRepository.GetByIdAsync(id);
            if (role == null)
                throw new NotFoundException($"Role not found with id: {id}");
            return _mapper.Map<RoleReadDto>(role);
        }

        public async Task<IEnumerable<UserReadDto>> GetUsersWithRole(string role)
        {
            var roleRepository = _unitOfWork.GetRepository<Role>();
            var founded_role = (await roleRepository.GetByPredicateAsync(r => r.Name == role)).First();
            if (founded_role == null)
            {
                throw new NotFoundException($"Role with name {role} not found");
            }
            return _mapper.Map<IEnumerable<UserReadDto>>(founded_role.Users);
        }
    }
}