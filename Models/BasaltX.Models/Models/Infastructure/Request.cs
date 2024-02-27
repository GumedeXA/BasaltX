using System.Net;
using BasaltX.Common.Models.Models.Constants.Enums;
using BasaltX.Common.Models.Models.DTO.Request;
using BasaltX.Common.Models.Models.DTO.Response;

namespace BasaltX.Common.Models.Models.Infastructure;

public abstract class Request
{
    public Dictionary<ServiceType, Request> ServiceTypes { get; set; }

    public Request()
    {
        ServiceTypes = new Dictionary<ServiceType, Request>();
    }

    public virtual async Task<ResponseData> ProcessRequestAsync(RequestData requestData)
    {
        try
        {
            return await (ServiceTypes[requestData.Service] ?? null).ProcessRequestAsync(requestData);
        }
        catch (KeyNotFoundException)
        {
            return new ResponseData
            {
                ResponsePayload = "Unknown resource",
                Status = HttpStatusCode.InternalServerError
            };
        }
    }
}
