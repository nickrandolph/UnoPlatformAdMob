#if __ANDROID__
using Android.Gms.Ads;
using System.Threading.Tasks;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Android implementation of MobileAds initialization using Google Mobile Ads SDK.
    /// </summary>
    public static partial class MobileAds
    {
        private static partial Task<bool> InitializePartial()
        {
            var tcs = new TaskCompletionSource<bool>();

            try
            {
                var context = Android.App.Application.Context;
                MobileAdsClass.Initialize(context, new InitializationCompleteListener(tcs));
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
                var builder = new RequestConfiguration.Builder();
                
                if (testDeviceIds != null && testDeviceIds.Length > 0)
                {
                    var testDeviceIdsList = new Java.Util.ArrayList();
                    foreach (var deviceId in testDeviceIds)
                    {
                        testDeviceIdsList.Add(deviceId);
                    }
                    builder.SetTestDeviceIds(testDeviceIdsList);
                }

                MobileAdsClass.SetRequestConfiguration(builder.Build());
            }
            catch (System.Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Failed to set request configuration: {ex.Message}");
            }
        }

        private class InitializationCompleteListener : Java.Lang.Object, IOnInitializationCompleteListener
        {
            private readonly TaskCompletionSource<bool> _tcs;

            public InitializationCompleteListener(TaskCompletionSource<bool> tcs)
            {
                _tcs = tcs;
            }

            public void OnInitializationComplete(IInitializationStatus status)
            {
                SetInitialized();
                _tcs.SetResult(true);
            }
        }
    }
}
#endif
