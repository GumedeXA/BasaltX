using System.ComponentModel.DataAnnotations;

namespace BasalX.Service.Agents.Models.Settings
{
    /// <summary>
    /// The internal api settings.
    /// </summary>
    public class InternalApiSettings
    {
        /// <summary>
        /// The settings section name.
        /// </summary>
        public const string SectionName = "InternalApiSettings";

        /// <summary>
        /// Gets the AI endpoint.
        /// </summary>
        [Required]
        public string AIEndpoint { get; init; } = default!;

        /// <summary>
        /// Gets the local business endpoint.
        /// </summary>
        [Required]
        public string LocalBusinessEndpoint { get; init; } = default!;

        /// <summary>
        /// Gets the API key.
        /// </summary>
        [Required]
        public string APIKey { get; init; } = default!;
    }
}
