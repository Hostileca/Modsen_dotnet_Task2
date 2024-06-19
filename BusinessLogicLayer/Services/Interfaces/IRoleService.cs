using BusinessLogicLayer.Dtos.Roles;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleReadDto>> GetAllRolesAsync();
        Task<RoleReadDto> GetRoleByIdAsync(Guid id);
        Task<IEnumerable<RoleReadDto>> GetRoleByPredicateAsync(Expression<Func<RoleReadDto, bool>> predicate);
        Task<RoleReadDto> CreateRoleAsync(RoleCreateDto RoleCreateDto);
        Task<RoleReadDto> UpdateRoleAsync(RoleUpdateDto RoleUpdateDto);
        Task<RoleReadDto> DeleteRoleByIdAsync(Guid id);
    }
}
