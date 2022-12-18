using System.Text.Json;

namespace UrlShortener.Ui.Data;

public class StatisticService
{
    private readonly HttpClient _httpClient;

    public StatisticService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<Statistic?> GetStatisticAsync(string shortId)
    {
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"http://urlshortener.api/Shortener/statistics/{shortId}");
        request.Headers.Add("Accept", "application/json");

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            return new Statistic
            {
                Count = -1
            };
        }

        await using var responseStream = await response.Content.ReadAsStreamAsync();
        return await JsonSerializer.DeserializeAsync<Statistic>(responseStream);
    }
}
