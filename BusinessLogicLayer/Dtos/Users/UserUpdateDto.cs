using DataAccessLayer.Models;

namespace BusinessLogicLayer.Dtos.Users
{
    public class UserUpdateDto
    {
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
