﻿using Newtonsoft.Json;

namespace BasalX.Service.Agents.Models.Responses;

public class PlacesResponse
{
    [JsonProperty("lat")]
    public string lat { get; set; }
    [JsonProperty("lon")]
    public string lon { get; set; }

    [JsonProperty("name")]
    public string name { get; set; }

    [JsonProperty("place_id")]
    public string place_id { get; set; }

    [JsonProperty("adm_area1")]
    public string adm_area1 { get; set; }

    [JsonProperty("adm_area2")]
    public string adm_area2 { get; set; }

    [JsonProperty("country")]
    public string country { get; set; }

    [JsonProperty("timezone")]
    public string timezone { get; set; }

    [JsonProperty("type")]
    public string type { get; set; }
}

public class PlacesResponseCollection
{
    public List<PlacesResponse> responsePayload { get; set; }
    public int Status { get; set; }
}
