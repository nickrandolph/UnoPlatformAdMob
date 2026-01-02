#if __IOS__
using Google.MobileAds;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using UIKit;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// iOS implementation of BannerAdView using Google Mobile Ads SDK.
    /// </summary>
    public partial class BannerAdView
    {
        private BannerView? _bannerView;
        private ContentPresenter? _contentPresenter;

        partial void OnAdUnitIdChangedPartial()
        {
            if (_bannerView != null && !string.IsNullOrEmpty(AdUnitId))
            {
                // Recreate banner view with new ad unit ID
                CleanupBannerView();
                InitializeBannerView();
            }
        }

        partial void OnAdSizeChangedPartial()
        {
            if (_bannerView != null && AdSize != null)
            {
                // Recreate banner view with new size
                CleanupBannerView();
                InitializeBannerView();
            }
        }

        partial void OnLoadedPartial()
        {
            InitializeBannerView();
        }

        partial void OnUnloadedPartial()
        {
            CleanupBannerView();
        }

        partial void LoadAdPartial(AdRequest request)
        {
            if (_bannerView == null)
            {
                InitializeBannerView();
            }

            if (_bannerView != null)
            {
                var adRequest = ConvertToNativeRequest(request);
                _bannerView.LoadRequest(adRequest);
            }
        }

        private void InitializeBannerView()
        {
            if (_bannerView != null || string.IsNullOrEmpty(AdUnitId))
                return;

            try
            {
                var adSize = ConvertToNativeAdSize(AdSize ?? AdSize.Banner);
                _bannerView = new BannerView(adSize)
                {
                    AdUnitId = AdUnitId,
                    RootViewController = GetRootViewController()
                };

                // Set up delegates
                _bannerView.AdReceived += OnAdReceived;
                _bannerView.ReceiveAdFailed += OnReceiveAdFailed;
                _bannerView.WillPresentScreen += OnWillPresentScreen;
                _bannerView.WillDismissScreen += OnWillDismissScreen;
                _bannerView.ImpressionRecorded += OnImpressionRecorded;

                // Add the native ad view to the control
                if (_contentPresenter == null)
                {
                    _contentPresenter = this.GetTemplateChild("ContentPresenter") as ContentPresenter;
                }

                if (_contentPresenter != null)
                {
                    _contentPresenter.Content = _bannerView;
                }
            }
            catch (System.Exception ex)
            {
                RaiseAdFailedToLoad(new AdError(-1, ex.Message, "iOS"));
            }
        }

        private void CleanupBannerView()
        {
            if (_bannerView != null)
            {
                _bannerView.AdReceived -= OnAdReceived;
                _bannerView.ReceiveAdFailed -= OnReceiveAdFailed;
                _bannerView.WillPresentScreen -= OnWillPresentScreen;
                _bannerView.WillDismissScreen -= OnWillDismissScreen;
                _bannerView.ImpressionRecorded -= OnImpressionRecorded;
                _bannerView.Dispose();
                _bannerView = null;
            }
        }

        private UIViewController? GetRootViewController()
        {
            var window = UIApplication.SharedApplication.KeyWindow;
            return window?.RootViewController;
        }

        private Google.MobileAds.AdSize ConvertToNativeAdSize(AdSize adSize)
        {
            if (adSize.Width == -1 && adSize.Height == -2)
            {
                return Google.MobileAds.AdSize.SmartBannerPortrait;
            }
            else if (adSize.Width == 320 && adSize.Height == 50)
            {
                return Google.MobileAds.AdSize.Banner;
            }
            else if (adSize.Width == 320 && adSize.Height == 100)
            {
                return Google.MobileAds.AdSize.LargeBanner;
            }
            else if (adSize.Width == 300 && adSize.Height == 250)
            {
                return Google.MobileAds.AdSize.MediumRectangle;
            }
            else if (adSize.Width == 468 && adSize.Height == 60)
            {
                return Google.MobileAds.AdSize.FullBanner;
            }
            else if (adSize.Width == 728 && adSize.Height == 90)
            {
                return Google.MobileAds.AdSize.Leaderboard;
            }
            else
            {
                return new Google.MobileAds.AdSize((nint)adSize.Width, (nint)adSize.Height);
            }
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

        private void OnAdReceived(object? sender, System.EventArgs e)
        {
            RaiseAdLoaded();
        }

        private void OnReceiveAdFailed(object? sender, BannerViewErrorEventArgs e)
        {
            var error = e.Error;
            RaiseAdFailedToLoad(new AdError(
                (int)error.Code,
                error.LocalizedDescription ?? error.Description,
                error.Domain));
        }

        private void OnWillPresentScreen(object? sender, System.EventArgs e)
        {
            RaiseAdOpened();
        }

        private void OnWillDismissScreen(object? sender, System.EventArgs e)
        {
            RaiseAdClosed();
        }

        private void OnImpressionRecorded(object? sender, System.EventArgs e)
        {
            RaiseAdImpression();
        }
    }
}
#endif
