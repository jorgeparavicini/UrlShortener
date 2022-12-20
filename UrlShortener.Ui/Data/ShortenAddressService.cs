using System.Text.Json;

namespace UrlShortener.Ui.Data;

public class ShortenAddressService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public ShortenAddressService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<ShortenedAddress?> ShortenAddress(string fullAddress)
    {
        var baseUrl = _configuration.GetConnectionString("apiService");
        var request = new HttpRequestMessage(HttpMethod.Post,
            $"{baseUrl}/Shortener");
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
