namespace DataAccessLayer.Models
{
    public class User : BaseModel
    {
        public Guid RoleId { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public Role Role { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
