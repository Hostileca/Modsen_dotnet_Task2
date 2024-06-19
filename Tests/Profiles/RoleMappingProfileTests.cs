using BusinessLogicLayer.Dtos.Roles;

namespace Tests.Profiles
{
    public class RoleMappingProfileTests
    {
        private IMapper _mapper;

        public RoleMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new RoleMappingProfile());
            });

            _mapper = configuration.CreateMapper();
        }

        [Fact]
        public void Mapping_Configuration_IsValid()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }

        [Fact]
        public void RoleCreateDto_To_Role_Mapping()
        {
            var roleCreateDto = new RoleCreateDto
            {
                Name = "Admin"
            };

            var role = _mapper.Map<Role>(roleCreateDto);

            Assert.Equal(roleCreateDto.Name, role.Name);
        }

        [Fact]
        public void RoleUpdateDto_To_Role_Mapping()
        {
            var roleUpdateDto = new RoleUpdateDto
            {
                Id = Guid.NewGuid(),
                Name = "SuperAdmin"
            };

            var role = _mapper.Map<Role>(roleUpdateDto);

            Assert.Equal(roleUpdateDto.Id, role.Id);
            Assert.Equal(roleUpdateDto.Name, role.Name);
        }

        [Fact]
        public void Role_To_RoleReadDto_Mapping()
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "User"
            };

            var roleReadDto = _mapper.Map<RoleReadDto>(role);

            Assert.Equal(role.Id, roleReadDto.Id);
            Assert.Equal(role.Name, roleReadDto.Role);
        }
    }
}