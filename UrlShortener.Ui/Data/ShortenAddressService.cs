using System.Text.Json;

namespace UrlShortener.Ui.Data;

public class ShortenAddressService
{
    private readonly HttpClient _httpClient;

    public ShortenAddressService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<ShortenedAddress?> ShortenAddress(string fullAddress)
    {
        var request = new HttpRequestMessage(HttpMethod.Post, "http://urlshortener.api/Shortener");
        request.Content = JsonContent.Create(new ShortenAddressRequest
        {
            FullAddress = fullAddress
        });

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            Console.WriteLine(response);
            return null;
        }

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<ShortenedAddress>(responseStream);
    }
}
