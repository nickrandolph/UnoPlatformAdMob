# UnoPlatform.AdMob

A Google Mobile Ads (AdMob) wrapper for Uno Platform applications. This library provides cross-platform support for displaying banner, interstitial, and rewarded ads in your Uno Platform apps.

## Features

- **Banner Ads**: Display standard banner advertisements in your UI
- **Interstitial Ads**: Show full-screen ads between content
- **Rewarded Ads**: Reward users for watching video advertisements
- **Cross-Platform**: Works across iOS, Android, WebAssembly, Windows, and more
- **Type-Safe API**: Strongly-typed C# API based on the Flutter Google Mobile Ads SDK

## Installation

Install the NuGet package:

```bash
dotnet add package UnoPlatform.AdMob
```

Or via Package Manager:

```
Install-Package UnoPlatform.AdMob
```

## Quick Start

### 1. Initialize the SDK

Initialize the Mobile Ads SDK when your app starts:

```csharp
using UnoPlatform.AdMob;

// In your App.xaml.cs or startup code
await MobileAds.Initialize();
```

### 2. Display a Banner Ad

Add a banner ad to your XAML:

```xml
<Page xmlns:admob="using:UnoPlatform.AdMob">
    <StackPanel>
        <admob:BannerAdView 
            x:Name="BannerAd"
            AdUnitId="ca-app-pub-3940256099942544/6300978111"
            Height="60"
            HorizontalAlignment="Center"
            AdLoaded="BannerAd_AdLoaded"
            AdFailedToLoad="BannerAd_AdFailedToLoad"/>
    </StackPanel>
</Page>
```

Load the ad in your code-behind:

```csharp
private void LoadBannerAd()
{
    var request = new AdRequest();
    BannerAd.LoadAd(request);
}

private void BannerAd_AdLoaded(object sender, AdEventArgs e)
{
    // Ad loaded successfully
}

private void BannerAd_AdFailedToLoad(object sender, AdLoadErrorEventArgs e)
{
    // Handle ad load failure
    Console.WriteLine($"Ad failed to load: {e.Error.Message}");
}
```

### 3. Show an Interstitial Ad

```csharp
private InterstitialAd _interstitialAd;

private void LoadInterstitialAd()
{
    _interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
    
    _interstitialAd.AdLoaded += (s, e) =>
    {
        Console.WriteLine("Interstitial ad loaded");
    };
    
    _interstitialAd.AdDismissed += (s, e) =>
    {
        Console.WriteLine("Interstitial ad dismissed");
    };
    
    var request = new AdRequest();
    _interstitialAd.Load(request);
}

private void ShowInterstitialAd()
{
    _interstitialAd?.Show();
}
```

### 4. Show a Rewarded Ad

```csharp
private RewardedAd _rewardedAd;

private void LoadRewardedAd()
{
    _rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
    
    _rewardedAd.AdLoaded += (s, e) =>
    {
        Console.WriteLine("Rewarded ad loaded");
    };
    
    _rewardedAd.UserEarnedReward += (s, e) =>
    {
        Console.WriteLine($"User earned reward: {e.Amount} {e.Type}");
        // Grant the reward to the user
    };
    
    var request = new AdRequest();
    _rewardedAd.Load(request);
}

private void ShowRewardedAd()
{
    _rewardedAd?.Show();
}
```

## API Reference

### AdSize

Predefined ad sizes for banner ads:

- `AdSize.Banner` - 320x50
- `AdSize.LargeBanner` - 320x100
- `AdSize.MediumRectangle` - 300x250
- `AdSize.FullBanner` - 468x60
- `AdSize.Leaderboard` - 728x90
- `AdSize.SmartBanner` - Adaptive banner

### AdRequest

Configuration for ad requests:

```csharp
var request = new AdRequest
{
    RequestNonPersonalizedAdsOnly = false,
    ContentUrl = "https://example.com"
};
request.Keywords.Add("gaming");
request.TestDeviceIds.Add("YOUR_TEST_DEVICE_ID");
```

### BannerAdView Properties

- `AdUnitId` - Your AdMob ad unit ID
- `AdSize` - The size of the banner ad

### BannerAdView Events

- `AdLoaded` - Fired when an ad successfully loads
- `AdFailedToLoad` - Fired when an ad fails to load
- `AdOpened` - Fired when an ad is opened
- `AdClosed` - Fired when an ad is closed
- `AdImpression` - Fired when an impression is recorded

### InterstitialAd Events

- `AdLoaded` - Fired when an ad successfully loads
- `AdFailedToLoad` - Fired when an ad fails to load
- `AdShown` - Fired when an ad is shown
- `AdFailedToShow` - Fired when an ad fails to show
- `AdDismissed` - Fired when an ad is dismissed
- `AdImpression` - Fired when an impression is recorded

### RewardedAd Events

- `AdLoaded` - Fired when an ad successfully loads
- `AdFailedToLoad` - Fired when an ad fails to load
- `AdShown` - Fired when an ad is shown
- `AdFailedToShow` - Fired when an ad fails to show
- `AdDismissed` - Fired when an ad is dismissed
- `UserEarnedReward` - Fired when a user earns a reward
- `AdImpression` - Fired when an impression is recorded

## Test Ad Unit IDs

Google provides test ad unit IDs for development. Replace these with your actual ad unit IDs before publishing:

- **Banner**: `ca-app-pub-3940256099942544/6300978111`
- **Interstitial**: `ca-app-pub-3940256099942544/1033173712`
- **Rewarded**: `ca-app-pub-3940256099942544/5224354917`

## Platform-Specific Setup

### Android

Add the AdMob App ID to your `AndroidManifest.xml`:

```xml
<meta-data
    android:name="com.google.android.gms.ads.APPLICATION_ID"
    android:value="ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy"/>
```

### iOS

Add the AdMob App ID to your `Info.plist`:

```xml
<key>GADApplicationIdentifier</key>
<string>ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy</string>
```

## Sample Application

Check out the sample application in the `samples/` directory for complete working examples of all ad types.

## License

This project is licensed under the MIT License.

## Contributing

Contributions are welcome! Please open an issue or submit a pull request.

## Acknowledgments

This library is inspired by the [Flutter Google Mobile Ads SDK](https://github.com/googleads/googleads-mobile-flutter).
