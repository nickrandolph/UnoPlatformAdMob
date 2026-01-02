#if __ANDROID__
using Android.Gms.Ads;
using Android.Content;
using Android.Views;
using Uno.AdMob.Configuration;

namespace Uno.AdMob;

public partial class BannerAd
{
    private AdView? _adView;
    private Context? _context;

    partial void LoadAd()
    {
        if (string.IsNullOrEmpty(AdUnitId))
        {
            var defaultAdUnitId = AdConfig.DefaultBannerAdUnitId;
            if (string.IsNullOrEmpty(defaultAdUnitId))
            {
                return;
            }
            AdUnitId = defaultAdUnitId;
        }

        if (_adView != null)
        {
            DestroyAd();
        }

        try
        {
            _context = Android.App.Application.Context;
            
            var adSize = GetAndroidAdSize();
            _adView = new AdView(_context)
            {
                AdSize = adSize,
                AdUnitId = AdUnitId
            };

            var adListener = new AndroidAdListener(this);
            _adView.AdListener = adListener;

            // Clear existing content and add ad view
            Content = null;
            
            var nativeView = _adView;
            if (nativeView != null)
            {
                // For Uno Platform, we need to wrap the native view
                Content = new Microsoft.UI.Xaml.Controls.ContentControl
                {
                    Content = nativeView
                };
            }

            var adRequest = new AdRequest.Builder().Build();
            _adView.LoadAd(adRequest);
        }
        catch (Exception ex)
        {
            RaiseOnAdFailedToLoad(new AdError { Code = -1, Message = ex.Message });
        }
    }

    partial void DestroyAd()
    {
        if (_adView != null)
        {
            _adView.Destroy();
            _adView = null;
        }
        Content = null;
    }

    private Android.Gms.Ads.AdSize GetAndroidAdSize()
    {
        return AdSize switch
        {
            AdSize.Banner => Android.Gms.Ads.AdSize.Banner,
            AdSize.LargeBanner => Android.Gms.Ads.AdSize.LargeBanner,
            AdSize.MediumRectangle => Android.Gms.Ads.AdSize.MediumRectangle,
            AdSize.FullBanner => Android.Gms.Ads.AdSize.FullBanner,
            AdSize.Leaderboard => Android.Gms.Ads.AdSize.Leaderboard,
            AdSize.SmartBanner => Android.Gms.Ads.AdSize.SmartBanner,
            AdSize.Custom => new Android.Gms.Ads.AdSize(CustomAdWidth, CustomAdHeight),
            _ => Android.Gms.Ads.AdSize.Banner
        };
    }

    private class AndroidAdListener : AdListener
    {
        private readonly BannerAd _bannerAd;

        public AndroidAdListener(BannerAd bannerAd)
        {
            _bannerAd = bannerAd;
        }

        public override void OnAdLoaded()
        {
            base.OnAdLoaded();
            _bannerAd.RaiseOnAdLoaded();
        }

        public override void OnAdFailedToLoad(LoadAdError error)
        {
            base.OnAdFailedToLoad(error);
            _bannerAd.RaiseOnAdFailedToLoad(new AdError
            {
                Code = error.Code,
                Message = error.Message ?? "Unknown error"
            });
        }

        public override void OnAdImpression()
        {
            base.OnAdImpression();
            _bannerAd.RaiseOnAdImpression();
        }

        public override void OnAdClicked()
        {
            base.OnAdClicked();
            _bannerAd.RaiseOnAdClicked();
        }

        public override void OnAdOpened()
        {
            base.OnAdOpened();
            _bannerAd.RaiseOnAdOpened();
        }

        public override void OnAdClosed()
        {
            base.OnAdClosed();
            _bannerAd.RaiseOnAdClosed();
        }
    }
}
#endif
