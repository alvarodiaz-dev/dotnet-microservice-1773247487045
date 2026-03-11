using Microsoft.AspNetCore.Mvc;
using WeatherForecast.Application.DTOs;
using WeatherForecast.Application.Interfaces;

namespace WeatherForecast.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherController : ControllerBase
{
    private readonly IWeatherService _weatherService;

    public WeatherController(IWeatherService weatherService)
    {
        _weatherService = weatherService;
    }

    /// <summary>Returns a 5-day weather forecast.</summary>
    [HttpGet(Name = "GetForecasts")]
    [ProducesResponseType(typeof(IEnumerable<WeatherForecastDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetForecasts()
    {
        var forecasts = await _weatherService.GetForecastsAsync();
        return Ok(forecasts);
    }
}
