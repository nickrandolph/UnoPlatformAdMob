# Contributing to UnoPlatform.AdMob

Thank you for your interest in contributing to the UnoPlatform.AdMob project! This document provides guidelines and information for contributors.

## Getting Started

1. **Fork the repository** on GitHub
2. **Clone your fork** locally:
   ```bash
   git clone https://github.com/YOUR-USERNAME/UnoPlatformAdMob.git
   cd UnoPlatformAdMob
   ```
3. **Create a branch** for your changes:
   ```bash
   git checkout -b feature/your-feature-name
   ```

## Development Setup

### Prerequisites

- .NET SDK 10.0 or later
- Visual Studio 2022 (Windows) or Visual Studio Code with C# extensions
- For mobile development: Xcode (macOS), Android SDK

### Building the Project

```bash
# Restore dependencies
dotnet restore

# Build the library
dotnet build src/UnoPlatform.AdMob/UnoPlatform.AdMob.csproj

# Build in Release mode
dotnet build src/UnoPlatform.AdMob/UnoPlatform.AdMob.csproj -c Release

# Create NuGet package
dotnet pack src/UnoPlatform.AdMob/UnoPlatform.AdMob.csproj -c Release
```

### Running the Sample App

```bash
# For WebAssembly
dotnet build samples/UnoPlatform.AdMob.Sample/UnoPlatform.AdMob.Sample.Wasm/UnoPlatform.AdMob.Sample.Wasm.csproj

# For Android (requires Android SDK)
dotnet build samples/UnoPlatform.AdMob.Sample/UnoPlatform.AdMob.Sample.Mobile/UnoPlatform.AdMob.Sample.Mobile.csproj -f net7.0-android

# For iOS (requires macOS and Xcode)
dotnet build samples/UnoPlatform.AdMob.Sample/UnoPlatform.AdMob.Sample.Mobile/UnoPlatform.AdMob.Sample.Mobile.csproj -f net7.0-ios
```

## Coding Guidelines

### Code Style

- Follow standard C# coding conventions
- Use meaningful variable and method names
- Add XML documentation comments to all public APIs
- Keep methods focused and concise

### Example

```csharp
/// <summary>
/// Represents a banner advertisement view.
/// </summary>
public class BannerAdView : Control
{
    /// <summary>
    /// Gets or sets the Ad Unit ID for this banner ad.
    /// </summary>
    public string AdUnitId { get; set; }
}
```

### Platform-Specific Code

Use partial classes for platform-specific implementations:

```csharp
// BannerAdView.cs - Shared code
public partial class BannerAdView
{
    partial void LoadAdPartial(AdRequest request);
}

// BannerAdView.Android.cs - Android-specific
public partial class BannerAdView
{
    partial void LoadAdPartial(AdRequest request)
    {
        // Android-specific implementation
    }
}

// BannerAdView.Default.cs - Default/fallback
public partial class BannerAdView
{
    partial void LoadAdPartial(AdRequest request)
    {
        // Default implementation or error handling
    }
}
```

## Pull Request Process

1. **Update documentation** if you're adding new features
2. **Add XML comments** to all public APIs
3. **Test your changes** on multiple platforms if possible
4. **Update the README** if necessary
5. **Submit a pull request** with a clear description of your changes

### PR Description Template

```markdown
## Description
Brief description of the changes

## Type of Change
- [ ] Bug fix
- [ ] New feature
- [ ] Breaking change
- [ ] Documentation update

## Platforms Tested
- [ ] Android
- [ ] iOS
- [ ] WebAssembly
- [ ] Windows
- [ ] macOS

## Checklist
- [ ] My code follows the code style of this project
- [ ] I have added XML documentation comments
- [ ] I have updated the README (if applicable)
- [ ] My changes generate no new warnings
- [ ] I have tested my changes
```

## Adding New Features

### Adding a New Ad Type

1. Create the main class (e.g., `NativeAd.cs`)
2. Add platform-specific implementations (e.g., `NativeAd.Android.cs`, `NativeAd.iOS.cs`)
3. Add a default implementation (e.g., `NativeAd.Default.cs`)
4. Update documentation and README
5. Add usage examples to USAGE.md

### Adding New Events

```csharp
/// <summary>
/// Occurs when ad is clicked.
/// </summary>
public event EventHandler<AdEventArgs> AdClicked;

protected void RaiseAdClicked() => AdClicked?.Invoke(this, new AdEventArgs());
```

## Testing

### Manual Testing

1. Test with **test ad unit IDs** (provided by Google)
2. Verify on multiple platforms
3. Test error scenarios (no internet, invalid ad unit IDs)
4. Test ad lifecycle (load, show, dismiss)

### Test Ad Unit IDs

- Banner: `ca-app-pub-3940256099942544/6300978111`
- Interstitial: `ca-app-pub-3940256099942544/1033173712`
- Rewarded: `ca-app-pub-3940256099942544/5224354917`

## Platform-Specific Implementations

### Android

- Use Xamarin.Google.Android.Play.Services.Ads NuGet package
- Implement native AdView integration
- Handle activity lifecycle

### iOS

- Use Google-Mobile-Ads-SDK
- Implement UIView integration
- Handle view controller lifecycle

### WebAssembly

- Use Google AdSense or display placeholder
- Consider using JavaScript interop for ad display

## Documentation

### XML Documentation

All public APIs must have XML documentation:

```csharp
/// <summary>
/// Loads an advertisement with the specified request parameters.
/// </summary>
/// <param name="request">The ad request containing targeting information.</param>
/// <exception cref="ArgumentNullException">Thrown when request is null.</exception>
public void LoadAd(AdRequest request)
{
    // Implementation
}
```

### README Updates

When adding new features, update:
- Feature list
- Quick Start guide
- API Reference
- Examples

## Release Process

1. Update version number in `UnoPlatform.AdMob.csproj`
2. Update CHANGELOG.md
3. Create a GitHub release
4. CI/CD pipeline automatically publishes to NuGet

## Questions?

- Open an issue for bugs or feature requests
- Start a discussion for general questions
- Check existing issues before creating new ones

## Code of Conduct

Be respectful and constructive in all interactions. We're all here to build something great together!

Thank you for contributing! 🎉
