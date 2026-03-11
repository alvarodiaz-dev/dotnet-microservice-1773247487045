using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using WeatherForecast.Application.DTOs;
using Xunit;

namespace WeatherForecast.IntegrationTests;

public class WeatherEndpointTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public WeatherEndpointTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GET_Weather_Returns200WithForecasts()
    {
        var response = await _client.GetAsync("/weather");

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var forecasts = await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecastDto>>();
        forecasts.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GET_Weather_ReturnsForecastsWithValidData()
    {
        var response = await _client.GetAsync("/weather");
        var forecasts = (await response.Content.ReadFromJsonAsync<IEnumerable<WeatherForecastDto>>())!.ToList();

        forecasts.Should().OnlyContain(f => f.Summary != null);
        forecasts.Should().OnlyContain(f => f.TemperatureF == 32 + (int)(f.TemperatureC / 0.5556));
    }
}
