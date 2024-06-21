using BusinessLogicLayer.Dtos.Roles;

namespace Tests.Profiles
{
    public class RoleMappingProfileTests
    {
        private readonly IMapper _mapper;

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

            role.Should().BeEquivalentTo(roleCreateDto, options => options
                .ExcludingMissingMembers());
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

            role.Should().BeEquivalentTo(roleUpdateDto, options => options
                .ExcludingMissingMembers());
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

            roleReadDto.Should().BeEquivalentTo(role, options => options
                .ExcludingMissingMembers()
                .Excluding(dto => dto.Name));

            roleReadDto.Role.Should().Be(role.Name);
        }
    }
}