using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Roles
{
    public class RoleCreateDto
    {
        public DataAccessLayer.Models.Roles Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
