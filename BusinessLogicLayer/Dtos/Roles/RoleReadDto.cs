using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Roles
{
    public class RoleReadDto
    {
        public Guid Guid { get; set; }
        public DataAccessLayer.Models.Roles Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
