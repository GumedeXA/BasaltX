using Newtonsoft.Json;

namespace BasalX.Service.Agents.Models.Responses
{
    //Near by businesses search by location responses
    /// <summary>
    /// The local businesses data response.
    /// </summary>
    public class LocalBusinessesDataResponse
    {
        /// <summary>
        /// Gets or sets the response payload.
        /// </summary>
        public Responsepayload responsePayload { get; set; }

        //Logical grouping nested class
        /// <summary>
        /// The responsepayload.
        /// </summary>
        public class Responsepayload
        {
            /// <summary>
            /// Gets or sets the data.
            /// </summary>
            public List<BusinessDetails> data { get; set; }
        }
    }

    /// <summary>
    /// The business details.
    /// </summary>
    public class BusinessDetails
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [JsonProperty("name")]
        public string name { get; set; }
        /// <summary>
        /// Gets or sets the full address.
        /// </summary>
        [JsonProperty("full_address")]
        public string full_address { get; set; }
        /// <summary>
        /// Gets or sets the review count.
        /// </summary>
        [JsonProperty("review_count")]
        public int review_count { get; set; }
        /// <summary>
        /// Gets or sets the working hours.
        /// </summary>
        public Working_Hours working_hours { get; set; }
        /// <summary>
        /// Gets or sets the website.
        /// </summary>
        [JsonProperty("website")]
        public string website { get; set; }
        /// <summary>
        /// Gets or sets the place link.
        /// </summary>
        [JsonProperty("place_link")]
        public string place_link { get; set; }

    }
    /// <summary>
    /// The working hours.
    /// </summary>
    public class Working_Hours
    {
        /// <summary>
        /// Gets or sets the tuesday.
        /// </summary>
        public string[] Tuesday { get; set; }
        /// <summary>
        /// Gets or sets the wednesday.
        /// </summary>
        public string[] Wednesday { get; set; }
        /// <summary>
        /// Gets or sets the thursday.
        /// </summary>
        public string[] Thursday { get; set; }
        /// <summary>
        /// Gets or sets the friday.
        /// </summary>
        public string[] Friday { get; set; }
        /// <summary>
        /// Gets or sets the saturday.
        /// </summary>
        public string[] Saturday { get; set; }
        /// <summary>
        /// Gets or sets the sunday.
        /// </summary>
        public string[] Sunday { get; set; }
        /// <summary>
        /// Gets or sets the monday.
        /// </summary>
        public string[] Monday { get; set; }
    }
}
