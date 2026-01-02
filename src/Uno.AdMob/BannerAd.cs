using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace Uno.AdMob;

/// <summary>
/// Displays a banner ad.
/// </summary>
public partial class BannerAd : UserControl
{
    /// <summary>
    /// Raised when an ad is received.
    /// </summary>
    public event EventHandler? OnAdLoaded;

    /// <summary>
    /// Raised when an ad request failed.
    /// </summary>
    public event EventHandler<IAdError>? OnAdFailedToLoad;

    /// <summary>
    /// Raised when an impression is recorded for an ad.
    /// </summary>
    public event EventHandler? OnAdImpression;

    /// <summary>
    /// Raised when a click is recorded for an ad.
    /// </summary>
    public event EventHandler? OnAdClicked;

    /// <summary>
    /// Raised when a swipe gesture on an ad is recorded as a click. Supported only by Android.
    /// </summary>
    public event EventHandler? OnAdSwiped;

    /// <summary>
    /// Raised when an ad opens an overlay that covers the screen.
    /// </summary>
    public event EventHandler? OnAdOpened;

    /// <summary>
    /// Raised when the user is about to return to the application after clicking on an ad.
    /// </summary>
    public event EventHandler? OnAdClosed;

    /// <summary>
    /// The ad unit id dependency property.
    /// </summary>
    public static readonly DependencyProperty AdUnitIdProperty =
        DependencyProperty.Register(nameof(AdUnitId), typeof(string), typeof(BannerAd), new PropertyMetadata(null, OnAdUnitIdChanged));

    /// <summary>
    /// The ad unit id.
    /// </summary>
    public string AdUnitId
    {
        get => (string)GetValue(AdUnitIdProperty);
        set => SetValue(AdUnitIdProperty, value);
    }

    /// <summary>
    /// The desired ad size dependency property.
    /// </summary>
    public static readonly DependencyProperty AdSizeProperty =
        DependencyProperty.Register(nameof(AdSize), typeof(AdSize), typeof(BannerAd), new PropertyMetadata(AdSize.Banner, OnAdSizeChanged));

    /// <summary>
    /// The desired ad size.
    /// </summary>
    public AdSize AdSize
    {
        get => (AdSize)GetValue(AdSizeProperty);
        set => SetValue(AdSizeProperty, value);
    }

    /// <summary>
    /// The desired ad width in density-independent pixels dependency property.
    /// </summary>
    public static readonly DependencyProperty CustomAdWidthProperty =
        DependencyProperty.Register(nameof(CustomAdWidth), typeof(int), typeof(BannerAd), new PropertyMetadata(0, OnCustomSizeChanged));

    /// <summary>
    /// The desired ad width in density-independent pixels. Used when <see cref="AdSize" /> is set to <see cref="AdMob.AdSize.Custom" />.
    /// </summary>
    public int CustomAdWidth
    {
        get => (int)GetValue(CustomAdWidthProperty);
        set => SetValue(CustomAdWidthProperty, value);
    }

    /// <summary>
    /// The desired ad height in density-independent pixels dependency property.
    /// </summary>
    public static readonly DependencyProperty CustomAdHeightProperty =
        DependencyProperty.Register(nameof(CustomAdHeight), typeof(int), typeof(BannerAd), new PropertyMetadata(0, OnCustomSizeChanged));

    /// <summary>
    /// The desired ad height in density-independent pixels. Used when <see cref="AdSize" /> is set to <see cref="AdMob.AdSize.Custom" />.
    /// </summary>
    public int CustomAdHeight
    {
        get => (int)GetValue(CustomAdHeightProperty);
        set => SetValue(CustomAdHeightProperty, value);
    }

    public BannerAd()
    {
        Loaded += OnLoaded;
        Unloaded += OnUnloaded;
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        LoadAd();
    }

    private void OnUnloaded(object sender, RoutedEventArgs e)
    {
        DestroyAd();
    }

    private static void OnAdUnitIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BannerAd bannerAd)
        {
            bannerAd.LoadAd();
        }
    }

    private static void OnAdSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BannerAd bannerAd)
        {
            bannerAd.LoadAd();
        }
    }

    private static void OnCustomSizeChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is BannerAd bannerAd && bannerAd.AdSize == AdSize.Custom)
        {
            bannerAd.LoadAd();
        }
    }

    partial void LoadAd();
    partial void DestroyAd();

    internal void RaiseOnAdLoaded() => OnAdLoaded?.Invoke(this, EventArgs.Empty);
    internal void RaiseOnAdFailedToLoad(IAdError error) => OnAdFailedToLoad?.Invoke(this, error);
    internal void RaiseOnAdImpression() => OnAdImpression?.Invoke(this, EventArgs.Empty);
    internal void RaiseOnAdClicked() => OnAdClicked?.Invoke(this, EventArgs.Empty);
    internal void RaiseOnAdSwiped() => OnAdSwiped?.Invoke(this, EventArgs.Empty);
    internal void RaiseOnAdOpened() => OnAdOpened?.Invoke(this, EventArgs.Empty);
    internal void RaiseOnAdClosed() => OnAdClosed?.Invoke(this, EventArgs.Empty);
}
