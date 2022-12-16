namespace UrlShortener.API.Services;

public class ShortenerService : IShortenerService
{
    private readonly Statistics.StatisticsClient _statisticsClient;
    private readonly Shortener.ShortenerClient _shortenerClient;


    public ShortenerService(
        Statistics.StatisticsClient statisticsClient,
        Shortener.ShortenerClient shortenerClient)
    {
        _statisticsClient = statisticsClient;
        _shortenerClient = shortenerClient;
    }

    public async Task<string?> ResolveAddress(string shortAddress)
    {
        var reply = await _shortenerClient.ResolveShortAddressAsync(new ResolveShortAddressRequest
        {
            ShortAddress = shortAddress
        });

        await _statisticsClient.IncreaseClickCountAsync(new IncreaseClickCountRequest
        {
            ShortAddress = shortAddress
        });

        return string.IsNullOrWhiteSpace(reply.FullAddress) ? null : reply.FullAddress;
    }

    public async Task<string> ShortenAddress(string fullAddress)
    {
        var reply = await _shortenerClient.ShortenAddressAsync(new ShortenAddressRequest
        {
            FullAddress = fullAddress
        });
        return reply.ShortAddress;
    }

    public async Task<int?> GetClickCount(string shortAddress)
    {
        var reply = await _statisticsClient.GetClickCountAsync(new GetClickCountRequest
        {
            ShortAddress = shortAddress
        });

        return reply.ClickCount < 0 ? null : reply.ClickCount;
    }
}
