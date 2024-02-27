
using BasaltX.Common.Models.Models.Weather;
using Newtonsoft.Json;

namespace BasaltX.LB.BL.Models.Request;

public record LocalBusinessRequest : GetPlaceWeatherDetails
{
    //Search query for the business,wherether it a pizza place, plumbers,Bars etc...
    [JsonProperty("query")]
    public string query { get; set; } = default!;
}
