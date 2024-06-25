namespace DataAccessLayer.Models
{
    public class Role : BaseModel
    {
        public Roles Name { get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
