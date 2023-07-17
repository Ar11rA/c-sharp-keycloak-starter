using System.Text.Json.Serialization;

namespace Sample.Api.DTO;

public class LoginRequest
{
    [JsonPropertyName("code")] public string Code { get; set; }
}
