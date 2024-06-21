using BusinessLogicLayer.Dtos.Users;

namespace Tests.Profiles
{
    public class UserMappingProfileTests
    {
        private readonly IMapper _mapper;

        public UserMappingProfileTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new UserMappingProfile());
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
        public void UserCreateDto_To_User_Mapping()
        {
            var userCreateDto = new UserCreateDto
            {
                UserName = "TestUser",
                Password = "TestPassword"
            };

            var user = _mapper.Map<User>(userCreateDto);

            user.UserName.Should().Be(userCreateDto.UserName);
            user.HashedPassword.Should().NotBeNull();
        }

        [Fact]
        public void UserUpdateDto_To_User_Mapping()
        {
            var userUpdateDto = new UserUpdateDto
            {
                Id = Guid.NewGuid(),
                UserName = "UpdatedUser",
                Password = "UpdatedPassword",
                RoleId = Guid.NewGuid()
            };

            var user = _mapper.Map<User>(userUpdateDto);

            user.Should().BeEquivalentTo(userUpdateDto, options => options
                .Excluding(dto => dto.Password)
                .ExcludingMissingMembers());
            user.HashedPassword.Should().NotBeNull();
        }

        [Fact]
        public void User_To_UserReadDto_Mapping()
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = "UserRole"
            };

            var user = new User
            {
                Id = Guid.NewGuid(),
                UserName = "TestUser",
                Role = role
            };

            var userReadDto = _mapper.Map<UserReadDto>(user);

            userReadDto.Should().BeEquivalentTo(user, options => options
                .Excluding(u => u.Role)
                .ExcludingMissingMembers());
            userReadDto.Role.Id.Should().Be(user.Role.Id);
            userReadDto.Role.Role.Should().Be(user.Role.Name);
        }
    }
}
