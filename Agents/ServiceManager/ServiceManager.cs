using BasaltX.Common.Models.Models.DTO.Request;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.Common.Models.Models.Infastructure;
using BasalX.Service.Agents.Features.AIWeather.Implementation;

namespace BasalX.Service.Agents.ServiceManager;

public class ServiceManager : Request
{
    #region Private Members
    private readonly TsoAgent _tsoAgent;
    #endregion Private Members

    #region Constructors
    public ServiceManager(TsoAgent _tsoAgent)
    {
        this._tsoAgent = _tsoAgent;
    }
    #endregion Constructors

    #region Public Methods
    
    public override async Task<ResponseData> ProcessRequestAsync(RequestData requestData)
    => await _tsoAgent.ProcessRequestAsync(requestData);

    #endregion Public Methods
}