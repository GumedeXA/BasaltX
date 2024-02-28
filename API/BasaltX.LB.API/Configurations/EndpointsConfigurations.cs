using BasaltX.LB.API.Constants;
using BasaltX.LB.BL.Models.Request;
using BasaltX.LB.BL.Features.Get.Interfaces;

namespace BasaltX.LB.API.Configurations;

internal static class EndpointsConfigurations
{
    internal static void AddEndPointsConfiguration(this WebApplication app)
    {
        _ = app.MapGet(pattern: Routes.SearchInArea, async (ILocalBusinessService _proccessRequest,[AsParameters] LocalBusinessRequest lbRequest)
        =>
        {
            return await _proccessRequest.SearchNearByAsync(lbRequest).ConfigureAwait(false);

        });
    }
}
