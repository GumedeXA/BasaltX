using BasaltX.Utils;
using StackExchange.Redis;
using Microsoft.Extensions.Configuration;
using BasaltX.LB.BL.Features.Get.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using BasaltX.LB.BL.Features.Get.Implementation;
using BasaltX.LB.BL.Features.Caching.Implementation.Redis.Extenstions;
using Microsoft.AspNetCore.OutputCaching;

namespace BasaltX.LB.BL;

/// <summary>
/// The basaltX local business module.
/// </summary>
public static class BasaltXLocalBusinessModule
{
    /// <summary>
    /// The already added.
    /// </summary>
    private static bool _alreadyAdded;

    /// <summary>
    /// Add LB module collection.
    /// </summary>
    /// <param name="services">The services.</param>
    /// <returns>An IServiceCollection</returns>
    public static IServiceCollection AddLBModuleCollection(this IServiceCollection services, IConfiguration configuration)
    {
        if (_alreadyAdded) return services;

        //Register Redis Exchange
        var redisUrl = configuration.GetSection("AppConfiguration").GetValue<string>("RedisUrl");
        services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(redisUrl));

        //Register Redis Cache configurations for querying the same data
        services.AddRedisOutputCache();

        //Register the utils services for consumption
        services.AddUtilitiesModuleCollection();
        services.AddSingleton<ILocalBusinessService, LocalBusinessService>();

        _alreadyAdded = true;

        return services;
    }
}
