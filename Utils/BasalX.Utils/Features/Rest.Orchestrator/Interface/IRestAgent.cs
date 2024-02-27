using BasaltX.Common.Models.Models.DTO.Response;
using RestSharp;

namespace BasaltX.Utils.Features.RestOrchestrator.Interface;

public interface IRestAgent
{
    Task<ResponseData> SendRequestAsync(string apiUrl, string path, Method methodType, object? payload = null,IDictionary<string, string>? extraHeaders = null, string? contentNegotiation = null);
}
