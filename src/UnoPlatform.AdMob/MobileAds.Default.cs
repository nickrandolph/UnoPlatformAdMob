using System.Threading.Tasks;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Default implementation of MobileAds for platforms without native ad support.
    /// </summary>
    public static partial class MobileAds
    {
        private static partial Task<bool> InitializePartial()
        {
            // Default implementation - no initialization needed for unsupported platforms
            SetInitialized();
            return Task.FromResult(false);
        }

        private static partial void SetRequestConfigurationPartial(string[] testDeviceIds)
        {
            // Default implementation - does nothing on unsupported platforms
        }
    }
}
