namespace DataAccessLayer.Models
{
    public class User
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
    }
}
