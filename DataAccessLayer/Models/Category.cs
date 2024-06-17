using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Category
    {
        public Guid Guid { get; set; }
        public string Name { get; set; }
        public ICollection<Product> Products { get; set; }
    }
}
