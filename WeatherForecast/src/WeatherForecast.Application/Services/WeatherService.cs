using WeatherForecast.Application.DTOs;
using WeatherForecast.Application.Interfaces;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Application.Services;

public interface IWeatherRepository
{
    Task<IEnumerable<WeatherForecast>> GetAllAsync();
}

public class WeatherService : IWeatherService
{
    private readonly IWeatherRepository _repository;

    public WeatherService(IWeatherRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<WeatherForecastDto>> GetForecastsAsync()
    {
        var forecasts = await _repository.GetAllAsync();
        return forecasts.Select(f => new WeatherForecastDto(
            f.Date,
            f.TemperatureC,
            f.TemperatureF,
            f.Summary
        ));
    }
}
