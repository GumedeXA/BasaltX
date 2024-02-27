using BasaltX.Utils;
using BasaltX.AI.BL.Features.Interfaces;
using BasaltX.AI.BL.Features.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace BasaltX.AI.BL
{
    public static class BasaltXAIModule
    {
        private static bool _alreadyAdded;

        public static IServiceCollection AddAIModuleCollection(this IServiceCollection services)
        {
            if (_alreadyAdded) return services;

            //Register the utils services for consumption
            services.AddUtilitiesModuleCollection();
            services.AddSingleton<IWeatherService, WeatherService>();

            _alreadyAdded = true;

            return services;
        }
    }
}
