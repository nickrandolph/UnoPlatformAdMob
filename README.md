# UnoPlatform.AdSense

A Google AdSense wrapper control for Uno Platform applications that uses WebView2 to render advertisements.

## Features

- **Cross-platform**: Works on all Uno Platform supported targets
- **Easy Integration**: Simple XAML control with configurable properties
- **Responsive Ads**: Supports responsive and fixed-size ad formats
- **WebView2 Based**: Uses WebView2 for reliable ad rendering

## Installation

Install the NuGet package:

```bash
dotnet add package UnoPlatform.AdSense
```

Or via Package Manager:

```powershell
Install-Package UnoPlatform.AdSense
```

## Usage

### Basic Usage

Add the namespace to your XAML page:

```xaml
xmlns:adsense="using:UnoPlatform.AdSense"
```

Add the control to your page:

```xaml
<adsense:AdSenseControl 
    ClientId="ca-pub-XXXXXXXXXXXXXXXX"
    SlotId="YYYYYYYYYY"
    AdFormat="auto"
    IsResponsive="True"
    Height="250" />
```

### Properties

| Property | Type | Description | Default |
|----------|------|-------------|---------|
| `ClientId` | string | Your AdSense publisher ID (e.g., "ca-pub-XXXXXXXXXXXXXXXX") | Empty |
| `SlotId` | string | Your ad unit/slot ID | Empty |
| `AdFormat` | string | Ad format: "auto", "rectangle", "vertical", "horizontal" | "auto" |
| `IsResponsive` | bool | Enable responsive ads | true |

### Getting Your AdSense Credentials

1. Sign up for [Google AdSense](https://www.google.com/adsense/)
2. Create an ad unit in your AdSense dashboard
3. Copy your Publisher ID (ClientId) - looks like "ca-pub-XXXXXXXXXXXXXXXX"
4. Copy your Ad Unit ID (SlotId)

### Example

```xaml
<Page xmlns:adsense="using:UnoPlatform.AdSense">
    <Grid>
        <!-- Responsive banner ad -->
        <adsense:AdSenseControl 
            ClientId="ca-pub-1234567890123456"
            SlotId="9876543210"
            IsResponsive="True"
            Height="200" />
    </Grid>
</Page>
```

## Sample Application

The repository includes a sample Uno Platform application demonstrating the AdSense control usage. See the `samples/UnoPlatform.AdSense.Sample` directory.

## Building from Source

### Prerequisites

- .NET 10.0 SDK or later
- Uno Platform workloads (installed via `dotnet workload restore`)

### Build Steps

```bash
# Clone the repository
git clone https://github.com/nickrandolph/UnoPlatformAdMob.git
cd UnoPlatformAdMob

# Restore dependencies
dotnet restore

# Build the solution
dotnet build

# Run the sample (specify a target platform)
dotnet run --project samples/UnoPlatform.AdSense.Sample/UnoPlatform.AdSense.Sample/UnoPlatform.AdSense.Sample.csproj
```

## Platform Support

- ✅ Windows (WinUI)
- ✅ WebAssembly (WASM)
- ✅ Android
- ✅ iOS
- ✅ macOS
- ✅ Linux (Skia)

## Requirements

- Google AdSense account
- Valid AdSense credentials (Client ID and Slot ID)
- WebView2 support on target platform

## License

MIT License - See LICENSE file for details

## Contributing

Contributions are welcome! Please feel free to submit a Pull Request.

## Support

For issues and questions:
- GitHub Issues: https://github.com/nickrandolph/UnoPlatformAdMob/issues
- Uno Platform Documentation: https://platform.uno/docs/

## Disclaimer

This is an unofficial library and is not affiliated with or endorsed by Google. Google AdSense is a trademark of Google LLC.
