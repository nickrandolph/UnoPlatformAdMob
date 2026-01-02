namespace Uno.AdMob.Configuration;

/// <summary>
/// Configuration for AdMob.
/// </summary>
public static class AdConfig
{
    /// <summary>
    /// Default banner ad unit ID.
    /// </summary>
    public static string? DefaultBannerAdUnitId { get; set; }

    /// <summary>
    /// Default interstitial ad unit ID.
    /// </summary>
    public static string? DefaultInterstitialAdUnitId { get; set; }

    /// <summary>
    /// Default rewarded ad unit ID.
    /// </summary>
    public static string? DefaultRewardedAdUnitId { get; set; }

    /// <summary>
    /// Default rewarded interstitial ad unit ID.
    /// </summary>
    public static string? DefaultRewardedInterstitialAdUnitId { get; set; }

    /// <summary>
    /// Default app open ad unit ID.
    /// </summary>
    public static string? DefaultAppOpenAdUnitId { get; set; }

    /// <summary>
    /// Default native ad unit ID.
    /// </summary>
    public static string? DefaultNativeAdUnitId { get; set; }

    /// <summary>
    /// Disable consent check.
    /// </summary>
    public static bool DisableConsentCheck { get; set; }

    /// <summary>
    /// Tag for child-directed treatment.
    /// </summary>
    public static TagForChildDirectedTreatment TagForChildDirectedTreatment { get; set; }

    /// <summary>
    /// Tag for under age of consent.
    /// </summary>
    public static TagForUnderAgeOfConsent TagForUnderAgeOfConsent { get; set; }

    /// <summary>
    /// Maximum ad content rating.
    /// </summary>
    public static MaxAdContentRating MaxAdContentRating { get; set; }
}
