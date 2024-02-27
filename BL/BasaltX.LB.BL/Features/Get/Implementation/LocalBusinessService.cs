using System.Globalization;
using Microsoft.Extensions.Options;
using BasaltX.LB.BL.Models.Request;
using BasaltX.Models.Models.Settings;
using BasaltX.LB.BL.BL.Models.Constants;
using BasaltX.LB.BL.Features.Get.Interfaces;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.Utils.Features.RestOrchestrator.Interface;

namespace BasaltX.LB.BL.Features.Get.Implementation;

internal class LocalBusinessService : ILocalBusinessService
{
    #region Private Members

    private readonly IRestAgent _restAgent;
    private readonly RapidApiSettings _rapiApiSettings;
    #endregion Private Members

    #region Constrcutor(s)

    public LocalBusinessService(IRestAgent _restAgent, IOptions<RapidApiSettings> _rapiApiSettings)
    {
        this._restAgent = _restAgent;
        this._rapiApiSettings = _rapiApiSettings.Value;
    }

    #endregion Constrcutor(s)

    #region Public Methods
    public async Task<ResponseData> SearchNearByAsync(LocalBusinessRequest lbRequest)
    {
        var headers = new Dictionary<string, string>
        {
            { "X-RapidAPI-Host", _rapiApiSettings.RapiAPIHost },
            { "X-RapidAPI-Key", _rapiApiSettings.RapidAPIKey }
        };

        // Convert the latitude and longitude to strings using InvariantCulture
        string latitude = lbRequest.lat.ToString(CultureInfo.InvariantCulture);
        string longitude = lbRequest.lon.ToString(CultureInfo.InvariantCulture);

        var response = await _restAgent.SendRequestAsync($"{_rapiApiSettings.RapidAPIEndpoint}",
        $"/{AttributesRoutes.SearchNearByArea}?query={lbRequest.query}&lat={latitude}&lng={longitude}&limit={2}",
                                                                    RestSharp.Method.Get, null, extraHeaders: headers);

        return response;
    }
    #endregion Public Methods
}
