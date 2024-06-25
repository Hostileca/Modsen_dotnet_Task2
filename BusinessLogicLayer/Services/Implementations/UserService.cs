using AutoMapper;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Exceptions;
using BusinessLogicLayer.Services.Algorithms;
using BusinessLogicLayer.Services.Interfaces;
using DataAccessLayer.Data.Interfaces;
using DataAccessLayer.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BusinessLogicLayer.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IPasswordHasher _passwordHasher;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IPasswordHasher passwordHasher, IUserRepository userRepository, IRoleRepository roleRepository, IMapper mapper, IConfiguration configuration)
        {
            _passwordHasher = passwordHasher;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AuthenticationResponseDto> AuthenticateAsync(UserLoginDto loginDto, CancellationToken cancellationToken = default)
        {
            if (loginDto == null)
            {
                throw new ArgumentNullException(nameof(loginDto));
            }

            var existingUser = (await _userRepository.GetByPredicateAsync(user => user.UserName == loginDto.UserName, cancellationToken)).FirstOrDefault();
            if(existingUser == null)
            {
                throw new NotFoundException($"User not found with username {loginDto.UserName}");
            }

            if (!_passwordHasher.VerifyHashedPassword(existingUser.HashedPassword, loginDto.Password))
            {
                throw new UnauthorizedException("Wrong password");
            }

            var token = GenerateAccessToken(existingUser);
            return new AuthenticationResponseDto
            {
                AccessToken = token,
                ExpiresIn = DateTime.UtcNow.AddHours(1)
            };
        }

        public async Task<UserReadDto> CreateUserAsync(UserCreateDto userCreateDto, CancellationToken cancellationToken = default)
        {
            if (userCreateDto == null)
            {
                throw new ArgumentNullException(nameof(userCreateDto));
            }

            var existingUser = (await _userRepository.GetByPredicateAsync(user => user.UserName == userCreateDto.UserName, cancellationToken)).FirstOrDefault();
            if (existingUser != null)
            {
                throw new DuplicateNameException($"User with username {userCreateDto.UserName} already exists");
            }
            var user = _mapper.Map<User>(userCreateDto);

            user.HashedPassword = _passwordHasher.HashPassword(userCreateDto.Password);
            user.Role = (await _roleRepository.GetByPredicateAsync(r => r.Name == RoleConstants.User, cancellationToken)).FirstOrDefault();

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

        private string GenerateAccessToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name,user.UserName)
            };

            claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtSettings:Key"]));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha512Signature);
            var securityToken = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(60),
                issuer: _configuration["JwtSettings:Issuer"],
                audience: _configuration["JwtSettings:Audience"],
                signingCredentials: signingCredentials);

            var tokenString = new JwtSecurityTokenHandler().WriteToken(securityToken);
            return tokenString;
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
