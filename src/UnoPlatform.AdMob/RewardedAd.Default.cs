namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Default implementation of RewardedAd for platforms without native ad support.
    /// </summary>
    public partial class RewardedAd
    {
        partial void InitializePartial()
        {
            // Default implementation - does nothing on unsupported platforms
        }

        partial void LoadPartial(AdRequest request)
        {
            // Default implementation - report error on unsupported platforms
            RaiseAdFailedToLoad(new AdError(
                -1,
                "RewardedAd is not supported on this platform",
                "UnoPlatform.AdMob"));
        }

        partial void ShowPartial()
        {
            // Default implementation - report error on unsupported platforms
            RaiseAdFailedToShow(new AdError(
                -1,
                "RewardedAd is not supported on this platform",
                "UnoPlatform.AdMob"));
        }
    }
}
