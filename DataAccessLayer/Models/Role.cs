namespace DataAccessLayer.Models
{
    public class Role
    {
        public Guid Id { get; set; }
        public Roles Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
