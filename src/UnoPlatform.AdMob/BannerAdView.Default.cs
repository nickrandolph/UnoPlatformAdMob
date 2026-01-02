namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Default implementation of BannerAdView for platforms without native ad support.
    /// </summary>
    public partial class BannerAdView
    {
        partial void OnAdUnitIdChangedPartial()
        {
            // Default implementation - does nothing on unsupported platforms
        }

        partial void OnAdSizeChangedPartial()
        {
            // Default implementation - does nothing on unsupported platforms
        }

        partial void OnLoadedPartial()
        {
            // Default implementation - does nothing on unsupported platforms
        }

        partial void OnUnloadedPartial()
        {
            // Default implementation - does nothing on unsupported platforms
        }

        partial void LoadAdPartial(AdRequest request)
        {
            // Default implementation - report error on unsupported platforms
            RaiseAdFailedToLoad(new AdError(
                -1,
                "AdMob is not supported on this platform",
                "UnoPlatform.AdMob"));
        }
    }
}
