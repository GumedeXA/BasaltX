using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.LB.BL.Models.Request;

namespace BasaltX.LB.BL.Features.Get.Interfaces;

public interface ILocalBusinessService
{
    Task<ResponseData> SearchNearByAsync(LocalBusinessRequest weatherRequest);
}
