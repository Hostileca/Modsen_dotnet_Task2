using BusinessLogicLayer.Dtos.Roles;

namespace BusinessLogicLayer.Dtos.Users
{
    public class UserReadDto
    {
        public Guid Id { get; set; }
        public string UserName { get; set; }
        public RoleReadDto Role { get; set; }
    }
}
