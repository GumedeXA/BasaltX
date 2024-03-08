using System.Net;
using Microsoft.AspNetCore.Mvc;
using BasaltX.Models.Models.Constants;
using BasaltX.Gateway.Api.RequestManager;
using BasaltX.Common.Models.Models.DTO.Request;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.Common.Models.Models.Constants.Enums;
using BasaltX.Utils.Features.Generics.Interfaces;
using BasalX.Service.Agents.Features.AIWeather.Implementation;
using BasaltX.Gateway.Api.Authentication;

namespace BasaltX.Gateway.Api.Configurations
{
    /// <summary>
    /// The gateway api endpoints configurations.
    /// </summary>
    internal static class GatewayApiEndpointsConfigurations
    {
        /// <summary>
        /// Add end points configuration.
        /// </summary>
        /// <param name="app">The app.</param>
        internal static void AddEndPointsConfiguration(this WebApplication app)
        {
            _ = app.MapPost(pattern: "api/gate-way", async ([FromBody] RequestData? requestData,
                TsoAgent _aiTsoAgent,
                IGenerics _generics)
            =>
            {
                try
                {
                    if (requestData is null
                        || requestData.Action == ActionType.NotSpecified
                        || requestData.Service == ServiceType.NotSpecified)
                    {

                        return new ResponseData { ResponsePayload = _generics.HandleGenericResponse(ErrorMessages.InvalidRequest), Status = HttpStatusCode.BadRequest };
                    }

                    return await GatewayApiManager.GetInstance(_aiTsoAgent).ProcessRequestAsync(requestData).ConfigureAwait(false);
                }
                catch
                {
                    return new ResponseData { ResponsePayload = _generics.HandleGenericResponse(ErrorMessages.InternalErrorOccurred), Status = HttpStatusCode.InternalServerError };
                }
            }).AddEndpointFilter<ApiKeyEndpointFilter>();

        }
    }
}
