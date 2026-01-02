using System;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Event arguments for ad load failures.
    /// </summary>
    public class AdLoadErrorEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the error that occurred.
        /// </summary>
        public AdError Error { get; }

        public AdLoadErrorEventArgs(AdError error)
        {
            Error = error;
        }
    }

    /// <summary>
    /// Event arguments for ad events.
    /// </summary>
    public class AdEventArgs : EventArgs
    {
        public AdEventArgs()
        {
        }
    }

    /// <summary>
    /// Event arguments for impression events.
    /// </summary>
    public class AdImpressionEventArgs : EventArgs
    {
        public AdImpressionEventArgs()
        {
        }
    }

    /// <summary>
    /// Event arguments for reward events.
    /// </summary>
    public class RewardEventArgs : EventArgs
    {
        /// <summary>
        /// Gets the type of reward.
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// Gets the reward amount.
        /// </summary>
        public int Amount { get; }

        public RewardEventArgs(string type, int amount)
        {
            Type = type;
            Amount = amount;
        }
    }
}
