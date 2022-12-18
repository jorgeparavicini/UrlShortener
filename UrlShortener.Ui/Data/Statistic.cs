using System.Text.Json.Serialization;

namespace UrlShortener.Ui.Data;

public class Statistic
{
    [JsonPropertyName("count")]
    public required int Count { get; init; }
}
