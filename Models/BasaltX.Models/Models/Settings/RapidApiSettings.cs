using System.ComponentModel.DataAnnotations;

namespace BasaltX.Models.Models.Settings
{
    public record RapidApiSettings
    {
        public const string SectionName = "RapidApiSettings";

        [Required]
        public string? RapidAPIKey { get; init; }
        [Required]
        public string? RapiAPIHost { get; init; }
        [Required]
        public string? RapidAPIEndpoint { get; init; }
    }
}
