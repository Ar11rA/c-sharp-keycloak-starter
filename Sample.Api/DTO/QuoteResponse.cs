using System.Text.Json.Serialization;

namespace Sample.Api.DTO;

public class QuoteResponse
{
    [JsonPropertyName("content")]
    public string Content { get; set; }

    [JsonPropertyName("author")]
    public string Author { get; set; }

    [JsonPropertyName("dateAdded")]
    public DateTime DateAdded { get; set; }

    [JsonPropertyName("dateModified")]
    public DateTime DateModified { get; set; }
}
