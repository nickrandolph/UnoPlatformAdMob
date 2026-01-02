using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Controls.Primitives;
using Microsoft.UI.Xaml.Data;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;
using Microsoft.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace UnoPlatform.AdMob.Sample
{
    /// <summary>
    /// Sample page demonstrating AdMob controls.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private InterstitialAd _interstitialAd;
        private RewardedAd _rewardedAd;

        public MainPage()
        {
            this.InitializeComponent();
            this.Loaded += MainPage_Loaded;
        }

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            // Initialize AdMob SDK
            await MobileAds.Initialize();
        }

        #region Banner Ad

        private void LoadBannerAd_Click(object sender, RoutedEventArgs e)
        {
            var request = new AdRequest();
            BannerAd.LoadAd(request);
            BannerStatusText.Text = "Banner status: Loading...";
        }

        private void BannerAd_AdLoaded(object sender, AdEventArgs e)
        {
            BannerStatusText.Text = "Banner status: Loaded successfully ✓";
        }

        private void BannerAd_AdFailedToLoad(object sender, AdLoadErrorEventArgs e)
        {
            BannerStatusText.Text = $"Banner status: Failed - {e.Error.Message}";
        }

        #endregion

        #region Interstitial Ad

        private void LoadInterstitial_Click(object sender, RoutedEventArgs e)
        {
            // Test Ad Unit ID for Interstitial
            _interstitialAd = new InterstitialAd("ca-app-pub-3940256099942544/1033173712");
            
            _interstitialAd.AdLoaded += (s, args) =>
            {
                InterstitialStatusText.Text = "Interstitial status: Loaded successfully ✓";
                ShowInterstitialButton.IsEnabled = true;
            };

            _interstitialAd.AdFailedToLoad += (s, args) =>
            {
                InterstitialStatusText.Text = $"Interstitial status: Failed - {args.Error.Message}";
                ShowInterstitialButton.IsEnabled = false;
            };

            _interstitialAd.AdDismissed += (s, args) =>
            {
                InterstitialStatusText.Text = "Interstitial status: Dismissed";
                ShowInterstitialButton.IsEnabled = false;
            };

            var request = new AdRequest();
            _interstitialAd.Load(request);
            InterstitialStatusText.Text = "Interstitial status: Loading...";
        }

        private void ShowInterstitial_Click(object sender, RoutedEventArgs e)
        {
            _interstitialAd?.Show();
            InterstitialStatusText.Text = "Interstitial status: Showing...";
        }

        #endregion

        #region Rewarded Ad

        private void LoadRewarded_Click(object sender, RoutedEventArgs e)
        {
            // Test Ad Unit ID for Rewarded Video
            _rewardedAd = new RewardedAd("ca-app-pub-3940256099942544/5224354917");
            
            _rewardedAd.AdLoaded += (s, args) =>
            {
                RewardedStatusText.Text = "Rewarded status: Loaded successfully ✓";
                ShowRewardedButton.IsEnabled = true;
            };

            _rewardedAd.AdFailedToLoad += (s, args) =>
            {
                RewardedStatusText.Text = $"Rewarded status: Failed - {args.Error.Message}";
                ShowRewardedButton.IsEnabled = false;
            };

            _rewardedAd.UserEarnedReward += (s, args) =>
            {
                RewardedStatusText.Text = $"Rewarded status: Earned {args.Amount} {args.Type} 🎉";
            };

            _rewardedAd.AdDismissed += (s, args) =>
            {
                RewardedStatusText.Text = "Rewarded status: Dismissed";
                ShowRewardedButton.IsEnabled = false;
            };

            var request = new AdRequest();
            _rewardedAd.Load(request);
            RewardedStatusText.Text = "Rewarded status: Loading...";
        }

        private void ShowRewarded_Click(object sender, RoutedEventArgs e)
        {
            _rewardedAd?.Show();
            RewardedStatusText.Text = "Rewarded status: Showing...";
        }

        #endregion
    }
}
