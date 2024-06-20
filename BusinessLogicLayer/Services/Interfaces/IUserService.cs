using BusinessLogicLayer.Dtos.Users;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(Guid id);
        Task<UserReadDto> CreateUserAsync(UserCreateDto UserCreateDto);
        Task<UserReadDto> UpdateUserAsync(UserUpdateDto UserUpdateDto);
        Task<UserReadDto> DeleteUserByIdAsync(Guid id);
    }
}
