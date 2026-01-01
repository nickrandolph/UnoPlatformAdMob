using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.Web.WebView2.Core;

namespace UnoPlatformAdMob;

/// <summary>
/// A control that displays Google AdMob ads using WebView2 and the AdMob JavaScript SDK.
/// </summary>
public partial class AdMobView : Control
{
    private WebView2? _webView;

    /// <summary>
    /// Identifies the AdUnitId dependency property.
    /// </summary>
    public static readonly DependencyProperty AdUnitIdProperty =
        DependencyProperty.Register(
            nameof(AdUnitId),
            typeof(string),
            typeof(AdMobView),
            new PropertyMetadata(string.Empty, OnAdUnitIdChanged));

    /// <summary>
    /// Identifies the AdFormat dependency property.
    /// </summary>
    public static readonly DependencyProperty AdFormatProperty =
        DependencyProperty.Register(
            nameof(AdFormat),
            typeof(AdFormat),
            typeof(AdMobView),
            new PropertyMetadata(AdFormat.Banner, OnAdFormatChanged));

    /// <summary>
    /// Gets or sets the AdMob Ad Unit ID.
    /// </summary>
    public string AdUnitId
    {
        get => (string)GetValue(AdUnitIdProperty);
        set => SetValue(AdUnitIdProperty, value);
    }

    /// <summary>
    /// Gets or sets the ad format (Banner, Interstitial, etc.).
    /// </summary>
    public AdFormat AdFormat
    {
        get => (AdFormat)GetValue(AdFormatProperty);
        set => SetValue(AdFormatProperty, value);
    }

    public AdMobView()
    {
        DefaultStyleKey = typeof(AdMobView);
        Loaded += OnLoaded;
    }

    protected override void OnApplyTemplate()
    {
        base.OnApplyTemplate();
        _webView = GetTemplateChild("PART_WebView") as WebView2;
        
        if (_webView != null)
        {
            _webView.NavigationCompleted += OnNavigationCompleted;
            LoadAdContent();
        }
    }

    private void OnLoaded(object sender, RoutedEventArgs e)
    {
        if (_webView != null)
        {
            LoadAdContent();
        }
    }

    private static void OnAdUnitIdChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AdMobView adMobView)
        {
            adMobView.LoadAdContent();
        }
    }

    private static void OnAdFormatChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is AdMobView adMobView)
        {
            adMobView.LoadAdContent();
        }
    }

    private void LoadAdContent()
    {
        if (_webView == null || string.IsNullOrEmpty(AdUnitId))
        {
            return;
        }

        var html = GenerateAdHtml();
        _webView.NavigateToString(html);
    }

    private void OnNavigationCompleted(WebView2 sender, CoreWebView2NavigationCompletedEventArgs args)
    {
        // Ad loaded successfully
    }

    private string GenerateAdHtml()
    {
        var adFormatClass = AdFormat switch
        {
            AdFormat.Banner => "adsbygoogle",
            AdFormat.InArticle => "adsbygoogle",
            AdFormat.InFeed => "adsbygoogle",
            _ => "adsbygoogle"
        };

        var adFormatValue = AdFormat switch
        {
            AdFormat.Banner => "auto",
            AdFormat.InArticle => "fluid",
            AdFormat.InFeed => "fluid",
            _ => "auto"
        };

        return $@"
<!DOCTYPE html>
<html>
<head>
    <meta charset=""utf-8"">
    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <style>
        body {{
            margin: 0;
            padding: 0;
            background-color: transparent;
        }}
        .ad-container {{
            width: 100%;
            height: 100%;
            display: flex;
            align-items: center;
            justify-content: center;
        }}
    </style>
    <script async src=""https://pagead2.googlesyndication.com/pagead/js/adsbygoogle.js?client={AdUnitId}""
            crossorigin=""anonymous""></script>
</head>
<body>
    <div class=""ad-container"">
        <ins class=""{adFormatClass}""
             style=""display:block""
             data-ad-client=""{AdUnitId}""
             data-ad-format=""{adFormatValue}""
             data-full-width-responsive=""true""></ins>
    </div>
    <script>
        (adsbygoogle = window.adsbygoogle || []).push({{}});
    </script>
</body>
</html>";
    }
}

/// <summary>
/// Specifies the format of the AdMob ad.
/// </summary>
public enum AdFormat
{
    /// <summary>
    /// Standard banner ad.
    /// </summary>
    Banner,
    
    /// <summary>
    /// In-article ad format.
    /// </summary>
    InArticle,
    
    /// <summary>
    /// In-feed ad format.
    /// </summary>
    InFeed
}
