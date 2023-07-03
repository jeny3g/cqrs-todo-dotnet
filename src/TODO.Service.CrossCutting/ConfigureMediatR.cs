using System.Diagnostics.CodeAnalysis;
using Todo.Service.Application.WeatherForecast.Queries.List;

[assembly: ExcludeFromCodeCoverage]
namespace Todo.Service.CrossCutting;

public static class ConfigureMediatR
{
    public static IServiceCollection InjectMediatR(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<ListWeatherForecastsQuery>());

        return services;
    }
}
