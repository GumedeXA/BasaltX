using System.ComponentModel.DataAnnotations;

namespace BasalX.Service.Agents.Models.Settings
{
    public class InternalApiSettings
    {
        public const string SectionName = "InternalApiSettings";

        [Required]
        public string? AIEndpoint { get; init; }

        [Required]
        public string? LocalBusinessEndpoint { get; init; }
    }
}
