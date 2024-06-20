
using System.Text.Json.Serialization;

namespace BusinessLogicLayer.Dtos.Roles
{
    public class RoleUpdateDto
    {
        [JsonIgnore]
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
