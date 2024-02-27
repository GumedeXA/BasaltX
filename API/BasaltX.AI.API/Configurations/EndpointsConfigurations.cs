using Microsoft.AspNetCore.Mvc;
using BasaltX.AI.Api.Constants;
using BasaltX.AI.BL.Features.Interfaces;
using BasaltX.Common.Models.Models.Weather;

namespace BasaltX.AI.Api.Configurations
{
    internal static class EndpointsConfigurations
    {
        internal static void AddEndPointsConfiguration(this WebApplication app)
        {
            _ = app.MapGet(pattern: Routes.FindPlace, async (IWeatherService _proccessAIRequest, [FromQuery] string place_name)
            =>
            {
                return await _proccessAIRequest.FindPlacesAsync(place_name).ConfigureAwait(false);

            });

            _ = app.MapGet(pattern: Routes.GetCurrentWeather, async (IWeatherService _proccessAIRequest, [AsParameters] GetPlaceWeatherDetails weather_request)
                        =>
            {
                return await _proccessAIRequest.GetPlaceCurrentWeatherAsync(weather_request).ConfigureAwait(false);

            });
        }
    }
}
