using Microsoft.AspNetCore.OutputCaching;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace BasaltX.LB.BL.Features.Caching.Implementation.Redis.Extenstions
{
    /// <summary>
    /// The redis output cache service extensions.
    /// </summary>
    public static class RedisOutputCacheServiceExtensions
    {
        /// <summary>
        /// Add redis output cache.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection</returns>
        public static IServiceCollection AddRedisOutputCache(this IServiceCollection services)
        {
            ArgumentNullException.ThrowIfNull(services);

            services.AddOutputCache();
            services.RemoveAll<IOutputCacheStore>();
            services.AddSingleton<IOutputCacheStore, RedisOutputCacheStore>();
            return services;
        }

        /// <summary>
        /// Add redis output cache with options.
        /// </summary>
        /// <param name="services">The services.</param>
        /// <returns>An IServiceCollection</returns>
        public static IServiceCollection AddRedisOutputCache(this IServiceCollection services,
            Action<OutputCacheOptions> configurationOptions)
        {
            ArgumentNullException.ThrowIfNull(services);
            ArgumentNullException.ThrowIfNull(configurationOptions);

            services.Configure(configurationOptions);
            services.AddOutputCache();

            services.RemoveAll<IOutputCacheStore>();
            services.AddSingleton<IOutputCacheStore, RedisOutputCacheStore>();

            return services;
        }
    }
}
