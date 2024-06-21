using BusinessLogicLayer.Dtos.Roles;
using BusinessLogicLayer.Dtos.Users;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleReadDto>> GetAllRolesAsync();
        Task<RoleReadDto> GetRoleByIdAsync(Guid id);
        Task<IEnumerable<UserReadDto>> GetUsersWithRole(string role);
    }
}
