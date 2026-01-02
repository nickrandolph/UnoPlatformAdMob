# Uno.AdMob

AdMob wrapper library for Uno Platform applications.

## Overview

Uno.AdMob is a cross-platform library that brings Google AdMob advertising functionality to Uno Platform applications. It provides a simple and consistent API for displaying ads across Android, iOS, and other supported platforms.

## Features

- **Banner Ads**: Display banner advertisements in your app
- **Cross-Platform Support**: Works on Android and iOS
- **Easy Integration**: Simple API for adding ads to your Uno Platform apps
- **Customizable Ad Sizes**: Support for various ad sizes (Banner, Large Banner, Medium Rectangle, etc.)

## Supported Platforms

- ✅ Android
- ✅ iOS  
- ⚠️ WebAssembly (placeholder only)
- ⚠️ Windows (placeholder only)
- ⚠️ Desktop (placeholder only)

## Installation

```xml
<PackageReference Include="Uno.AdMob" Version="1.0.0" />
```

## Quick Start

### 1. Configure AdMob

In your `App.xaml.cs` or app initialization code, configure your AdMob settings:

```csharp
using Uno.AdMob.Configuration;

// Set default ad unit IDs
#if __ANDROID__
AdConfig.DefaultBannerAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // Test ad unit
#elif __IOS__
AdConfig.DefaultBannerAdUnitId = "ca-app-pub-3940256099942544/2934735716"; // Test ad unit
#endif
```

### 2. Add Banner Ad to XAML

```xml
<Page x:Class="YourApp.MainPage"
      xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
      xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
      xmlns:admob="using:Uno.AdMob">

    <Grid>
        <!-- Your content -->
        
        <admob:BannerAd 
            AdSize="Banner"
            VerticalAlignment="Bottom"
            HorizontalAlignment="Center" />
    </Grid>
</Page>
```

### 3. Set Up Platform-Specific Configuration

#### Android

Add your AdMob App ID to `AndroidManifest.xml`:

```xml
<manifest>
    <application>
        <meta-data
            android:name="com.google.android.gms.ads.APPLICATION_ID"
            android:value="ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy"/>
    </application>
</manifest>
```

#### iOS

Add your AdMob App ID to `Info.plist`:

```xml
<key>GADApplicationIdentifier</key>
<string>ca-app-pub-xxxxxxxxxxxxxxxx~yyyyyyyyyy</string>
```

## API Reference

### BannerAd Properties

- **AdUnitId** (string): The ad unit ID for this banner
- **AdSize** (AdSize): The size of the banner ad
- **CustomAdWidth** (int): Custom width when AdSize is set to Custom
- **CustomAdHeight** (int): Custom height when AdSize is set to Custom

### AdSize Enum

- `Banner`: 320x50 dp
- `LargeBanner`: 320x100 dp
- `MediumRectangle`: 300x250 dp
- `FullBanner`: 468x60 dp
- `Leaderboard`: 728x90 dp
- `SmartBanner`: Full-width, auto-height
- `Custom`: Custom dimensions

### BannerAd Events

- **OnAdLoaded**: Raised when an ad is successfully loaded
- **OnAdFailedToLoad**: Raised when an ad fails to load
- **OnAdImpression**: Raised when an ad impression is recorded
- **OnAdClicked**: Raised when an ad is clicked
- **OnAdOpened**: Raised when an ad opens an overlay
- **OnAdClosed**: Raised when the user returns to the app

## Testing

Use Google's test ad unit IDs during development:

**Android Banner**: `ca-app-pub-3940256099942544/6300978111`
**iOS Banner**: `ca-app-pub-3940256099942544/2934735716`

## License

MIT License - see LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Based On

This library is inspired by and adapted from [Plugin.AdMob](https://github.com/marius-bughiu/Plugin.AdMob) for .NET MAUI.
