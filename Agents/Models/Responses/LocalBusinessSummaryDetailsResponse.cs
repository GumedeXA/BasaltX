using Newtonsoft.Json;

namespace BasalX.Service.Agents.Models.Responses
{
    public class LocalBusinessSummaryDetailsResponse
    {
        public string full_address { get; set; }
        public string place_link { get; set; }
        public string website { get; set; }
        public string type { get; set; }
        public string name { get; set; }
        public Current current_weather { get; set; }
        public Working_Hours working_hours { get; set; }

    }
}


