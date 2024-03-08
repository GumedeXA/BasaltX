using System.Net;

namespace BasaltX.Common.Models.Models.DTO.Response;

/// <summary>
/// The response data.
/// </summary>
public class ResponseData
{
    /// <summary>
    /// Gets or sets the response payload.
    /// </summary>
    public dynamic? ResponsePayload { get; set; }

    /// <summary>
    /// Gets or sets the status.
    /// </summary>
    public HttpStatusCode Status { get; set; }
}
