using System.Net;
using BasaltX.Common.Models.Models.Constants.Enums;
using BasaltX.Common.Models.Models.DTO.Request;
using BasaltX.Common.Models.Models.DTO.Response;

namespace BasaltX.Common.Models.Models.Infastructure;

/// <summary>
/// The request.
/// </summary>
public abstract class Request
{
    /// <summary>
    /// Gets or sets the service types.
    /// </summary>
    public Dictionary<ServiceType, Request> ServiceTypes { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="Request"/> class.
    /// </summary>
    protected Request()
    {
        ServiceTypes = new Dictionary<ServiceType, Request>();
    }

    /// <summary>
    /// Process the request asynchronously.
    /// </summary>
    /// <param name="requestData">The request data.</param>
    /// <returns><![CDATA[Task<ResponseData>]]></returns>
    public virtual async Task<ResponseData> ProcessRequestAsync(RequestData requestData)
    {
        try
        {
            return await (ServiceTypes[requestData.Service]).ProcessRequestAsync(requestData);
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
