#if __ANDROID__
using Android.Gms.Ads;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Android implementation of BannerAdView using Google Mobile Ads SDK.
    /// </summary>
    public partial class BannerAdView
    {
        private AdView? _adView;
        private ContentPresenter? _contentPresenter;

        partial void OnAdUnitIdChangedPartial()
        {
            if (_adView != null && !string.IsNullOrEmpty(AdUnitId))
            {
                _adView.AdUnitId = AdUnitId;
            }
        }

        partial void OnAdSizeChangedPartial()
        {
            if (_adView != null && AdSize != null)
            {
                _adView.SetAdSize(ConvertToNativeAdSize(AdSize));
            }
        }

        partial void OnLoadedPartial()
        {
            InitializeAdView();
        }

        partial void OnUnloadedPartial()
        {
            CleanupAdView();
        }

        partial void LoadAdPartial(AdRequest request)
        {
            if (_adView == null)
            {
                InitializeAdView();
            }

            if (_adView != null)
            {
                var adRequest = ConvertToNativeAdRequest(request);
                _adView.LoadAd(adRequest);
            }
        }

        private void InitializeAdView()
        {
            if (_adView != null || string.IsNullOrEmpty(AdUnitId))
                return;

            try
            {
                _adView = new AdView(Android.App.Application.Context);
                _adView.AdUnitId = AdUnitId;
                _adView.SetAdSize(ConvertToNativeAdSize(AdSize ?? AdSize.Banner));

                // Set up event listeners
                _adView.AdListener = new BannerAdListener(this);

                // Add the native ad view to the control
                if (_contentPresenter == null)
                {
                    _contentPresenter = this.GetTemplateChild("ContentPresenter") as ContentPresenter;
                }

                if (_contentPresenter != null)
                {
                    _contentPresenter.Content = _adView;
                }
            }
            catch (System.Exception ex)
            {
                RaiseAdFailedToLoad(new AdError(-1, ex.Message, "Android"));
            }
        }

        private void CleanupAdView()
        {
            if (_adView != null)
            {
                _adView.Destroy();
                _adView = null;
            }
        }

        private Android.Gms.Ads.AdSize ConvertToNativeAdSize(AdSize adSize)
        {
            if (adSize.Width == -1 && adSize.Height == -2)
            {
                return Android.Gms.Ads.AdSize.SmartBanner;
            }
            else if (adSize.Width == 320 && adSize.Height == 50)
            {
                return Android.Gms.Ads.AdSize.Banner;
            }
            else if (adSize.Width == 320 && adSize.Height == 100)
            {
                return Android.Gms.Ads.AdSize.LargeBanner;
            }
            else if (adSize.Width == 300 && adSize.Height == 250)
            {
                return Android.Gms.Ads.AdSize.MediumRectangle;
            }
            else if (adSize.Width == 468 && adSize.Height == 60)
            {
                return Android.Gms.Ads.AdSize.FullBanner;
            }
            else if (adSize.Width == 728 && adSize.Height == 90)
            {
                return Android.Gms.Ads.AdSize.Leaderboard;
            }
            else
            {
                return new Android.Gms.Ads.AdSize(adSize.Width, adSize.Height);
            }
        }

        private Android.Gms.Ads.AdRequest ConvertToNativeAdRequest(AdRequest request)
        {
            var builder = new Android.Gms.Ads.AdRequest.Builder();

            if (request.RequestNonPersonalizedAdsOnly)
            {
                var extras = new Android.OS.Bundle();
                extras.PutString("npa", "1");
                builder.AddNetworkExtrasBundle(Java.Lang.Class.FromType(typeof(Android.Gms.Ads.AdRequest)), extras);
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

        private class BannerAdListener : AdListener
        {
            private readonly BannerAdView _owner;

            public BannerAdListener(BannerAdView owner)
            {
                _owner = owner;
            }

            public override void OnAdLoaded()
            {
                _owner.RaiseAdLoaded();
            }

            public override void OnAdFailedToLoad(LoadAdError error)
            {
                _owner.RaiseAdFailedToLoad(new AdError(
                    error.Code,
                    error.Message,
                    error.Domain));
            }

            public override void OnAdOpened()
            {
                _owner.RaiseAdOpened();
            }

            public override void OnAdClosed()
            {
                _owner.RaiseAdClosed();
            }

            public override void OnAdImpression()
            {
                _owner.RaiseAdImpression();
            }
        }
    }
}
#endif
