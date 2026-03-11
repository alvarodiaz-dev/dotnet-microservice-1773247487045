using Microsoft.EntityFrameworkCore;
using WeatherForecast.Domain.Entities;

namespace WeatherForecast.Infrastructure.Persistence;

public class WeatherDbContext : DbContext
{
    public WeatherDbContext(DbContextOptions<WeatherDbContext> options) : base(options) { }

    public DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<WeatherForecast>().HasKey(w => w.Date);

        modelBuilder.Entity<WeatherForecast>().HasData(
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(1)), TemperatureC = 15, Summary = "Mild" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(2)), TemperatureC = 22, Summary = "Warm" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(3)), TemperatureC = -3, Summary = "Freezing" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(4)), TemperatureC = 8,  Summary = "Cool" },
            new WeatherForecast { Date = DateOnly.FromDateTime(DateTime.Today.AddDays(5)), TemperatureC = 30, Summary = "Hot" }
        );
    }
}
