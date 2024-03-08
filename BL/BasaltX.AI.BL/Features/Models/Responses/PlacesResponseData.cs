using Newtonsoft.Json;

namespace BasaltX.AI.BL.Features.Models.Responses;

/// <summary>
/// The places response data.
/// </summary>
public class PlacesResponseData
{
    /// <summary>
    /// Gets or sets the lat.
    /// </summary>
    [JsonProperty("lat")]
    public string lat { get; set; }
    /// <summary>
    /// Gets or sets the lon.
    /// </summary>
    [JsonProperty("lon")]
    public string lon { get; set; }

    /// <summary>
    /// Gets or sets the name.
    /// </summary>
    [JsonProperty("name")]
    public string name { get; set; }

    /// <summary>
    /// Gets or sets the place id.
    /// </summary>
    [JsonProperty("place_id")]
    public string place_id { get; set; }

    /// <summary>
    /// Gets or sets the adm area1.
    /// </summary>
    [JsonProperty("adm_area1")]
    public string adm_area1 { get; set; }

    /// <summary>
    /// Gets or sets the adm area2.
    /// </summary>
    [JsonProperty("adm_area2")]
    public string adm_area2 { get; set; }

    /// <summary>
    /// Gets or sets the country.
    /// </summary>
    [JsonProperty("country")]
    public string country { get; set; }

    /// <summary>
    /// Gets or sets the timezone.
    /// </summary>
    [JsonProperty("timezone")]
    public string timezone { get; set; }

    /// <summary>
    /// Gets or sets the type.
    /// </summary>
    [JsonProperty("type")]
    public string type { get; set; }
}
