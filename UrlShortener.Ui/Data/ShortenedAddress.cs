using System.Text.Json.Serialization;

namespace UrlShortener.Ui.Data;

public class ShortenedAddress
{
    [JsonPropertyName("shortId")]
    public required string ShortId { get; init; }
}
