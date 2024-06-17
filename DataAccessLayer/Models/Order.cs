using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class Order
    {
        public Guid Guid { get; set; }
        public User User { get; set; }
        public ICollection<OrderItem> Items { get; set; }
    }
}
