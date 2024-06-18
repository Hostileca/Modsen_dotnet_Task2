using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Role
    {
        public Guid Guid { get; set; }
        public Roles Name { get; set; }
        public ICollection<User> Users { get; set; }
    }

}
