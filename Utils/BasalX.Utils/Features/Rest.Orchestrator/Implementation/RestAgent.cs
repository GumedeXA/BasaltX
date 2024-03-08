using RestSharp;
using System.Net;
using Microsoft.Extensions.Logging;
using BasaltX.Models.Models.Constants;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.Utils.Features.Generics.Interfaces;
using BasaltX.Utils.Features.RestOrchestrator.Interface;
using BasaltX.Utils.Features.Rest.Orchestrator.Private.Models.Constants;

namespace BasaltX.Utils.Features.RestOrchestrator.Implementation;

/// <summary>
/// This service class is responsible to handle all our REST calls inside and outside
/// It provides one central control of our api calls and also a linear way of handling the 
/// responses
/// </summary>
internal class RestAgent : IRestAgent
{
    #region Private Members

    /// <summary>
    /// The generics.
    /// </summary>
    private readonly IGenerics _generics;
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<RestAgent> _logger;

    #endregion Private Members

    #region Constructor(s)
    /// <summary>
    /// Initializes a new instance of the <see cref="RestAgent"/> class.
    /// </summary>
    /// <param name="_logger">The logger.</param>
    /// <param name="_generics">The generics.</param>
    public RestAgent(ILogger<RestAgent> _logger, IGenerics _generics)
    {
        this._logger = _logger;
        this._generics = _generics;
    }
    #endregion Constructor(s)

    #region Public Methods
    /// <summary>
    /// This method will handle all rest calls to api's
    /// </summary>
    /// <param name="apiUrl">The enpoint url</param>
    /// <param name="path">The endpoint attribute route</param>
    /// <param name="methodType">POST,GET,PUT etc</param>
    /// <param name="payload">The Request object</param>
    /// <param name="extraHeaders">All extra headers like bearer token etc</param>
    /// <param name="contentNegotiation">JSON,XML by default it json</param>
    /// <returns></returns>
    public async Task<ResponseData> SendRequestAsync(string apiUrl, string path, Method methodType,
        object? payload = null, IDictionary<string, string>? extraHeaders = null, string? contentNegotiation = null)
    {
        try
        {
            var restRequest = new RestRequest(path, methodType);

            string cNegotiation = ContentNegotiationTypes.Json;

            if (!string.IsNullOrWhiteSpace(contentNegotiation))
            {
                switch (contentNegotiation)
                {
                    case "Urlencoded":
                        cNegotiation = ContentNegotiationTypes.Urlencoded;
                        break;
                    case "xml":
                        cNegotiation = ContentNegotiationTypes.Xml;
                        break;
                }
            }

            restRequest.AddHeader("Accept", cNegotiation);
            restRequest.AddHeader("Content-Type", cNegotiation);


            if (extraHeaders is not null)
            {
                foreach (var header in extraHeaders)
                {
                    restRequest.AddHeader(header.Key, header.Value);
                }
            }
            if (payload is not null)
            {
                restRequest.AddObject(payload);

                if (cNegotiation == ContentNegotiationTypes.Urlencoded)
                {
                    restRequest.AddObject(payload);
                }
                else
                {
                    var jsonPayload = _generics.Serialize(payload);
                    restRequest.AddBody(jsonPayload, cNegotiation);
                }

                _logger.LogInformation($"Sending Request to endpoint:-> {apiUrl}");
            }
            return await ProcessRestCall(apiUrl, restRequest);
        }
        catch (Exception e)
        {
            _logger.LogError(e.Message, e);

            //Let handle the possible error's for the consuming application
            var errorMessage = !string.IsNullOrWhiteSpace(e.Message) ? e.Message : _generics.HandleGenericResponse(ErrorMessages.InternalErrorOccurred);

            return new() { ResponsePayload = errorMessage, Status = HttpStatusCode.InternalServerError };
        }
    }

    #endregion Public Methods

    #region Encapsulation

    /// <summary>
    /// Processes rest call.
    /// </summary>
    /// <param name="apiUrl">The api url.</param>
    /// <param name="restRequest">The rest request.</param>
    /// <returns><![CDATA[Task<ResponseData>]]></returns>
    private async Task<ResponseData> ProcessRestCall(string apiUrl, RestRequest restRequest)
    {
        var restClient = new RestClient(new RestClientOptions
        {
            BaseUrl = new(apiUrl)
        });

        var response = await restClient.ExecuteAsync<ResponseData>(restRequest);
        return !string.IsNullOrWhiteSpace(response.Content)
                ? new ResponseData() { Status = response.StatusCode, ResponsePayload = _generics.Deserialize<dynamic>(response.Content) }
                : new ResponseData() { Status = response.StatusCode, ResponsePayload = response };
    }



    #endregion Encapsulation
}
