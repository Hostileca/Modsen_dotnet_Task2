//using BusinessLogicLayer.Dtos.Roles;
//using BusinessLogicLayer.Dtos.Users;

//namespace Tests.Services
//{

//    public class UserServiceTests
//    {
//        private readonly Mock<IUserService> _mockUserService;

//        public UserServiceTests()
//        {
//            _mockUserService = new Mock<IUserService>();
//        }

//        [Fact]
//        public async Task CreateUserAsync_ValidUserCreateDto_CreateUserAndReturnDto()
//        {
//            var userCreateDto = new UserCreateDto
//            {
//                UserName = "TestUser",
//                Password = "TestPassword"
//            };

//            var userReadDto = new UserReadDto
//            {
//                Id = Guid.NewGuid(),
//                UserName = userCreateDto.UserName,
//                Role = new RoleReadDto
//                {
//                    Id = Guid.NewGuid(),
//                    Role = "TestRole"
//                }
//            };

//            _mockUserService.Setup(s => s.CreateUserAsync(userCreateDto))
//                            .ReturnsAsync(userReadDto);

//            var result = await _mockUserService.Object.CreateUserAsync(userCreateDto);

//            Assert.NotNull(result);
//            Assert.Equal(userReadDto, result);
//        }

//        [Fact]
//        public async Task DeleteUserByIdAsync_ExistingUserId_DeleteUserAndReturnDto()
//        {
//            var userId = Guid.NewGuid();
//            var userReadDto = new UserReadDto
//            {
//                Id = userId,
//                UserName = "TestUser",
//                Role = new RoleReadDto
//                {
//                    Id = Guid.NewGuid(),
//                    Role = "TestRole"
//                }
//            };

//            _mockUserService.Setup(s => s.DeleteUserByIdAsync(userId))
//                            .ReturnsAsync(userReadDto);

//            var result = await _mockUserService.Object.DeleteUserByIdAsync(userId);

//            Assert.NotNull(result);
//            Assert.Equal(userReadDto, result);
//        }

//        [Fact]
//        public async Task GetAllUsersAsync_ReturnsAllUsers()
//        {
//            var userReadDtos = new List<UserReadDto>
//            {
//                new UserReadDto { Id = Guid.NewGuid(), UserName = "User1", Role = new RoleReadDto { Id = Guid.NewGuid(), Role = "Role1" } },
//                new UserReadDto { Id = Guid.NewGuid(), UserName = "User2", Role = new RoleReadDto { Id = Guid.NewGuid(), Role = "Role2" } }
//            };

//            _mockUserService.Setup(s => s.GetAllUsersAsync())
//                            .ReturnsAsync(userReadDtos);

//            var result = await _mockUserService.Object.GetAllUsersAsync();

//            Assert.NotNull(result);
//            Assert.Collection(result,
//                item => Assert.Equal(userReadDtos[0], item),
//                item => Assert.Equal(userReadDtos[1], item));
//        }

//        [Fact]
//        public async Task GetUserByIdAsync_ExistingUserId_ReturnsUser()
//        {
//            var userId = Guid.NewGuid();
//            var userReadDto = new UserReadDto
//            {
//                Id = userId,
//                UserName = "TestUser",
//                Role = new RoleReadDto
//                {
//                    Id = Guid.NewGuid(),
//                    Role = "TestRole"
//                }
//            };

//            _mockUserService.Setup(s => s.GetUserByIdAsync(userId))
//                            .ReturnsAsync(userReadDto);

//            var result = await _mockUserService.Object.GetUserByIdAsync(userId);

//            Assert.NotNull(result);
//            Assert.Equal(userReadDto, result);
//        }

//        [Fact]
//        public async Task UpdateUserAsync_ValidUserUpdateDto_UpdateUserAndReturnDto()
//        {
//            var userUpdateDto = new UserUpdateDto
//            {
//                Id = Guid.NewGuid(),
//                UserName = "UpdatedUser",
//                Password = "UpdatedPassword",
//                RoleId = Guid.NewGuid()
//            };

//            var updatedUserReadDto = new UserReadDto
//            {
//                Id = userUpdateDto.Id,
//                UserName = userUpdateDto.UserName,
//                Role = new RoleReadDto
//                {
//                    Id = userUpdateDto.RoleId,
//                    Role = "TestRole"
//                }
//            };

//            _mockUserService.Setup(s => s.UpdateUserAsync(userUpdateDto))
//                            .ReturnsAsync(updatedUserReadDto);

//            var result = await _mockUserService.Object.UpdateUserAsync(userUpdateDto);

//            Assert.NotNull(result);
//            Assert.Equal(updatedUserReadDto, result);
//        }

//        [Fact]
//        public async Task CreateUserAsync_NullUserCreateDto_ThrowsArgumentNullException()
//        {
//            UserCreateDto userCreateDto = null;

//            _mockUserService.Setup(s => s.CreateUserAsync(null))
//                            .ThrowsAsync(new ArgumentNullException());

//            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockUserService.Object.CreateUserAsync(userCreateDto));
//            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
//        }

//        [Fact]
//        public async Task DeleteUserByIdAsync_NonExistingUserId_ThrowsKeyNotFoundException()
//        {
//            var userId = Guid.NewGuid();

//            _mockUserService.Setup(s => s.DeleteUserByIdAsync(userId))
//                            .ThrowsAsync(new KeyNotFoundException($"User not found with id: {userId}"));

//            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockUserService.Object.DeleteUserByIdAsync(userId));
//            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
//        }

//        [Fact]
//        public async Task GetUserByIdAsync_NonExistingUserId_ThrowsKeyNotFoundException()
//        {
//            var userId = Guid.NewGuid();

//            _mockUserService.Setup(s => s.GetUserByIdAsync(userId))
//                            .ThrowsAsync(new KeyNotFoundException($"User not found with id: {userId}"));

//            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockUserService.Object.GetUserByIdAsync(userId));
//            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
//        }

//        [Fact]
//        public async Task UpdateUserAsync_NonExistingUserId_ThrowsKeyNotFoundException()
//        {
//            var userUpdateDto = new UserUpdateDto
//            {
//                Id = Guid.NewGuid(),
//                UserName = "UpdatedUser",
//                Password = "UpdatedPassword",
//                RoleId = Guid.NewGuid()
//            };

//            _mockUserService.Setup(s => s.UpdateUserAsync(userUpdateDto))
//                            .ThrowsAsync(new KeyNotFoundException($"User not found with id: {userUpdateDto.Id}"));

//            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockUserService.Object.UpdateUserAsync(userUpdateDto));
//            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
//        }

//        [Fact]
//        public async Task UpdateUserAsync_NonExistingRoleId_ThrowsKeyNotFoundException()
//        {
//            var userUpdateDto = new UserUpdateDto
//            {
//                Id = Guid.NewGuid(),
//                UserName = "UpdatedUser",
//                Password = "UpdatedPassword",
//                RoleId = Guid.NewGuid()
//            };

//            _mockUserService.Setup(s => s.UpdateUserAsync(userUpdateDto))
//                            .ThrowsAsync(new KeyNotFoundException($"Role not found with id: {userUpdateDto.RoleId}"));

//            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockUserService.Object.UpdateUserAsync(userUpdateDto));
//            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
//        }

//        [Fact]
//        public async Task UpdateUserAsync_NullUserUpdateDto_ThrowsArgumentNullException()
//        {
//            UserUpdateDto userUpdateDto = null;

//            _mockUserService.Setup(s => s.UpdateUserAsync(null))
//                            .ThrowsAsync(new ArgumentNullException());

//            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockUserService.Object.UpdateUserAsync(userUpdateDto));
//            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
//        }
//    }
//}