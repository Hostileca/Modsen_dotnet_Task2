using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class User
    {
        public Guid Guid { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Guid RoleGuid { get; set; }
        public Role Role { get; set; }
    }
}
