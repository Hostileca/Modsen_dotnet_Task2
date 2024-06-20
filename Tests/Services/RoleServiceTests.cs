using BusinessLogicLayer.Dtos.Roles;

namespace Tests.Services
{
    public class RoleServiceTests
    {
        private readonly Mock<IRoleService> _mockRoleService;

        public RoleServiceTests()
        {
            _mockRoleService = new Mock<IRoleService>();
        }

        [Fact]
        public async Task CreateRoleAsync_ValidRoleCreateDto_CreateRoleAndReturnDto()
        {
            var roleCreateDto = new RoleCreateDto
            {
                Name = "TestRole"
            };

            var roleReadDto = new RoleReadDto
            {
                Id = Guid.NewGuid(),
                Role = roleCreateDto.Name
            };

            _mockRoleService.Setup(s => s.CreateRoleAsync(roleCreateDto))
                            .ReturnsAsync(roleReadDto);

            var result = await _mockRoleService.Object.CreateRoleAsync(roleCreateDto);

            Assert.NotNull(result);
            Assert.Equal(roleReadDto, result);
        }

        [Fact]
        public async Task DeleteRoleByIdAsync_ExistingRoleId_DeleteRoleAndReturnDto()
        {
            var roleId = Guid.NewGuid();
            var roleReadDto = new RoleReadDto
            {
                Id = roleId,
                Role = "TestRole"
            };

            _mockRoleService.Setup(s => s.DeleteRoleByIdAsync(roleId))
                            .ReturnsAsync(roleReadDto);

            var result = await _mockRoleService.Object.DeleteRoleByIdAsync(roleId);

            Assert.NotNull(result);
            Assert.Equal(roleReadDto, result);
        }

        [Fact]
        public async Task GetAllRolesAsync_ReturnsAllRoles()
        {
            var roleReadDtos = new List<RoleReadDto>
            {
                new RoleReadDto { Id = Guid.NewGuid(), Role = "Role1" },
                new RoleReadDto { Id = Guid.NewGuid(), Role = "Role2" }
            };

            _mockRoleService.Setup(s => s.GetAllRolesAsync())
                            .ReturnsAsync(roleReadDtos);

            var result = await _mockRoleService.Object.GetAllRolesAsync();

            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal(roleReadDtos[0], item),
                item => Assert.Equal(roleReadDtos[1], item));
        }

        [Fact]
        public async Task GetRoleByIdAsync_ExistingRoleId_ReturnsRole()
        {
            var roleId = Guid.NewGuid();
            var roleReadDto = new RoleReadDto
            {
                Id = roleId,
                Role = "TestRole"
            };

            _mockRoleService.Setup(s => s.GetRoleByIdAsync(roleId))
                            .ReturnsAsync(roleReadDto);

            var result = await _mockRoleService.Object.GetRoleByIdAsync(roleId);

            Assert.NotNull(result);
            Assert.Equal(roleReadDto, result);
        }

        [Fact]
        public async Task UpdateRoleAsync_ValidRoleUpdateDto_UpdateRoleAndReturnDto()
        {
            var roleUpdateDto = new RoleUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedRole"
            };

            var updatedRoleReadDto = new RoleReadDto
            {
                Id = roleUpdateDto.Id,
                Role = roleUpdateDto.Name
            };

            _mockRoleService.Setup(s => s.UpdateRoleAsync(roleUpdateDto))
                            .ReturnsAsync(updatedRoleReadDto);

            var result = await _mockRoleService.Object.UpdateRoleAsync(roleUpdateDto);

            Assert.NotNull(result);
            Assert.Equal(updatedRoleReadDto, result);
        }

        [Fact]
        public async Task CreateRoleAsync_NullRoleCreateDto_ThrowsArgumentNullException()
        {
            RoleCreateDto roleCreateDto = null;

            _mockRoleService.Setup(s => s.CreateRoleAsync(null))
                            .ThrowsAsync(new ArgumentNullException());

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockRoleService.Object.CreateRoleAsync(roleCreateDto));
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        }

        [Fact]
        public async Task DeleteRoleByIdAsync_NonExistingRoleId_ThrowsKeyNotFoundException()
        {
            var roleId = Guid.NewGuid();

            _mockRoleService.Setup(s => s.DeleteRoleByIdAsync(roleId))
                            .ThrowsAsync(new KeyNotFoundException($"Role not found with id: {roleId}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockRoleService.Object.DeleteRoleByIdAsync(roleId));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task GetRoleByIdAsync_NonExistingRoleId_ThrowsKeyNotFoundException()
        {
            var roleId = Guid.NewGuid();

            _mockRoleService.Setup(s => s.GetRoleByIdAsync(roleId))
                            .ThrowsAsync(new KeyNotFoundException($"Role not found with id: {roleId}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockRoleService.Object.GetRoleByIdAsync(roleId));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task UpdateRoleAsync_NonExistingRoleId_ThrowsKeyNotFoundException()
        {
            var roleUpdateDto = new RoleUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "UpdatedRole"
            };

            _mockRoleService.Setup(s => s.UpdateRoleAsync(roleUpdateDto))
                            .ThrowsAsync(new KeyNotFoundException($"Role not found with id: {roleUpdateDto.Id}"));

            var exception = await Assert.ThrowsAsync<KeyNotFoundException>(() => _mockRoleService.Object.UpdateRoleAsync(roleUpdateDto));
            Assert.Equal(typeof(KeyNotFoundException), exception.GetType());
        }

        [Fact]
        public async Task UpdateRoleAsync_NullRoleUpdateDto_ThrowsArgumentNullException()
        {
            RoleUpdateDto roleUpdateDto = null;

            _mockRoleService.Setup(s => s.UpdateRoleAsync(null))
                            .ThrowsAsync(new ArgumentNullException());

            var exception = await Assert.ThrowsAsync<ArgumentNullException>(() => _mockRoleService.Object.UpdateRoleAsync(roleUpdateDto));
            Assert.Equal(typeof(ArgumentNullException), exception.GetType());
        }
    }
}