#if __IOS__
using Google.MobileAds;
using System.Threading.Tasks;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// iOS implementation of MobileAds initialization using Google Mobile Ads SDK.
    /// </summary>
    public static partial class MobileAds
    {
        private static partial Task<bool> InitializePartial()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                MobileAdsClass.SharedInstance.Start((status) =>
                {
                    SetInitialized();
                    tcs.SetResult(true);
                });
            }
            catch (System.Exception ex)
            {
                tcs.SetResult(false);
                System.Diagnostics.Debug.WriteLine($"AdMob initialization failed: {ex.Message}");
            }

            return tcs.Task;
        }

        private static partial void SetRequestConfigurationPartial(string[] testDeviceIds)
        {
            try
            {
                var requestConfiguration = new RequestConfiguration();
                
                if (testDeviceIds != null && testDeviceIds.Length > 0)
                {
                    requestConfiguration.TestDeviceIdentifiers = testDeviceIds;
                }

                MobileAdsClass.SharedInstance.RequestConfiguration = requestConfiguration;
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to set request configuration: {ex.Message}");
            }
        }
    }
}
#endif
