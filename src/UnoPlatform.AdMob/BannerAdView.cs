using System;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// A control that displays banner ads from Google AdMob.
    /// </summary>
    [TemplatePart(Name = ContentPresenterName, Type = typeof(ContentPresenter))]
    public partial class BannerAdView : ContentControl
    {
        private const string ContentPresenterName = "ContentPresenter";

        /// <summary>
        /// Identifies the AdUnitId dependency property.
        /// </summary>
        public static readonly DependencyProperty AdUnitIdProperty =
            DependencyProperty.Register(
                nameof(AdUnitId),
                typeof(string),
                typeof(BannerAdView),
                new PropertyMetadata(null, OnAdUnitIdChanged));

        /// <summary>
        /// Identifies the AdSize dependency property.
        /// </summary>
        public static readonly DependencyProperty AdSizeProperty =
            DependencyProperty.Register(
                nameof(AdSize),
                typeof(AdSize),
                typeof(BannerAdView),
                new PropertyMetadata(AdSize.Banner, OnAdSizeChanged));

        /// <summary>
        /// Gets or sets the Ad Unit ID for this banner ad.
        /// </summary>
        public string AdUnitId
        {
            get => (string)GetValue(AdUnitIdProperty);
            set => SetValue(AdUnitIdProperty, value);
        }

        /// <summary>
        /// Gets or sets the size of the banner ad.
        /// </summary>
        public AdSize AdSize
        {
            get => (AdSize)GetValue(AdSizeProperty);
            set => SetValue(AdSizeProperty, value);
        }

        /// <summary>
        /// Occurs when an ad is successfully loaded.
        /// </summary>
        public event EventHandler<AdEventArgs> AdLoaded;

        /// <summary>
        /// Occurs when an ad fails to load.
        /// </summary>
        public event EventHandler<AdLoadErrorEventArgs> AdFailedToLoad;

        /// <summary>
        /// Occurs when an ad is opened.
        /// </summary>
        public event EventHandler<AdEventArgs> AdOpened;

        /// <summary>
        /// Occurs when an ad is closed.
        /// </summary>
        public event EventHandler<AdEventArgs> AdClosed;

        /// <summary>
        /// Occurs when an ad impression is recorded.
        /// </summary>
        public event EventHandler<AdImpressionEventArgs> AdImpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="BannerAdView"/> class.
        /// </summary>
        public BannerAdView()
        {
            DefaultStyleKey = typeof(BannerAdView);
            Loaded += OnLoaded;
            Unloaded += OnUnloaded;
        }

        private static void OnAdUnitIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BannerAdView bannerAdView)
            {
                bannerAdView.OnAdUnitIdChangedPartial();
            }
        }

        private static void OnAdSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is BannerAdView bannerAdView)
            {
                bannerAdView.OnAdSizeChangedPartial();
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            OnLoadedPartial();
        }

        private void OnUnloaded(object sender, RoutedEventArgs e)
        {
            OnUnloadedPartial();
        }

        /// <summary>
        /// Loads an ad with the specified request.
        /// </summary>
        /// <param name="request">The ad request.</param>
        public void LoadAd(AdRequest request)
        {
            LoadAdPartial(request);
        }

        partial void OnAdUnitIdChangedPartial();
        partial void OnAdSizeChangedPartial();
        partial void OnLoadedPartial();
        partial void OnUnloadedPartial();
        partial void LoadAdPartial(AdRequest request);

        protected void RaiseAdLoaded() => AdLoaded?.Invoke(this, new AdEventArgs());
        protected void RaiseAdFailedToLoad(AdError error) => AdFailedToLoad?.Invoke(this, new AdLoadErrorEventArgs(error));
        protected void RaiseAdOpened() => AdOpened?.Invoke(this, new AdEventArgs());
        protected void RaiseAdClosed() => AdClosed?.Invoke(this, new AdEventArgs());
        protected void RaiseAdImpression() => AdImpression?.Invoke(this, new AdImpressionEventArgs());
    }
}
