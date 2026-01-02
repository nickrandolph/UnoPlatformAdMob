#if __IOS__
using Google.MobileAds;
using UIKit;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// iOS implementation of RewardedAd using Google Mobile Ads SDK.
    /// </summary>
    public partial class RewardedAd
    {
        private RewardedAd? _rewardedAd;

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
                
                Google.MobileAds.RewardedAd.Load(_adUnitId, adRequest, (rewardedAd, error) =>
                {
                    if (error != null)
                    {
                        RaiseAdFailedToLoad(new AdError(
                            (int)error.Code,
                            error.LocalizedDescription ?? error.Description,
                            error.Domain));
                    }
                    else if (rewardedAd != null)
                    {
                        _rewardedAd = rewardedAd;
                        _rewardedAd.PaidEventHandler += OnPaidEvent;
                        _rewardedAd.FullScreenContentDelegate = new RewardedAdDelegate(this);
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
            if (_rewardedAd == null)
            {
                RaiseAdFailedToShow(new AdError(-1, "Rewarded ad is not loaded", "iOS"));
                return;
            }

            try
            {
                var rootViewController = GetRootViewController();
                if (rootViewController != null)
                {
                    _rewardedAd.Present(rootViewController, () =>
                    {
                        var reward = _rewardedAd.AdReward;
                        if (reward != null)
                        {
                            RaiseUserEarnedReward(
                                reward.Type,
                                (int)reward.Amount.Int32Value);
                        }
                    });
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

        private class RewardedAdDelegate : FullScreenContentDelegate
        {
            private readonly RewardedAd _owner;

            public RewardedAdDelegate(RewardedAd owner)
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
                _owner._rewardedAd?.Dispose();
                _owner._rewardedAd = null;
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
