using System.Text.Json.Serialization;

namespace BusinessLogicLayer.Dtos.OrderItems
{
    public class OrderItemUpdateDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public int Amount { get; set; }
    }
}
