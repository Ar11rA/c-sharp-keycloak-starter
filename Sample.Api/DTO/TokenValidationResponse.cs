using System.Text.Json.Serialization;

namespace Sample.Api.DTO;

public class TokenValidationResponse
{
    [JsonPropertyName("active")] public bool IsActive { get; set; }
    [JsonPropertyName("assignedGroups")] public List<string> Groups { get; set; }
    [JsonPropertyName("email")] public string Email { get; set; }
}
