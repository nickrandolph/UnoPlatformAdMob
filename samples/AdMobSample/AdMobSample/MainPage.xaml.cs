using Uno.AdMob.Configuration;

namespace AdMobSample;

public sealed partial class MainPage : Page
{
    public MainPage()
    {
        this.InitializeComponent();
        
        // Configure AdMob with test ad unit IDs
#if __ANDROID__
        AdConfig.DefaultBannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Android test banner
#elif __IOS__
        AdConfig.DefaultBannerAdUnitId = "ca-app-pub-3940256099942544/2934735716"; // iOS test banner
#endif

        // Set up event handlers
        BannerAd.OnAdLoaded += (s, e) => System.Diagnostics.Debug.WriteLine("Ad loaded successfully");
        BannerAd.OnAdFailedToLoad += (s, e) => System.Diagnostics.Debug.WriteLine($"Ad failed to load: {e.Message}");
        BannerAd.OnAdClicked += (s, e) => System.Diagnostics.Debug.WriteLine("Ad clicked");
    }
}
