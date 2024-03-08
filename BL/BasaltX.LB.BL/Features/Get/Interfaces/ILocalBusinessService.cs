using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.LB.BL.Models.Request;

namespace BasaltX.LB.BL.Features.Get.Interfaces;

/// <summary>
/// The local business service interface.
/// </summary>
public interface ILocalBusinessService
{
    /// <summary>
    /// Search near by asynchronously.
    /// </summary>
    /// <param name="lbRequest">The lb request.</param>
    /// <returns><![CDATA[Task<ResponseData>]]></returns>
    Task<ResponseData> SearchNearByAsync(LocalBusinessRequest lbRequest);
}
