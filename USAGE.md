# Usage Examples

This document provides practical examples of using the UnoPlatform.AdMob library.

## Table of Contents

1. [Setup](#setup)
2. [Banner Ads](#banner-ads)
3. [Interstitial Ads](#interstitial-ads)
4. [Rewarded Ads](#rewarded-ads)
5. [Advanced Configuration](#advanced-configuration)

## Setup

### Step 1: Install the Package

```bash
dotnet add package UnoPlatform.AdMob
```

### Step 2: Initialize the SDK

In your `App.xaml.cs` or application startup:

```csharp
using UnoPlatform.AdMob;

protected override async void OnLaunched(LaunchActivatedEventArgs e)
{
    // Initialize AdMob SDK
    await MobileAds.Initialize();
    
    // Configure test devices (optional, for testing)
    MobileAds.SetRequestConfiguration("YOUR_TEST_DEVICE_ID");
    
    // Rest of your app initialization...
}
```

## Banner Ads

### Simple Banner Ad

```xml
<Page xmlns:admob="using:UnoPlatform.AdMob">
    <Grid>
        <Grid.RowDefinitions>
            <RowDefinition Height="*"/>
            <RowDefinition Height="Auto"/>
        </Grid.RowDefinitions>
        
        <!-- Your content -->
        <TextBlock Grid.Row="0" Text="Main Content" />
        
        <!-- Banner Ad at bottom -->
        <admob:BannerAdView Grid.Row="1"
                            x:Name="BannerAd"
                            AdUnitId="ca-app-pub-3940256099942544/6300978111"
                            Height="50"
                            HorizontalAlignment="Center"
                            AdLoaded="OnBannerAdLoaded"/>
    </Grid>
</Page>
```

Code-behind:

```csharp
private void OnBannerAdLoaded(object sender, AdEventArgs e)
{
    Console.WriteLine("Banner ad loaded successfully!");
}

private void LoadBannerAd()
{
    var request = new AdRequest();
    BannerAd.LoadAd(request);
}
```

### Different Banner Sizes

```csharp
// Create banner ads of different sizes
var smallBanner = new BannerAdView
{
    AdUnitId = "ca-app-pub-3940256099942544/6300978111",
    AdSize = AdSize.Banner, // 320x50
    Height = 50
};

var mediumRectangle = new BannerAdView
{
    AdUnitId = "ca-app-pub-3940256099942544/6300978111",
    AdSize = AdSize.MediumRectangle, // 300x250
    Height = 250
};

var leaderboard = new BannerAdView
{
    AdUnitId = "ca-app-pub-3940256099942544/6300978111",
    AdSize = AdSize.Leaderboard, // 728x90
    Height = 90
};
```

## Interstitial Ads

### Basic Interstitial

```csharp
public class GamePage : Page
{
    private InterstitialAd _interstitialAd;
    
    private void LoadInterstitialAd()
    {
        // Create interstitial ad
        _interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
        
        // Subscribe to events
        _interstitialAd.AdLoaded += OnInterstitialAdLoaded;
        _interstitialAd.AdFailedToLoad += OnInterstitialAdFailedToLoad;
        _interstitialAd.AdShown += OnInterstitialAdShown;
        _interstitialAd.AdDismissed += OnInterstitialAdDismissed;
        
        // Load the ad
        var request = new AdRequest();
        _interstitialAd.Load(request);
    }
    
    private void OnInterstitialAdLoaded(object sender, AdEventArgs e)
    {
        Console.WriteLine("Interstitial ad loaded - ready to show");
    }
    
    private void OnInterstitialAdFailedToLoad(object sender, AdLoadErrorEventArgs e)
    {
        Console.WriteLine($"Failed to load interstitial ad: {e.Error.Message}");
    }
    
    private void OnInterstitialAdShown(object sender, AdEventArgs e)
    {
        Console.WriteLine("Interstitial ad shown");
    }
    
    private void OnInterstitialAdDismissed(object sender, AdEventArgs e)
    {
        Console.WriteLine("Interstitial ad dismissed");
        // Load a new ad for next time
        LoadInterstitialAd();
    }
    
    private void ShowInterstitialAd()
    {
        if (_interstitialAd != null)
        {
            _interstitialAd.Show();
        }
    }
    
    // Example: Show ad between game levels
    private void OnLevelComplete()
    {
        SaveProgress();
        ShowInterstitialAd();
        NavigateToNextLevel();
    }
}
```

### Preloading Interstitial Ads

```csharp
public class AdManager
{
    private InterstitialAd _nextAd;
    private bool _isAdReady;
    
    public AdManager()
    {
        PreloadNextAd();
    }
    
    private void PreloadNextAd()
    {
        _nextAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
        _nextAd.AdLoaded += (s, e) => _isAdReady = true;
        _nextAd.AdFailedToLoad += (s, e) => _isAdReady = false;
        _nextAd.AdDismissed += (s, e) =>
        {
            _isAdReady = false;
            PreloadNextAd(); // Preload the next ad
        };
        
        var request = new AdRequest();
        _nextAd.Load(request);
    }
    
    public void ShowAdIfReady()
    {
        if (_isAdReady && _nextAd != null)
        {
            _nextAd.Show();
        }
    }
}
```

## Rewarded Ads

### Basic Rewarded Ad

```csharp
public class RewardPage : Page
{
    private RewardedAd _rewardedAd;
    private int _userCoins = 100;
    
    private void LoadRewardedAd()
    {
        _rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        
        _rewardedAd.AdLoaded += (s, e) =>
        {
            Console.WriteLine("Rewarded ad loaded");
            EnableRewardButton();
        };
        
        _rewardedAd.AdFailedToLoad += (s, e) =>
        {
            Console.WriteLine($"Failed to load rewarded ad: {e.Error.Message}");
            DisableRewardButton();
        };
        
        _rewardedAd.UserEarnedReward += OnUserEarnedReward;
        
        _rewardedAd.AdDismissed += (s, e) =>
        {
            Console.WriteLine("Rewarded ad dismissed");
            LoadRewardedAd(); // Load next ad
        };
        
        var request = new AdRequest();
        _rewardedAd.Load(request);
    }
    
    private void OnUserEarnedReward(object sender, RewardEventArgs e)
    {
        // Grant the reward to the user
        _userCoins += e.Amount;
        Console.WriteLine($"User earned {e.Amount} {e.Type}");
        UpdateUI();
    }
    
    private void ShowRewardedAd_Click(object sender, RoutedEventArgs e)
    {
        if (_rewardedAd != null)
        {
            _rewardedAd.Show();
        }
    }
}
```

### Reward System Example

```csharp
public class CoinRewardSystem
{
    private RewardedAd _coinAd;
    private Action<int> _onCoinsGranted;
    
    public CoinRewardSystem(Action<int> onCoinsGranted)
    {
        _onCoinsGranted = onCoinsGranted;
        LoadCoinAd();
    }
    
    private void LoadCoinAd()
    {
        _coinAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
        
        _coinAd.AdLoaded += (s, e) => UpdateButtonState(true);
        _coinAd.AdFailedToLoad += (s, e) => UpdateButtonState(false);
        
        _coinAd.UserEarnedReward += (s, e) =>
        {
            // Verify the reward type and amount
            if (e.Type == "coin" || e.Type == "coins")
            {
                _onCoinsGranted?.Invoke(e.Amount);
                SaveRewardToBackend(e.Amount);
            }
        };
        
        _coinAd.AdDismissed += (s, e) => LoadCoinAd();
        
        var request = new AdRequest();
        _coinAd.Load(request);
    }
    
    public void ShowRewardVideo()
    {
        _coinAd?.Show();
    }
    
    private void SaveRewardToBackend(int coins)
    {
        // Save to your backend to prevent cheating
        // API call to verify and credit the reward
    }
    
    private void UpdateButtonState(bool enabled)
    {
        // Update UI to enable/disable the "Watch Ad" button
    }
}
```

## Advanced Configuration

### Custom Ad Request

```csharp
var request = new AdRequest
{
    RequestNonPersonalizedAdsOnly = true,
    ContentUrl = "https://example.com/article-page"
};

// Add keywords for better targeting
request.Keywords.Add("gaming");
request.Keywords.Add("action");
request.Keywords.Add("mobile");

// Add test device IDs
request.TestDeviceIds.Add("33BE2250B43518CCDA7DE426D04EE231");

// Load ad with custom request
bannerAd.LoadAd(request);
```

### Error Handling

```csharp
private void SetupAdWithErrorHandling()
{
    var bannerAd = new BannerAdView
    {
        AdUnitId = GetAdUnitId(),
        AdSize = AdSize.Banner
    };
    
    bannerAd.AdLoaded += (s, e) =>
    {
        Console.WriteLine("✓ Ad loaded successfully");
    };
    
    bannerAd.AdFailedToLoad += (s, e) =>
    {
        Console.WriteLine($"✗ Ad failed to load");
        Console.WriteLine($"  Error Code: {e.Error.Code}");
        Console.WriteLine($"  Error Message: {e.Error.Message}");
        Console.WriteLine($"  Error Domain: {e.Error.Domain}");
        
        // Implement retry logic or fallback
        RetryAdLoad(bannerAd);
    };
    
    var request = new AdRequest();
    bannerAd.LoadAd(request);
}

private async void RetryAdLoad(BannerAdView ad)
{
    await Task.Delay(5000); // Wait 5 seconds before retrying
    var request = new AdRequest();
    ad.LoadAd(request);
}
```

### Environment-Specific Ad Unit IDs

```csharp
public class AdConfig
{
    public static string GetBannerAdUnitId()
    {
#if DEBUG
        // Test ad unit for development
        return "ca-app-pub-3940256099942544/6300978111";
#else
        // Production ad unit
        return "ca-app-pub-XXXXXXXXXXXXXXXX/YYYYYYYYYY";
#endif
    }
    
    public static string GetInterstitialAdUnitId()
    {
#if DEBUG
        return "ca-app-pub-3940256099942544/1033173712";
#else
        return "ca-app-pub-XXXXXXXXXXXXXXXX/ZZZZZZZZZZ";
#endif
    }
    
    public static string GetRewardedAdUnitId()
    {
#if DEBUG
        return "ca-app-pub-3940256099942544/5224354917";
#else
        return "ca-app-pub-XXXXXXXXXXXXXXXX/WWWWWWWWWW";
#endif
    }
}
```

## Best Practices

1. **Always preload ads** before showing them to minimize wait time
2. **Handle all error cases** gracefully with appropriate fallbacks
3. **Use test ad unit IDs** during development to avoid policy violations
4. **Respect user experience** - don't show too many ads
5. **Implement frequency capping** for interstitial and rewarded ads
6. **Save reward state** on your backend to prevent manipulation
7. **Test on real devices** to ensure ads display correctly
8. **Follow AdMob policies** regarding ad placement and frequency
