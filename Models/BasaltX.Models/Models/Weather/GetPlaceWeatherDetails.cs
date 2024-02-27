using Newtonsoft.Json;

namespace BasaltX.Common.Models.Models.Weather;

public record GetPlaceWeatherDetails
{
    [JsonProperty("lat")]
    public float lat { get; set; } = default!;
    [JsonProperty("lon")]
    public float lon { get; set; } = default!;
};