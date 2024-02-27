using Newtonsoft.Json;

namespace BasalX.Service.Agents.Models.Responses
{
    //Near by businesses search by location responses
    public class LocalBusinessesDataResponse
    {
        public Responsepayload responsePayload { get; set; }

        //Logical grouping nested class
        public class Responsepayload
        {
            public List<BusinessDetails> data { get; set; }
        }
    }

    public class BusinessDetails
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("full_address")]
        public string full_address { get; set; }
        [JsonProperty("review_count")]
        public int review_count { get; set; }
        public Working_Hours working_hours { get; set; }
        [JsonProperty("website")]
        public string website { get; set; }
        [JsonProperty("place_link")]
        public string place_link { get; set; }

    }
    public class Working_Hours
    {
        public string[] Tuesday { get; set; }
        public string[] Wednesday { get; set; }
        public string[] Thursday { get; set; }
        public string[] Friday { get; set; }
        public string[] Saturday { get; set; }
        public string[] Sunday { get; set; }
        public string[] Monday { get; set; }
    }
}
