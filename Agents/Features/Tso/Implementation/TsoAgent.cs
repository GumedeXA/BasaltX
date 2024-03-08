using RestSharp;
using System.Net;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Logging;
using BasaltX.Models.Models.Constants;
using BasalX.Service.Agents.Models.DTO;
using BasalX.Service.Agents.Models.Settings;
using BasalX.Service.Agents.Models.Responses;
using BasaltX.Common.Models.Models.DTO.Request;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.Common.Models.Models.Infastructure;
using BasaltX.Utils.Features.Generics.Interfaces;
using BasaltX.Common.Models.Models.Constants.Enums;
using BasaltX.Utils.Features.RestOrchestrator.Interface;

namespace BasalX.Service.Agents.Features.AIWeather.Implementation;

/// <summary>
/// The tso agent.
/// </summary>
public class TsoAgent : Request
{
    #region Private Memebers
    /// <summary>
    /// The rest agent.
    /// </summary>
    private readonly IRestAgent _restAgent;
    /// <summary>
    /// The generics.
    /// </summary>
    private readonly IGenerics _generics;
    /// <summary>
    /// The settings.
    /// </summary>
    private readonly InternalApiSettings _settings;

    /// <summary>
    /// The logger.
    /// </summary>
    private readonly ILogger<TsoAgent> _logger;
    #endregion Private Memebers

    #region Constructors
    /// <summary>
    /// Initializes a new instance of the <see cref="TsoAgent"/> class.
    /// </summary>
    /// <param name="_restAgent">The rest agent.</param>
    /// <param name="_settings">The settings.</param>
    /// <param name="_generics">The generics.</param>
    public TsoAgent(IRestAgent _restAgent, IOptions<InternalApiSettings> _settings, IGenerics _generics,
        ILogger<TsoAgent> _logger)
    {
        this._logger = _logger;
        this._restAgent = _restAgent;
        this._settings = _settings.Value;
        this._generics = _generics;
    }
    #endregion Constructors

    #region Public Methods
    /// <summary>
    /// Process the request asynchronously.
    /// </summary>
    /// <param name="requestData">The request data.</param>
    /// <returns><![CDATA[Task<ResponseData>]]></returns>
    public override async Task<ResponseData> ProcessRequestAsync(RequestData requestData)
    {
        return requestData.Action switch
        {
            ActionType.Get => await GetLocalBusinessSummaryDetails(requestData),

            _ => new ResponseData
            {
                Status = HttpStatusCode.BadRequest,
                ResponsePayload = ErrorMessages.InvalidRequest
            }
        };
    }

    #endregion Public Methods

    #region Private Methods
    /// <summary>
    /// Constructs find place query.
    /// </summary>
    /// <param name="placeName">The place name.</param>
    /// <returns>A string</returns>
    private static string ConstructFindPlaceQuery(string placeName)
    {
        return $"api/find-place?place_name={placeName}";
    }

    /// <summary>
    /// Constructs weather request query.
    /// </summary>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <returns>A string</returns>
    private static string ConstructWeatherRequestQuery(string latitude, string longitude)
    {
        return $"api/get-current-weather?lat={latitude}&lon={longitude}";
    }

    /// <summary>
    /// Get local business summary details.
    /// </summary>
    /// <param name="requestData">The request data.</param>
    /// <returns><![CDATA[Task<ResponseData>]]></returns>
    private async Task<ResponseData> GetLocalBusinessSummaryDetails(RequestData requestData)
    {
        try
        {
            if (requestData.Payload is null)
            {
                return new ResponseData
                {
                    Status = HttpStatusCode.BadRequest,
                    ResponsePayload = ErrorMessages.InvalidRequest
                };
            }
            var summaryData = await ConstructResponseSummary(requestData);

            if (summaryData.Count == 0)
            {
                return new ResponseData { Status = HttpStatusCode.OK, ResponsePayload = "Failed to get information" };
            }
            return new ResponseData { Status = HttpStatusCode.OK, ResponsePayload = summaryData };
        }
        catch (Exception ex)
        {
            return new ResponseData
            {
                Status = HttpStatusCode.OK,
                ResponsePayload = !string.IsNullOrWhiteSpace(ex.Message) ? ex.Message : "Failed to retrieve data"
            };
        }
    }

    /// <summary>
    /// This function sends request sent through the gatewap api
    /// Than construct the response back to the gateway api to service the client api call
    /// </summary>
    /// <param name="requestData">The request Payload</param>
    /// <returns>Returns the summary of the local businesses search as well as the weather around those businesses</returns>
    private async Task<List<LocalBusinessSummaryDetailsResponse>> ConstructResponseSummary(RequestData requestData)
    {
        var searchPlaceRequest = JsonSerializer.Deserialize<SearchPlaceRequest>(JsonSerializer
                    .Serialize(requestData?.Payload));

        if (searchPlaceRequest is not null)
        {
            var getPlacesDetails = await _restAgent
                                        .SendRequestAsync(_settings.AIEndpoint,
                                        ConstructFindPlaceQuery(searchPlaceRequest.location).ToString(), Method.Get);



            string data = _generics.Serialize(getPlacesDetails?.ResponsePayload);

            var getPlaceWeatherDetails = _generics.Deserialize<PlacesResponseCollection>(data);
            foreach (var place in getPlaceWeatherDetails.responsePayload)
            {
                //Remove the W(West) and N(North) from the latitude and longitude values
                string latitude = place.lat.Substring(0, place.lat.Length - 1);
                string longitude = place.lon.Substring(0, place.lon.Length - 1);

                //Get the Weather for the searched place by using the longitude and latitude
                var response = await _restAgent
                                .SendRequestAsync(_settings.AIEndpoint,
                                ConstructWeatherRequestQuery(latitude, longitude).ToString(), Method.Get);

                string serializeWeatherResponse = _generics.Serialize(response?.ResponsePayload);

                if (!string.IsNullOrWhiteSpace(serializeWeatherResponse))
                {
                  return  await LocalBusinessApiSummaryDetailsResponsesAsync(searchPlaceRequest,
                                    serializeWeatherResponse, latitude, longitude, place);
                }
            }
        }

        return [];
    }

    /// <summary>
    /// Locals business summary details responses asynchronously.
    /// </summary>
    /// <param name="searchPlaceRequest">The search place request.</param>
    /// <param name="serializeWeatherResponse">The serialize weather response.</param>
    /// <param name="latitude">The latitude.</param>
    /// <param name="longitude">The longitude.</param>
    /// <param name="place">The place.</param>
    /// <returns><![CDATA[Task<List<LocalBusinessSummaryDetailsResponse>>]]></returns>
    private async Task<List<LocalBusinessSummaryDetailsResponse>> LocalBusinessApiSummaryDetailsResponsesAsync(SearchPlaceRequest searchPlaceRequest,
        string serializeWeatherResponse, string latitude, string longitude, PlacesResponse place)
    {
        var localBusinessSummaryDetails = new List<LocalBusinessSummaryDetailsResponse>();

        //Get the local business service you looking for the searched place or area
        string queryString = $"api/search-nearby-area?query={searchPlaceRequest.search_service_type}&lat={latitude}&lon={longitude}";

        var searchNearByBusinessService = await _restAgent
                        .SendRequestAsync(_settings.LocalBusinessEndpoint, queryString.ToString(), Method.Get);

        try
        {
            WeatherResponse getWeatherBylocation = _generics
                   .Deserialize<WeatherResponse>(serializeWeatherResponse);

            LocalBusinessesDataResponse nearByBusinessList = _generics.Deserialize<LocalBusinessesDataResponse>(_generics
              .Serialize(searchNearByBusinessService.ResponsePayload));

            if (nearByBusinessList.responsePayload is not null)
            {
                //Construct the summary data for the search place by return the nearby area for the search place
                //Aswell as the current weather for that place or location
                foreach (var business in nearByBusinessList.responsePayload.data)
                {

                    localBusinessSummaryDetails.Add(new LocalBusinessSummaryDetailsResponse
                    {
                        type = place.type,
                        name = place.name,
                        website = business?.website,
                        full_address = business?.full_address,
                        place_link = business?.place_link,
                        current_weather = new()
                        {
                            humidity = getWeatherBylocation.responsePayload.current.humidity,
                            summary = getWeatherBylocation.responsePayload.current.summary,
                            temperature = getWeatherBylocation.responsePayload.current.temperature
                        },
                        working_hours = new()
                        {
                            Monday = business?.working_hours?.Monday,
                            Tuesday = business?.working_hours?.Tuesday,
                            Wednesday = business?.working_hours?.Wednesday,
                            Thursday = business?.working_hours?.Thursday,
                            Friday = business?.working_hours?.Friday,
                            Saturday = business?.working_hours?.Saturday,
                            Sunday = business?.working_hours?.Sunday
                        }
                    });
                }
            }
            else
            {
                _logger.LogError("Failed to retrieve local business data.");
            }
        }
        catch (Exception exception)
        {
            _logger.LogError($"{nameof(ConstructResponseSummary)} :-> {exception}");
        }
        return localBusinessSummaryDetails;
    }
    #endregion Private Methods
}

