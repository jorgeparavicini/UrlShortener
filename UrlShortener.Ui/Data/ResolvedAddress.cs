using System.Text.Json.Serialization;

namespace UrlShortener.Ui.Data;

public class ResolvedAddress
{
    [JsonPropertyName("fullAddress")]
    public required string FullAddress { get; init; }
}
