using WeatherForecast.Application.DTOs;

namespace WeatherForecast.Application.Interfaces;

public interface IWeatherService
{
    Task<IEnumerable<WeatherForecastDto>> GetForecastsAsync();
}
