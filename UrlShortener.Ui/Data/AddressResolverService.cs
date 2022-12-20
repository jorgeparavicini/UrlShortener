using System.Text.Json;

namespace UrlShortener.Ui.Data;

public class AddressResolverService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public AddressResolverService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ResolvedAddress?> ResolveAddress(string shortId)
    {
        var baseUrl = _configuration.GetConnectionString("apiService");
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{baseUrl}/Shortener/{shortId}");
        request.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            return null;
        }

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<ResolvedAddress>(responseStream);
    }
}
