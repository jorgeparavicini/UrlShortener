namespace UrlShortener.Service.Entities;

public class ShortenedAddress
{
    public int Id { get; init; }
    public required string ShortAddress { get; init; }
    public required string FullAddress { get; init; }
}

