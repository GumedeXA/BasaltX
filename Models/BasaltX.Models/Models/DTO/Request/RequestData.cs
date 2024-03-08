using BasaltX.Common.Models.Models.Constants.Enums;

namespace BasaltX.Common.Models.Models.DTO.Request
{
    /// <summary>
    /// The request data.
    /// </summary>
    public class RequestData
    {
        /// <summary>
        /// Gets or sets the payload.
        /// </summary>
        public object? Payload { get; set; }
        /// <summary>
        /// Gets or sets the action.
        /// </summary>
        public ActionType Action { get; set; }
        /// <summary>
        /// Gets or sets the access token.
        /// </summary>
        public string? AccessToken { get; set; }
        /// <summary>
        /// Gets or sets the service.
        /// </summary>
        public ServiceType Service { get; set; }
    }
}
