using BusinessLogicLayer.Dtos.Users;

namespace BusinessLogicLayer.Dtos.Roles
{
    public class RoleReadDto
    {
        public Guid Id { get; set; }
        public DataAccessLayer.Models.Roles Role { get; set; }
        public ICollection<UserReadDto> Users { get; set; }
    }
}
