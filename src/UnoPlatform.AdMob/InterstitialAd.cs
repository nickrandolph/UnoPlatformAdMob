using System;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Represents an interstitial ad that displays full-screen content.
    /// </summary>
    public partial class InterstitialAd
    {
        private string _adUnitId;

        /// <summary>
        /// Gets the Ad Unit ID for this interstitial ad.
        /// </summary>
        public string AdUnitId => _adUnitId;

        /// <summary>
        /// Occurs when an ad is successfully loaded.
        /// </summary>
        public event EventHandler<AdEventArgs> AdLoaded;

        /// <summary>
        /// Occurs when an ad fails to load.
        /// </summary>
        public event EventHandler<AdLoadErrorEventArgs> AdFailedToLoad;

        /// <summary>
        /// Occurs when an ad is shown.
        /// </summary>
        public event EventHandler<AdEventArgs> AdShown;

        /// <summary>
        /// Occurs when an ad fails to show.
        /// </summary>
        public event EventHandler<AdLoadErrorEventArgs> AdFailedToShow;

        /// <summary>
        /// Occurs when an ad is dismissed.
        /// </summary>
        public event EventHandler<AdEventArgs> AdDismissed;

        /// <summary>
        /// Occurs when an ad impression is recorded.
        /// </summary>
        public event EventHandler<AdImpressionEventArgs> AdImpression;

        /// <summary>
        /// Initializes a new instance of the <see cref="InterstitialAd"/> class.
        /// </summary>
        /// <param name="adUnitId">The Ad Unit ID.</param>
        public InterstitialAd(string adUnitId)
        {
            _adUnitId = adUnitId;
            InitializePartial();
        }

        /// <summary>
        /// Loads an interstitial ad.
        /// </summary>
        /// <param name="request">The ad request.</param>
        public void Load(AdRequest request)
        {
            LoadPartial(request);
        }

        /// <summary>
        /// Shows the interstitial ad.
        /// </summary>
        public void Show()
        {
            ShowPartial();
        }

        partial void InitializePartial();
        partial void LoadPartial(AdRequest request);
        partial void ShowPartial();

        protected void RaiseAdLoaded() => AdLoaded?.Invoke(this, new AdEventArgs());
        protected void RaiseAdFailedToLoad(AdError error) => AdFailedToLoad?.Invoke(this, new AdLoadErrorEventArgs(error));
        protected void RaiseAdShown() => AdShown?.Invoke(this, new AdEventArgs());
        protected void RaiseAdFailedToShow(AdError error) => AdFailedToShow?.Invoke(this, new AdLoadErrorEventArgs(error));
        protected void RaiseAdDismissed() => AdDismissed?.Invoke(this, new AdEventArgs());
        protected void RaiseAdImpression() => AdImpression?.Invoke(this, new AdImpressionEventArgs());
    }
}
