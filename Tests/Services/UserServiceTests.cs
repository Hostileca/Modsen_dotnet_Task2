using BusinessLogicLayer.Dtos.Roles;
using BusinessLogicLayer.Dtos.Users;
using BusinessLogicLayer.Services.Algorithms;

namespace Tests.Services
{
    public class UserServiceTests
    {
        private readonly Mock<IPasswordHasher> _mockPasswordHasher;
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IMapper> _mockMapper;
        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _mockPasswordHasher = new Mock<IPasswordHasher>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockMapper = new Mock<IMapper>();
            _userService = new UserService(_mockUnitOfWork.Object, _mockPasswordHasher.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllUsersAsync_ReturnsListOfUserReadDto()
        {
            var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), UserName = "user1", Role = new Role { Id = Guid.NewGuid(), Name = "User" } },
                new User { Id = Guid.NewGuid(), UserName = "user2", Role = new Role { Id = Guid.NewGuid(), Name = "Admin" } }
            };

            var mockUserRepository = new Mock<IRepository<User>>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<User>()).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(users);
            _mockMapper.Setup(m => m.Map<IEnumerable<UserReadDto>>(users)).Returns(users.Select(u => new UserReadDto { Id = u.Id, UserName = u.UserName, Role = new RoleReadDto { Id = u.Role.Id, Name = u.Role.Name } }));

            var result = await _userService.GetAllUsersAsync();

            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            result.First().UserName.Should().Be("user1");
        }

        [Fact]
        public async Task GetUserByIdAsync_ExistingUserId_ReturnsUserReadDto()
        {
            var userId = Guid.NewGuid();
            var existingUser = new User { Id = userId, UserName = "testuser", Role = new Role { Id = Guid.NewGuid(), Name = "User" } };

            var mockUserRepository = new Mock<IRepository<User>>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<User>()).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            _mockMapper.Setup(m => m.Map<UserReadDto>(existingUser)).Returns(new UserReadDto { Id = existingUser.Id, UserName = existingUser.UserName, Role = new RoleReadDto { Id = existingUser.Role.Id, Name = existingUser.Role.Name } });

            var result = await _userService.GetUserByIdAsync(userId);

            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.UserName.Should().Be("testuser");
        }

        [Fact]
        public async Task GetUserByIdAsync_NonExistingUserId_ThrowsNotFoundException()
        {
            var userId = Guid.NewGuid();
            var mockUserRepository = new Mock<IRepository<User>>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<User>()).Returns(mockUserRepository.Object);
            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync((User)null);

            Func<Task> action = async () => await _userService.GetUserByIdAsync(userId);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateUserAsync_ValidUserUpdateDto_ReturnsUpdatedUserReadDto()
        {
            var userId = Guid.NewGuid();
            var roleId = Guid.NewGuid();
            var userUpdateDto = new UserUpdateDto { Id = userId, UserName = "updateduser", Password = "updatedpassword", RoleId = roleId };
            var existingUser = new User { Id = userId, UserName = "olduser", RoleId = userUpdateDto.RoleId };
            var existingRole = new Role { Id = roleId, Name = "Admin" };

            var mockUserRepository = new Mock<IRepository<User>>();
            var mockRoleRepository = new Mock<IRepository<Role>>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<User>()).Returns(mockUserRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Role>()).Returns(mockRoleRepository.Object);

            mockUserRepository.Setup(repo => repo.GetByIdAsync(userId)).ReturnsAsync(existingUser);
            mockRoleRepository.Setup(repo => repo.GetByIdAsync(roleId)).ReturnsAsync(existingRole);
            _mockPasswordHasher.Setup(ph => ph.HashPassword(userUpdateDto.Password)).Returns("updatedhashedpassword");

            var updatedUserDto = new UserReadDto
            {
                Id = userId,
                UserName = userUpdateDto.UserName,
                Role = new RoleReadDto { Id = existingRole.Id, Name = existingRole.Name }
            };
            _mockMapper.Setup(m => m.Map<UserReadDto>(It.IsAny<User>())).Returns(updatedUserDto);

            var result = await _userService.UpdateUserAsync(userUpdateDto);

            result.Should().NotBeNull();
            result.Id.Should().Be(userId);
            result.UserName.Should().Be("updateduser");
            result.Role.Should().NotBeNull();
            result.Role.Id.Should().Be(existingRole.Id);
            result.Role.Name.Should().Be(existingRole.Name);
        }


        [Fact]
        public async Task UpdateUserAsync_NonExistingUserId_ThrowsNotFoundException()
        {
            var userUpdateDto = new UserUpdateDto { Id = Guid.NewGuid(), UserName = "updateduser", Password = "updatedpassword", RoleId = Guid.NewGuid() };

            var mockUserRepository = new Mock<IRepository<User>>();
            _mockUnitOfWork.Setup(uow => uow.GetRepository<User>()).Returns(mockUserRepository.Object);

            mockUserRepository.Setup(repo => repo.GetByIdAsync(userUpdateDto.Id)).ReturnsAsync((User)null);

            Func<Task> action = async () => await _userService.UpdateUserAsync(userUpdateDto);

            await action.Should().ThrowAsync<NotFoundException>();
        }

        [Fact]
        public async Task UpdateUserAsync_InvalidRoleId_ThrowsNotFoundException()
        {
            var userUpdateDto = new UserUpdateDto { Id = Guid.NewGuid(), UserName = "updateduser", Password = "updatedpassword", RoleId = Guid.NewGuid() };
            var existingUser = new User { Id = userUpdateDto.Id, UserName = "olduser", Role = new Role { Id = Guid.NewGuid(), Name = "User" } };

            var mockUserRepository = new Mock<IRepository<User>>();
            var mockRoleRepository = new Mock<IRepository<Role>>();

            _mockUnitOfWork.Setup(uow => uow.GetRepository<User>()).Returns(mockUserRepository.Object);
            _mockUnitOfWork.Setup(uow => uow.GetRepository<Role>()).Returns(mockRoleRepository.Object);

            mockUserRepository.Setup(repo => repo.GetByIdAsync(userUpdateDto.Id)).ReturnsAsync(existingUser);
            mockRoleRepository.Setup(repo => repo.GetByIdAsync(userUpdateDto.RoleId)).ReturnsAsync((Role)null);

            Func<Task> action = async () => await _userService.UpdateUserAsync(userUpdateDto);

            await action.Should().ThrowAsync<NotFoundException>();
        }
    }
}
