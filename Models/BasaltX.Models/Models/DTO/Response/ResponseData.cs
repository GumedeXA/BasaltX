using System.Net;

namespace BasaltX.Common.Models.Models.DTO.Response;

public class ResponseData
{
    public dynamic? ResponsePayload { get; set; }

    public HttpStatusCode Status { get; set; }
}
