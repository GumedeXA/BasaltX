using System.Globalization;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BasaltX.Models.Models.Settings;
using BasaltX.AI.BL.Features.Interfaces;
using BasaltX.Common.Models.Models.Weather;
using BasaltX.AI.BL.Features.Models.Constants;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.Utils.Features.Generics.Interfaces;
using BasaltX.Utils.Features.RestOrchestrator.Interface;

namespace BasaltX.AI.BL.Features.Implementation;

/// <summary>
/// This service class will handle all data related places and search of the weather for those places
/// </summary>
internal class WeatherService : IWeatherService
{
    #region Private Members
    /// <summary>
    /// The generics.
    /// </summary>
    private readonly IGenerics _generics;
    /// <summary>
    /// The rest agent.
    /// </summary>
    private readonly IRestAgent _restAgent;
    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<WeatherService> _logger;
    /// <summary>
    /// The rapi api settings.
    /// </summary>
    private readonly RapidApiSettings _rapiApiSettings;
    #endregion Private Members

    #region Constructor(s)
    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherService"/> class.
    /// </summary>
    /// <param name="_restAgent">The rest agent.</param>
    /// <param name="_generics">The generics.</param>
    /// <param name="_logger">The logger.</param>
    /// <param name="_rapiApiSettings">The rapi api settings.</param>
    public WeatherService(IRestAgent _restAgent, IGenerics _generics,
        ILogger<WeatherService> _logger, IOptions<RapidApiSettings> _rapiApiSettings)
    {
        this._logger = _logger;
        this._generics = _generics;
        this._restAgent = _restAgent;
        this._rapiApiSettings = _rapiApiSettings.Value;
    }
    #endregion Constructor(s)

    #region Public Methods
    /// <summary>
    /// This functions search a place abd returns the areas within that specific place
    /// This will be useful to dive deeper on finding the weather and business review of that specific
    /// place for the later integration
    /// </summary>
    /// <param name="placeName"></param>
    /// <returns></returns>
    public async Task<ResponseData> FindPlacesAsync(string placeName)
    {

        try
        {
            var headers = ConstructCommonHeaders();

            //Make a request to find the place coordinates to check the weather
            var findPlaceResponse = await _restAgent.SendRequestAsync($"{_rapiApiSettings.RapidAPIEndpoint}",
                                                                        $"/find_places?text={placeName}",
                                                                        RestSharp.Method.Get, null, extraHeaders: headers);
          return findPlaceResponse;
        }
        catch (Exception exception)
        {

            _logger.LogError($"Exception : '{exception.Message}' - '{Environment.NewLine}' Inner Exception " +
                               $": '{(exception.InnerException != null ? exception.InnerException.Message : string.Empty)}'");
            return new ResponseData
            {
                Status = System.Net.HttpStatusCode.InternalServerError,
                ResponsePayload = _generics.HandleGenericResponse(ErrorMessages.FailedToGetPlaces)
            };
        }
    }

    /// <summary>
    /// This function gets the current weather of the place being search on
    /// </summary>
    /// <param name="getPlaceWeatherDetails">Request payload of longitude and latitude data</param>
    /// <returns></returns>
    public async Task<ResponseData> GetPlaceCurrentWeatherAsync(GetPlaceWeatherDetails getPlaceWeatherDetails)
    {
        try
        {
            var headers = ConstructCommonHeaders();

            // Convert the latitude and longitude to strings using InvariantCulture
            string lat = getPlaceWeatherDetails.lat.ToString(CultureInfo.InvariantCulture);
            string lon = getPlaceWeatherDetails.lon.ToString(CultureInfo.InvariantCulture);

            var weatherResponse = await _restAgent.SendRequestAsync($"{_rapiApiSettings.RapidAPIEndpoint}",
                                                                   $"/current?lat={lat}&lon={lon}",
                                                                    RestSharp.Method.Get, null, extraHeaders: headers);
            return weatherResponse;
        }
        catch (Exception exception)
        {

            _logger.LogError($"Exception : '{exception.Message}' - '{Environment.NewLine}' Inner Exception " +
                               $": '{(exception.InnerException != null ? exception.InnerException.Message : string.Empty)}'");
            return new ResponseData
            {
                Status = System.Net.HttpStatusCode.InternalServerError,
                ResponsePayload = _generics.HandleGenericResponse(ErrorMessages.FailedToGetWeather)
            };
        }
    }
    #endregion Public Methods

    #region Encapsulation

    /// <summary>
    /// Constructs common headers.
    /// </summary>
    /// <returns><![CDATA[Dictionary<string, string>]]></returns>
    private Dictionary<string, string> ConstructCommonHeaders()
    {
        return new Dictionary<string, string>
                {
                    { "X-RapidAPI-Key", _rapiApiSettings.RapidAPIKey },
                    { "X-RapidAPI-Host", _rapiApiSettings.RapiAPIHost },
                };
    }
    #endregion Encapsulation
}
