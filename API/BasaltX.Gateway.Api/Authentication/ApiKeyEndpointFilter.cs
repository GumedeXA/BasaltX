using Microsoft.Extensions.Options;
using BasaltX.Gateway.Api.Models.Constants;
using BasalX.Service.Agents.Models.Settings;
using BasaltX.Common.Models.Models.DTO.Response;

namespace BasaltX.Gateway.Api.Authentication
{
    /// <summary>
    /// This class will handle authentication on the single gateway api endpoint
    /// To make sure that only authorized calls a permitted
    /// </summary>
    public class ApiKeyEndpointFilter : IEndpointFilter
    {
        #region Private Members
        private readonly InternalApiSettings _settings;
        #endregion Private Members

        public ApiKeyEndpointFilter(IOptions<InternalApiSettings> _settings)
        {
            this._settings = _settings.Value;
        }

        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context,
            EndpointFilterDelegate next)
        {
            if (!context.HttpContext.Request.Headers.TryGetValue(AuthConstants.ApiKeyHeaderName,
                out var extractedApiKey))
            {
                return new ResponseData
                {
                    Status = System.Net.HttpStatusCode.Unauthorized,
                    ResponsePayload = "Api key missing."
                };
            }
            if (!_settings.APIKey.Equals(extractedApiKey))
            {
                return new ResponseData
                {
                    Status = System.Net.HttpStatusCode.Unauthorized,
                    ResponsePayload = "Invalid Api key."
                };
            }
            return await next(context);
        }
    }
}
