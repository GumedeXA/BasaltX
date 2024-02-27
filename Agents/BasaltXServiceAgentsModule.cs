using Microsoft.Extensions.DependencyInjection;
using BasalX.Service.Agents.Features.AIWeather.Implementation;

namespace BasalX.Service.Agents;

public static class BasaltXServiceAgentsModule
{
    private static bool _alreadyAdded;

    public static IServiceCollection AddBasaltXServiceAgentsModuleCollection(this IServiceCollection services)
    {
        if (_alreadyAdded) return services;

        services.AddSingleton<TsoAgent>();

        _alreadyAdded = true;

        return services;
    }
}
