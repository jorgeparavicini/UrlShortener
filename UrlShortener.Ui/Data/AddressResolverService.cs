using System.Text.Json;

namespace UrlShortener.Ui.Data;

public class AddressResolverService
{
    private readonly HttpClient _httpClient;

    public AddressResolverService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ResolvedAddress?> ResolveAddress(string shortId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://urlshortener.api/Shortener/{shortId}");
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
