namespace UrlShortener.API.Services;

public interface IShortenerService
{
    Task<string?> ResolveAddress(string shortAddress);
    Task<string> ShortenAddress(string fullAddress);
    Task<int?> GetClickCount(string shortAddress);
}
