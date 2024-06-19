using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Dtos.OrderItems
{
    public class OrderItemUpdateDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}
