namespace BasalX.Service.Agents.Models.Responses
{

    public class WeatherResponse
    {
        public ResponsePayload responsePayload { get; set; }
        public int status { get; set; }

        //Nesting these classes as they will only be accessible via WeatherResponse
        public class ResponsePayload
        {
            public Current current { get; set; }
        }
    }
    public class Current
    {
        public string summary { get; set; }
        public float temperature { get; set; }
        public int humidity { get; set; }
    }
}
