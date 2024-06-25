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
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IPasswordHasher passwordHasher, IMapper mapper)
        {
            _unitOfWork = unitOfWork ?? throw new ArgumentNullException(nameof(unitOfWork));
            _passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto)
        {
            if (userCreateDto == null)
                throw new ArgumentNullException(nameof(userCreateDto));

            var userRepository = _unitOfWork.GetRepository<User>();
            var roleRepository = _unitOfWork.GetRepository<Role>();

            var user = _mapper.Map<User>(userCreateDto);

            user.HashedPassword = _passwordHasher.HashPassword(userCreateDto.Password);
            user.Role = (await roleRepository.GetByPredicateAsync(r => r.Name == RoleConstants.User)).FirstOrDefault();

            await userRepository.AddAsync(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> DeleteUserByIdAsync(Guid id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"User not found with id: {id}");

            userRepository.Delete(user);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<IEnumerable<UserReadDto>> GetAllUsersAsync()
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            var users = await userRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<UserReadDto>>(users);
        }

        public async Task<UserReadDto> GetUserByIdAsync(Guid id)
        {
            var userRepository = _unitOfWork.GetRepository<User>();

            var user = await userRepository.GetByIdAsync(id);
            if (user == null)
                throw new NotFoundException($"User not found with id: {id}");

            return _mapper.Map<UserReadDto>(user);
        }

        public async Task<UserReadDto> UpdateUserAsync(UserUpdateDto userUpdateDto)
        {
            if (userUpdateDto == null)
                throw new ArgumentNullException(nameof(userUpdateDto));

            var userRepository = _unitOfWork.GetRepository<User>();
            var roleRepository = _unitOfWork.GetRepository<Role>();

            var existingUser = await userRepository.GetByIdAsync(userUpdateDto.Id);
            if (existingUser == null)
                throw new NotFoundException($"User not found with id: {userUpdateDto.Id}");

            var existingRole = await roleRepository.GetByIdAsync(userUpdateDto.RoleId);
            if (existingRole == null)
                throw new NotFoundException($"Role not found with id: {userUpdateDto.RoleId}");

            existingUser.HashedPassword = _passwordHasher.HashPassword(userUpdateDto.Password);

            var updatedUser = _mapper.Map(userUpdateDto, existingUser);
            await _unitOfWork.SaveChangesAsync();
            return _mapper.Map<UserReadDto>(updatedUser);
        }
    }
}