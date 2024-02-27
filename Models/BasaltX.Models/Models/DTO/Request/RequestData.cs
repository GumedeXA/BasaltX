using BasaltX.Common.Models.Models.Constants.Enums;

namespace BasaltX.Common.Models.Models.DTO.Request
{
    public class RequestData
    {
        public object? Payload { get; set; }
        public ActionType Action { get; set; }
        public string? AccessToken { get; set; }
        public ServiceType Service { get; set; }
    }
}
