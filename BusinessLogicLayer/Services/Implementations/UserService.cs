using AutoMapper;
using System.Security.Cryptography;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using System.Linq.Expressions;
using System.Text;

namespace BusinessLogicLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IRepository<Role> _roleRepository;
        private readonly IMapper _mapper;

        public UserService(IRepository<User> userRepository, IRepository<Role> roleRepository, IMapper mapper)
        {
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
                throw new ArgumentNullException(nameof(userCreateDto));

            var user = _mapper.Map<User>(userCreateDto);

            user.HashedPassword = HashPassword(userCreateDto.Password);
            //user.Role = 

            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> DeleteUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User not found with id: {id}");

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var user = await _userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(user);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new KeyNotFoundException($"User not found with id: {id}");
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<IEnumerable<UserReadDto>> GetUserByPredicateAsync(Expression<Func<UserReadDto, bool>> predicate)
        {
            if (predicate == null)
                throw new ArgumentNullException(nameof(predicate));

            var userPredicate = _mapper.Map<Expression<Func<User, bool>>>(predicate);
            var users = await _userRepository.GetByPredicateAsync(userPredicate);
            if (users == null)
                throw new KeyNotFoundException("User not found.");

            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null)
                throw new ArgumentNullException(nameof(userUpdateDto));

            var existingUser = await _userRepository.GetByIdAsync(userUpdateDto.Id);
            if (existingUser == null)
                throw new KeyNotFoundException($"User not found with id: {userUpdateDto.Id}");

            var existingRole = await _roleRepository.GetByIdAsync(userUpdateDto.RoleId);
            if (existingRole == null)
                throw new KeyNotFoundException($"Role not found with id: {userUpdateDto.RoleId}");

            existingUser.HashedPassword = HashPassword(userUpdateDto.Password);

            var newUser = _mapper.Map(userUpdateDto, existingUser);
            await _userRepository.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(newUser);
        }


        //think about it
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                    builder.Append(bytes[i].ToString("x2"));
                return builder.ToString();
            }
        }
    }
}
