using System.Text.Json.Serialization;

namespace Sample.Api.DTO;

public class TokenDetails
{
    [JsonPropertyName("access_token")] public string? AccessToken { get; set; }
    [JsonPropertyName("refresh_token")] public string? RefreshToken { get; set; }
}
