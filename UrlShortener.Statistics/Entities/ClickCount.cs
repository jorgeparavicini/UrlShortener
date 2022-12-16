namespace UrlShortener.Statistics.Entities;

public class ClickCount
{
    public int Id { get; init; }
    public required string ShortId { get; init; }
    public required int Count { get; set; }
}
