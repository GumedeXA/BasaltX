using BasaltX.Common.Models.Models.Weather;
using BasaltX.Common.Models.Models.DTO.Response;
using BasaltX.AI.BL.Features.Models.Responses;

namespace BasaltX.AI.BL.Features.Interfaces;

public interface IWeatherService
{
    Task<ResponseData> FindPlacesAsync(string placeName);

    Task<ResponseData> GetPlaceCurrentWeatherAsync(GetPlaceWeatherDetails getPlaceWeatherDetails);

}
