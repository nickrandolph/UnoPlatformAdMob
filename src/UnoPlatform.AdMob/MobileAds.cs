using System;
using System.Threading.Tasks;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Manages initialization of the Google Mobile Ads SDK.
    /// </summary>
    public static partial class MobileAds
    {
        private static bool _initialized;

        /// <summary>
        /// Initializes the Google Mobile Ads SDK.
        /// </summary>
        /// <returns>A task representing the initialization operation.</returns>
        public static Task<bool> Initialize()
        {
            if (_initialized)
            {
                return Task.FromResult(true);
            }

            return InitializePartial();
        }

        /// <summary>
        /// Sets the request configuration for all ad requests.
        /// </summary>
        /// <param name="testDeviceIds">A list of test device IDs.</param>
        public static void SetRequestConfiguration(params string[] testDeviceIds)
        {
            SetRequestConfigurationPartial(testDeviceIds);
        }

        private static partial Task<bool> InitializePartial();
        private static partial void SetRequestConfigurationPartial(string[] testDeviceIds);

        internal static void SetInitialized()
        {
            _initialized = true;
        }
    }
}
