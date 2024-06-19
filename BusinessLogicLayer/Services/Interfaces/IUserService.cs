using BusinessLogicLayer.Dtos.Users;
using System.Linq.Expressions;

namespace BusinessLogicLayer.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserReadDto>> GetAllUsersAsync();
        Task<UserReadDto> GetUserByIdAsync(Guid id);
        Task<UserReadDto> GetUserByPredicateAsync(Expression<Func<UserReadDto, bool>> predicate);
        Task<UserReadDto> CreateUserAsync(UserCreateDto UserCreateDto);
        Task<UserReadDto> UpdateUserAsync(UserUpdateDto UserUpdateDto);
        Task<UserReadDto> DeleteUserByIdAsync(Guid id);
    }
}
