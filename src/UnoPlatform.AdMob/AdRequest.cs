using System.Collections.Generic;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Represents an ad request containing targeting information.
    /// </summary>
    public class AdRequest
    {
        /// <summary>
        /// Gets the keywords for targeting.
        /// </summary>
        public List<string> Keywords { get; } = new List<string>();

        /// <summary>
        /// Gets the content URL for targeting.
        /// </summary>
        public string ContentUrl { get; set; }

        /// <summary>
        /// Gets or sets whether to request non-personalized ads.
        /// </summary>
        public bool RequestNonPersonalizedAdsOnly { get; set; }

        /// <summary>
        /// Gets test device IDs.
        /// </summary>
        public List<string> TestDeviceIds { get; } = new List<string>();

        /// <summary>
        /// Creates a new AdRequest with default settings.
        /// </summary>
        public AdRequest()
        {
        }
    }
}
