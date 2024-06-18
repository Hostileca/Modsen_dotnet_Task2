using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Models
{
    public class OrderItem
    {
        public Guid OrderGuid { get; set; }
        public Order Order { get; set; }
        public Guid ProductGuid { get; set; }
        public Product Product { get; set; }
        public int Amount { get; set; }
    }
}
