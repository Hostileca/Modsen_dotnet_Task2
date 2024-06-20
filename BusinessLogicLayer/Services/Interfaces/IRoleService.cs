using BusinessLogicLayer.Dtos.Roles;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IRoleService
    {
        Task<IEnumerable<RoleReadDto>> GetAllRolesAsync();
        Task<RoleReadDto> GetRoleByIdAsync(Guid id);
    }
}
