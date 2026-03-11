using System.Collections.Generic;
using System.Threading.Tasks;
using FluentAssertions;
using Moq;
using WeatherForecast.Application.DTOs;
using WeatherForecast.Application.Interfaces;
using WeatherForecast.Application.Services;
using WeatherForecast.Domain.Entities;
using Xunit;

namespace WeatherForecast.UnitTests;

public class WeatherServiceTests
{
    private readonly Mock<IWeatherRepository> _repoMock = new();

    [Fact]
    public async Task GetForecastsAsync_ReturnsMappedDtos()
    {
        var entities = new List<WeatherForecast>
        {
            new() { Date = new DateOnly(2026, 3, 12), TemperatureC = 20, Summary = "Warm" },
            new() { Date = new DateOnly(2026, 3, 13), TemperatureC = -5, Summary = "Freezing" }
        };

        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var service = new WeatherService(_repoMock.Object);
        var result = await service.GetForecastsAsync();

        result.Should().HaveCount(2);
        result.Should().ContainSingle(d => d.Summary == "Warm" && d.TemperatureC == 20);
        result.Should().ContainSingle(d => d.Summary == "Freezing" && d.TemperatureC == -5);
    }

    [Fact]
    public async Task GetForecastsAsync_CalculatesTemperatureFCorrectly()
    {
        var entities = new List<WeatherForecast>
        {
            new() { Date = new DateOnly(2026, 3, 12), TemperatureC = 0, Summary = "Freezing" }
        };

        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(entities);

        var service = new WeatherService(_repoMock.Object);
        var result = (await service.GetForecastsAsync()).ToList();

        result[0].TemperatureF.Should().Be(32);
    }

    [Fact]
    public async Task GetForecastsAsync_ReturnsEmptyWhenNoData()
    {
        _repoMock.Setup(r => r.GetAllAsync()).ReturnsAsync(new List<WeatherForecast>());

        var service = new WeatherService(_repoMock.Object);
        var result = await service.GetForecastsAsync();

        result.Should().BeEmpty();
    }
}
