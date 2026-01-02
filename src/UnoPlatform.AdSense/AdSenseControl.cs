using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace UnoPlatform.AdSense;

/// <summary>
/// A control that displays Google AdSense advertisements using WebView2
/// </summary>
public sealed partial class AdSenseControl : Control
{
    private WebView2? _webView;

    public AdSenseControl()
    {
        DefaultStyleKey = typeof(AdSenseControl);
    }

    /// <summary>
    /// Gets or sets the AdSense client ID (e.g., "ca-pub-XXXXXXXXXXXXXXXX")
    /// </summary>
    public string ClientId
    {
        get { return (string)GetValue(ClientIdProperty); }
        set { SetValue(ClientIdProperty, value); }
    }

    public static readonly DependencyProperty ClientIdProperty =
        DependencyProperty.Register(
            nameof(ClientId),
            typeof(string),
            typeof(AdSenseControl),
            new PropertyMetadata(string.Empty, OnAdPropertyChanged));

    /// <summary>
    /// Gets or sets the AdSense slot ID
    /// </summary>
    public string SlotId
    {
        get { return (string)GetValue(SlotIdProperty); }
        set { SetValue(SlotIdProperty, value); }
    }

    public static readonly DependencyProperty SlotIdProperty =
        DependencyProperty.Register(
            nameof(SlotId),
            typeof(string),
            typeof(AdSenseControl),
            new PropertyMetadata(string.Empty, OnAdPropertyChanged));

    /// <summary>
    /// Gets or sets the ad format (e.g., "auto", "rectangle", "vertical", "horizontal")
    /// </summary>
    public string AdFormat
    {
        get { return (string)GetValue(AdFormatProperty); }
        set { SetValue(AdFormatProperty, value); }
    }

    public static readonly DependencyProperty AdFormatProperty =
        DependencyProperty.Register(
            nameof(AdFormat),
            typeof(string),
            typeof(AdSenseControl),
            new PropertyMetadata("auto", OnAdPropertyChanged));

    /// <summary>
    /// Gets or sets whether the ad should be responsive
    /// </summary>
    public bool IsResponsive
    {
        get { return (bool)GetValue(IsResponsiveProperty); }
        set { SetValue(IsResponsiveProperty, value); }
    }

    public static readonly DependencyProperty IsResponsiveProperty =
        DependencyProperty.Register(
            nameof(IsResponsive),
            typeof(bool),
            typeof(AdSenseControl),
            new PropertyMetadata(true, OnAdPropertyChanged));

    private static void OnAdPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AdSenseControl control && control._webView != null)
        {
            control.LoadAd();
        }
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        
        _webView = GetTemplateChild("PART_WebView") as WebView2;
        
        if (_webView != null)
        {
            LoadAd();
        }
    }

    private void LoadAd()
    {
        if (_webView == null || string.IsNullOrEmpty(ClientId) || string.IsNullOrEmpty(SlotId))
        {
            return;
        }

        var html = GenerateAdHtml();
        _webView.NavigateToString(html);
    }

    private string GenerateAdHtml()
    {
        var responsiveStyle = IsResponsive ? "display:block" : "";
        var adFormatAttribute = IsResponsive ? "" : $"data-ad-format=\"{AdFormat}\"";

        return $@"<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"" />
    <meta name=""viewport"" content=""width=device-width, initial-scale=1.0"" />
    <style>
        body {{
            margin: 0;
            padding: 0;
            overflow: hidden;
        }}
    </style>
    <script async src=""https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client={ClientId}""
            crossorigin=""anonymous""></script>
</head>
<body>
    <ins class=""adsbygoogle""
         style=""{responsiveStyle}""
         data-ad-client=""{ClientId}""
         data-ad-slot=""{SlotId}""
         {adFormatAttribute}></ins>
    <script>
        (adsbygoogle = window.adsbygoogle || []).push({{}});
    </script>
</body>
</html>";
    }
}
