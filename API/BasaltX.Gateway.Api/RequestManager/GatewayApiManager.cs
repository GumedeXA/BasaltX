using BasalX.Service.Agents.ServiceManager;
using BasaltX.Common.Models.Models.Infastructure;
using BasaltX.Common.Models.Models.Constants.Enums;
using BasalX.Service.Agents.Features.AIWeather.Implementation;

namespace BasaltX.Gateway.Api.RequestManager
{
    /// <summary>
    /// Will handle all the client facing calls and service the requests 
    /// to the relevant service
    /// </summary>
    public class GatewayApiManager: Request
    {
        private static GatewayApiManager? _instance = null;
        private static readonly object _lock = new();
        private readonly TsoAgent _aiTsoAgent;

        /// <summary>
        /// Get the instance.
        /// </summary>
        /// <param name="_aiTsoAgent">The ai tso agent.</param>
        /// <returns>A GatewayApiManager</returns>
        public static GatewayApiManager GetInstance(TsoAgent _aiTsoAgent)
        {
            if (_instance == null)
            {
                lock (_lock)
                {
                    _instance ??= new GatewayApiManager(_aiTsoAgent);
                }
            }
            return _instance;
        }

        /// <summary>
        /// Prevents a default instance of the <see cref="GatewayApiManager"/> class from being created.
        /// </summary>
        /// <param name="aiTsoAgent">The ai tso agent.</param>
        private GatewayApiManager(TsoAgent aiTsoAgent)
        {
            _aiTsoAgent = aiTsoAgent;

            ServiceTypes.Add(ServiceType.LocalBusiness, new ServiceManager(aiTsoAgent));
        }
    }
}
