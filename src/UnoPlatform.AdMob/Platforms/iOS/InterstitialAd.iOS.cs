#if __IOS__
using Google.MobileAds;
using UIKit;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// iOS implementation of InterstitialAd using Google Mobile Ads SDK.
    /// </summary>
    public partial class InterstitialAd
    {
        private Interstitial? _interstitial;

        partial void InitializePartial()
        {
            // Initialization happens when loading
        }

        partial void LoadPartial(AdRequest request)
        {
            if (string.IsNullOrEmpty(_adUnitId))
            {
                RaiseAdFailedToLoad(new AdError(-1, "Ad Unit ID is not set", "iOS"));
                return;
            }

            try
            {
                var adRequest = ConvertToNativeRequest(request);
                
                Interstitial.Load(_adUnitId, adRequest, (interstitial, error) =>
                {
                    if (error != null)
                    {
                        RaiseAdFailedToLoad(new AdError(
                            (int)error.Code,
                            error.LocalizedDescription ?? error.Description,
                            error.Domain));
                    }
                    else if (interstitial != null)
                    {
                        _interstitial = interstitial;
                        _interstitial.PaidEventHandler += OnPaidEvent;
                        _interstitial.FullScreenContentDelegate = new InterstitialDelegate(this);
                        RaiseAdLoaded();
                    }
                });
            }
            catch (System.Exception ex)
            {
                RaiseAdFailedToLoad(new AdError(-1, ex.Message, "iOS"));
            }
        }

        partial void ShowPartial()
        {
            if (_interstitial == null)
            {
                RaiseAdFailedToShow(new AdError(-1, "Interstitial ad is not loaded", "iOS"));
                return;
            }

            try
            {
                var rootViewController = GetRootViewController();
                if (rootViewController != null)
                {
                    _interstitial.Present(rootViewController);
                }
                else
                {
                    RaiseAdFailedToShow(new AdError(-1, "Unable to get root view controller", "iOS"));
                }
            }
            catch (System.Exception ex)
            {
                RaiseAdFailedToShow(new AdError(-1, ex.Message, "iOS"));
            }
        }

        private UIViewController? GetRootViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            return window?.RootViewController;
        }

        private Request ConvertToNativeRequest(AdRequest request)
        {
            var requestBuilder = Request.GetDefaultRequest();

            if (request.RequestNonPersonalizedAdsOnly)
            {
                var extras = new Extras();
                extras.AdditionalParameters = new Foundation.NSDictionary("npa", "1");
                requestBuilder.RegisterAdNetworkExtras(extras);
            }

            if (!string.IsNullOrEmpty(request.ContentUrl))
            {
                requestBuilder.ContentUrl = request.ContentUrl;
            }

            if (request.Keywords.Count > 0)
            {
                requestBuilder.Keywords = request.Keywords.ToArray();
            }

            if (request.TestDeviceIds.Count > 0)
            {
                requestBuilder.TestDevices = request.TestDeviceIds.ToArray();
            }

            return requestBuilder;
        }

        private void OnPaidEvent(object? sender, AdValueEventArgs e)
        {
            // Handle paid event if needed
        }

        private class InterstitialDelegate : FullScreenContentDelegate
        {
            private readonly InterstitialAd _owner;

            public InterstitialDelegate(InterstitialAd owner)
            {
                _owner = owner;
            }

            public override void DidPresentFullScreenContent(IFullScreenPresentingAd ad)
            {
                _owner.RaiseAdShown();
            }

            public override void DidFailToPresentFullScreenContentWithError(IFullScreenPresentingAd ad, Foundation.NSError error)
            {
                _owner.RaiseAdFailedToShow(new AdError(
                    (int)error.Code,
                    error.LocalizedDescription ?? error.Description,
                    error.Domain));
            }

            public override void DidDismissFullScreenContent(IFullScreenPresentingAd ad)
            {
                _owner._interstitial?.Dispose();
                _owner._interstitial = null;
                _owner.RaiseAdDismissed();
            }

            public override void DidRecordImpression(IFullScreenPresentingAd ad)
            {
                _owner.RaiseAdImpression();
            }
        }
    }
}
#endif
