using AutoMapper;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Algorithms;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;

namespace BusinessLogicLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper)
        {
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            _roleRepository = roleRepository ?? throw new ArgumentNullException(nameof(roleRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken = default)
        {
            if (userCreateDto == null)
            {
                throw new ArgumentNullException(nameof(userCreateDto));
            }

            var user = _mapper.Map<User>(userCreateDto);

            user.HashedPassword = _passwordHasher.HashPassword(userCreateDto.Password);
            user.Role = (await _roleRepository.GetByPredicateAsync(r => r.Name == RoleConstants.User, cancellationToken)).First();

            await _userRepository.AddAsync(user, cancellationToken);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> DeleteUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User not found with id: {id}");
            }

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync(CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetAllAsync(cancellationToken);
            return _mapper.Map<IEnumerable<UserReadDto>>(user);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetByIdAsync(id, cancellationToken);
            if (user == null)
            {
                throw new NotFoundException($"User not found with id: {id}");
            }
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> UpdateUserAsync(UserUpdateDto userUpdateDto, CancellationToken cancellationToken = default)
        {
            if (userUpdateDto == null)
            {
                throw new ArgumentNullException(nameof(userUpdateDto));
            }

            var existingUser = await _userRepository.GetByIdAsync(userUpdateDto.Id, cancellationToken);
            if (existingUser == null)
            {
                throw new NotFoundException($"User not found with id: {userUpdateDto.Id}");
            }

            var existingRole = await _roleRepository.GetByIdAsync(userUpdateDto.RoleId, cancellationToken);
            if (existingRole == null)
            {
                throw new NotFoundException($"Role not found with id: {userUpdateDto.RoleId}");
            }

            existingUser.HashedPassword = _passwordHasher.HashPassword(userUpdateDto.Password);

            var newUser = _mapper.Map(userUpdateDto, existingUser);
            await _userRepository.SaveChangesAsync(cancellationToken);
            return _mapper.Map<UserReadDto>(newUser);
        }
    }
}
