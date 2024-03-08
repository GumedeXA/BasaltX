using Newtonsoft.Json;

namespace BasaltX.Common.Models.Models.Weather;

/// <summary>
/// The get place weather details.
/// </summary>
public record GetPlaceWeatherDetails
{
    /// <summary>
    /// Gets or sets the lat.
    /// </summary>
    [JsonProperty("lat")]
    public float lat { get; set; } = default!;
    /// <summary>
    /// Gets or sets the lon.
    /// </summary>
    [JsonProperty("lon")]
    public float lon { get; set; } = default!;
};