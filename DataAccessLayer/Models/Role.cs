namespace DataAccessLayer.Models
{
    public class Role
    {
        public Guid Guid { get; set; }
        public Roles Name { get; set; }
        public ICollection<User> Users { get; set; }
    }
}
