#if __IOS__
using Foundation;
using Google.MobileAds;
using Uno.AdMob.Configuration;
using UIKit;

namespace Uno.AdMob;

public partial class BannerAd
{
    private BannerView? _bannerView;

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

        if (_bannerView != null)
        {
            DestroyAd();
        }

        try
        {
            var adSize = GetIOSAdSize();
            _bannerView = new BannerView(adSize)
            {
                AdUnitId = AdUnitId
            };

            var rootViewController = GetRootViewController();
            if (rootViewController != null)
            {
                _bannerView.RootViewController = rootViewController;
            }

            var adDelegate = new IOSBannerViewDelegate(this);
            _bannerView.AdReceived += adDelegate.AdReceived;
            _bannerView.ReceiveAdFailed += adDelegate.ReceiveAdFailed;
            _bannerView.ImpressionRecorded += adDelegate.ImpressionRecorded;
            _bannerView.AdClicked += adDelegate.AdClicked;
            _bannerView.WillPresentScreen += adDelegate.WillPresentScreen;
            _bannerView.WillDismissScreen += adDelegate.WillDismissScreen;

            // Clear existing content and add ad view
            Content = null;

            var nativeView = _bannerView;
            if (nativeView != null)
            {
                // For Uno Platform, we need to wrap the native view
                Content = new Microsoft.UI.Xaml.Controls.ContentControl
                {
                    Content = nativeView
                };
            }

            var request = Google.MobileAds.Request.GetDefaultRequest();
            _bannerView.LoadRequest(request);
        }
        catch (Exception ex)
        {
            RaiseOnAdFailedToLoad(new AdError { Code = -1, Message = ex.Message });
        }
    }

    partial void DestroyAd()
    {
        if (_bannerView != null)
        {
            _bannerView.Dispose();
            _bannerView = null;
        }
        Content = null;
    }

    private AdSizeCons GetIOSAdSize()
    {
        return AdSize switch
        {
            AdSize.Banner => AdSizeCons.Banner,
            AdSize.LargeBanner => AdSizeCons.LargeBanner,
            AdSize.MediumRectangle => AdSizeCons.MediumRectangle,
            AdSize.FullBanner => AdSizeCons.FullBanner,
            AdSize.Leaderboard => AdSizeCons.Leaderboard,
            AdSize.SmartBanner => AdSizeCons.SmartBannerPortrait,
            AdSize.Custom => new AdSizeCons { Size = new CoreGraphics.CGSize(CustomAdWidth, CustomAdHeight) },
            _ => AdSizeCons.Banner
        };
    }

    private UIViewController? GetRootViewController()
    {
        var window = UIApplication.SharedApplication.KeyWindow;
        return window?.RootViewController;
    }

    private class IOSBannerViewDelegate
    {
        private readonly BannerAd _bannerAd;

        public IOSBannerViewDelegate(BannerAd bannerAd)
        {
            _bannerAd = bannerAd;
        }

        public void AdReceived(object? sender, EventArgs e)
        {
            _bannerAd.RaiseOnAdLoaded();
        }

        public void ReceiveAdFailed(object? sender, BannerViewErrorEventArgs e)
        {
            _bannerAd.RaiseOnAdFailedToLoad(new AdError
            {
                Code = (int)e.Error.Code,
                Message = e.Error.LocalizedDescription ?? "Unknown error"
            });
        }

        public void ImpressionRecorded(object? sender, EventArgs e)
        {
            _bannerAd.RaiseOnAdImpression();
        }

        public void AdClicked(object? sender, EventArgs e)
        {
            _bannerAd.RaiseOnAdClicked();
        }

        public void WillPresentScreen(object? sender, EventArgs e)
        {
            _bannerAd.RaiseOnAdOpened();
        }

        public void WillDismissScreen(object? sender, EventArgs e)
        {
            _bannerAd.RaiseOnAdClosed();
        }
    }
}
#endif
