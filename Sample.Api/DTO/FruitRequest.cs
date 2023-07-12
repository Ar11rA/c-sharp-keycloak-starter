using System.Text.Json.Serialization;

namespace Sample.Api.DTO;

public class FruitRequest
{
    [JsonPropertyName("name")] public string Name { get; set; }
    [JsonPropertyName("description")] public string Description { get; set; }
    [JsonPropertyName("class")] public string Class { get; set; }
}
