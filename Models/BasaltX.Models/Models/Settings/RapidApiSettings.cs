using System.ComponentModel.DataAnnotations;

namespace BasaltX.Models.Models.Settings
{
    /// <summary>
    /// The rapid api settings.
    /// </summary>
    public record RapidApiSettings
    {
        /// <summary>
        /// The section name.
        /// </summary>
        public const string SectionName = "RapidApiSettings";

        /// <summary>
        /// Gets the rapid API key.
        /// </summary>
        [Required]
        public string RapidAPIKey { get; init; } = default!;
        /// <summary>
        /// Gets the rapi API host.
        /// </summary>
        [Required]
        public string RapiAPIHost { get; init; } = default!;
        /// <summary>
        /// Gets the rapid API endpoint.
        /// </summary>
        [Required]
        public string RapidAPIEndpoint { get; init; } = default!;
    }
}
