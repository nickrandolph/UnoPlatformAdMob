#if __WASM__ || WINDOWS || __MACOS__ || __SKIA__
namespace Uno.AdMob;

public partial class BannerAd
{
    partial void LoadAd()
    {
        // AdMob is not supported on this platform
        // Display a placeholder or message
        var textBlock = new Microsoft.UI.Xaml.Controls.TextBlock
        {
            Text = "AdMob ads are not supported on this platform",
            HorizontalAlignment = Microsoft.UI.Xaml.HorizontalAlignment.Center,
            VerticalAlignment = Microsoft.UI.Xaml.VerticalAlignment.Center
        };
        Content = textBlock;
    }

    partial void DestroyAd()
    {
        Content = null;
    }
}
#endif
