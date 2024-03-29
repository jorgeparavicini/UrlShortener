﻿using System.Text.Json;

namespace UrlShortener.Ui.Data;

public class StatisticService
{
    private readonly HttpClient _httpClient;
    private readonly IConfiguration _configuration;

    public StatisticService(HttpClient httpClient, IConfiguration configuration)
    {
        _httpClient = httpClient;
        _configuration = configuration;
    }

    public async Task<Statistic?> GetStatisticAsync(string shortId)
    {
        var baseUrl = _configuration.GetConnectionString("apiService");
        var request = new HttpRequestMessage(HttpMethod.Get,
            $"{baseUrl}/Shortener/statistics/{shortId}");
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
