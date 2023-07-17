using System.Text.Json.Serialization;

namespace Sample.Api.DTO;

public class TokenRequest
{
    [JsonPropertyName("token")] public string Token { get; set; }
}
