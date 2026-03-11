using Microsoft.EntityFrameworkCore;
using WeatherForecast.Application.Services;
using WeatherForecast.Domain.Entities;
using WeatherForecast.Infrastructure.Persistence;

namespace WeatherForecast.Infrastructure.Repositories;

public class WeatherRepository : IWeatherRepository
{
    private readonly WeatherDbContext _context;

    public WeatherRepository(WeatherDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<WeatherForecast>> GetAllAsync()
    {
        return await _context.WeatherForecasts.AsNoTracking().ToListAsync();
    }
}
