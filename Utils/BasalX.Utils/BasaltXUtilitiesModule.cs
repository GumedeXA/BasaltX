using Microsoft.Extensions.DependencyInjection;
using BasaltX.Utils.Features.Generics.Interfaces;
using BasaltX.Utils.Features.Generics.Implementation;
using BasaltX.Utils.Features.RestOrchestrator.Interface;
using BasaltX.Utils.Features.RestOrchestrator.Implementation;

namespace BasaltX.Utils;

/// <summary>
/// DI Registration of our services as a package
/// </summary>
public static class UtilitiesModule
{
    private static bool _alreadyAdded;

    public static IServiceCollection AddUtilitiesModuleCollection(this IServiceCollection services)
    {
        if (_alreadyAdded) return services;

        services.AddSingleton<IGenerics, Generics>();
        services.AddSingleton<IRestAgent, RestAgent>();

        _alreadyAdded = true;

        return services;
    }
}
