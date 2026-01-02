#if __ANDROID__
using Android.Gms.Ads;
using Android.Gms.Ads.Rewarded;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Android implementation of RewardedAd using Google Mobile Ads SDK.
    /// </summary>
    public partial class RewardedAd
    {
        private RewardedAdClass? _rewardedAd;

        partial void InitializePartial()
        {
            // Initialization happens when loading
        }

        partial void LoadPartial(AdRequest request)
        {
            if (string.IsNullOrEmpty(_adUnitId))
            {
                RaiseAdFailedToLoad(new AdError(-1, "Ad Unit ID is not set", "Android"));
                return;
            }

            try
            {
                var adRequest = ConvertToNativeAdRequest(request);
                
                RewardedAdClass.Load(
                    Android.App.Application.Context,
                    _adUnitId,
                    adRequest,
                    new RewardedAdLoadCallback(this));
            }
            catch (System.Exception ex)
            {
                RaiseAdFailedToLoad(new AdError(-1, ex.Message, "Android"));
            }
        }

        partial void ShowPartial()
        {
            if (_rewardedAd == null)
            {
                RaiseAdFailedToShow(new AdError(-1, "Rewarded ad is not loaded", "Android"));
                return;
            }

            try
            {
                var activity = Uno.UI.ContextHelper.Current as Android.App.Activity;
                if (activity != null)
                {
                    _rewardedAd.Show(activity, new RewardedAdCallback(this));
                }
                else
                {
                    RaiseAdFailedToShow(new AdError(-1, "Unable to get current activity", "Android"));
                }
            }
            catch (System.Exception ex)
            {
                RaiseAdFailedToShow(new AdError(-1, ex.Message, "Android"));
            }
        }

        private Android.Gms.Ads.AdRequest ConvertToNativeAdRequest(AdRequest request)
        {
            var builder = new Android.Gms.Ads.AdRequest.Builder();

            if (request.RequestNonPersonalizedAdsOnly)
            {
                var extras = new Android.OS.Bundle();
                extras.PutString("npa", "1");
                builder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(Android.Gms.Ads.AdMob.App.AppOpenAd)), extras);
            }

            if (!string.IsNullOrEmpty(request.ContentUrl))
            {
                builder.SetContentUrl(request.ContentUrl);
            }

            foreach (var keyword in request.Keywords)
            {
                builder.AddKeyword(keyword);
            }

            foreach (var testDeviceId in request.TestDeviceIds)
            {
                builder.AddTestDevice(testDeviceId);
            }

            return builder.Build();
        }

        private class RewardedAdLoadCallback : RewardedAdLoadCallbackClass
        {
            private readonly RewardedAd _owner;

            public RewardedAdLoadCallback(RewardedAd owner)
            {
                _owner = owner;
            }

            public override void OnAdLoaded(RewardedAdClass rewardedAd)
            {
                _owner._rewardedAd = rewardedAd;
                _owner._rewardedAd.FullScreenContentCallback = new RewardedAdContentCallback(_owner);
                _owner.RaiseAdLoaded();
            }

            public override void OnAdFailedToLoad(LoadAdError error)
            {
                _owner.RaiseAdFailedToLoad(new AdError(
                    error.Code,
                    error.Message,
                    error.Domain));
            }
        }

        private class RewardedAdContentCallback : FullScreenContentCallback
        {
            private readonly RewardedAd _owner;

            public RewardedAdContentCallback(RewardedAd owner)
            {
                _owner = owner;
            }

            public override void OnAdShowedFullScreenContent()
            {
                _owner.RaiseAdShown();
            }

            public override void OnAdFailedToShowFullScreenContent(Android.Gms.Ads.AdError error)
            {
                _owner.RaiseAdFailedToShow(new AdError(
                    error.Code,
                    error.Message,
                    error.Domain));
            }

            public override void OnAdDismissedFullScreenContent()
            {
                _owner._rewardedAd = null;
                _owner.RaiseAdDismissed();
            }

            public override void OnAdImpression()
            {
                _owner.RaiseAdImpression();
            }
        }

        private class RewardedAdCallback : Java.Lang.Object, IOnUserEarnedRewardListener
        {
            private readonly RewardedAd _owner;

            public RewardedAdCallback(RewardedAd owner)
            {
                _owner = owner;
            }

            public void OnUserEarnedReward(IRewardItem rewardItem)
            {
                _owner.RaiseUserEarnedReward(rewardItem.Type, rewardItem.Amount);
            }
        }
    }
}
#endif
