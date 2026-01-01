# UnoPlatformAdMob

An AdMob wrapper control for Uno Platform that uses WebView2 to render Google AdMob ads using the JavaScript SDK.

## Features

- 🎯 Cross-platform AdMob ad display using WebView2
- 📱 Support for multiple ad formats (Banner, In-Article, In-Feed)
- 🔧 Easy integration with existing Uno Platform applications
- 📦 Available as a NuGet package

## Installation

Install the NuGet package:

```bash
dotnet add package UnoPlatformAdMob
```

Or via Package Manager:

```powershell
Install-Package UnoPlatformAdMob
```

## Usage

### Basic Example

Add the AdMobView control to your XAML:

```xaml
<Page xmlns:admob="using:UnoPlatformAdMob">
    <admob:AdMobView 
        AdUnitId="ca-app-pub-XXXXXXXXXXXXXXXX/YYYYYYYYYY"
        AdFormat="Banner"
        Height="250"/>
</Page>
```

### Properties

- **AdUnitId** (string): Your Google AdMob ad unit ID
- **AdFormat** (AdFormat enum): The format of the ad
  - `Banner`: Standard banner ad
  - `InArticle`: In-article ad format
  - `InFeed`: In-feed ad format

### Getting an AdMob Ad Unit ID

1. Sign up for [Google AdMob](https://admob.google.com/)
2. Create a new app in your AdMob account
3. Create an ad unit for your app
4. Copy the ad unit ID (format: `ca-app-pub-XXXXXXXXXXXXXXXX/YYYYYYYYYY`)

### Test Ad Unit IDs

For testing purposes, you can use Google's test ad unit IDs:

- Banner: `ca-app-pub-3940256099942544/6300978111`
- Interstitial: `ca-app-pub-3940256099942544/1033173712`
- Rewarded: `ca-app-pub-3940256099942544/5224354917`

## Sample Application

The repository includes a sample Uno Platform application demonstrating the AdMobView control. To run the sample:

```bash
cd UnoPlatformAdMob.Sample
dotnet run
```

## Requirements

- .NET 10.0 or later
- Uno Platform 6.4 or later
- WebView2 support on target platform

## Supported Platforms

- Windows (Desktop)
- WebAssembly
- iOS
- Android
- macOS
- Linux (Skia)

## Building from Source

```bash
# Clone the repository
git clone https://github.com/nickrandolph/UnoPlatformAdMob.git
cd UnoPlatformAdMob

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run tests (if available)
dotnet test

# Pack NuGet package
dotnet pack
```

## CI/CD

This project uses GitHub Actions for continuous integration and NuGet package generation. The pipeline:

- ✅ Builds the library and sample application
- ✅ Runs tests (when available)
- ✅ Generates NuGet packages
- ✅ Publishes packages on release

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## License

This project is licensed under the MIT License.

## Resources

- [Google AdMob Documentation](https://developers.google.com/admob)
- [AdMob JavaScript SDK](https://developers.google.com/admob/js/quickstart)
- [Uno Platform Documentation](https://platform.uno/docs/)
- [WebView2 Documentation](https://docs.microsoft.com/microsoft-edge/webview2/)

## Support

For issues and questions, please use the [GitHub Issues](https://github.com/nickrandolph/UnoPlatformAdMob/issues) page.
