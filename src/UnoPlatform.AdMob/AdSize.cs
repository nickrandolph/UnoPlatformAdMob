using System;

namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Represents the size of an ad banner.
    /// </summary>
    public class AdSize
    {
        /// <summary>
        /// Gets the width of the ad in density-independent pixels.
        /// </summary>
        public int Width { get; }

        /// <summary>
        /// Gets the height of the ad in density-independent pixels.
        /// </summary>
        public int Height { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdSize"/> class.
        /// </summary>
        /// <param name="width">The width of the ad.</param>
        /// <param name="height">The height of the ad.</param>
        public AdSize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Standard banner ad size (320x50).
        /// </summary>
        public static AdSize Banner => new AdSize(320, 50);

        /// <summary>
        /// Large banner ad size (320x100).
        /// </summary>
        public static AdSize LargeBanner => new AdSize(320, 100);

        /// <summary>
        /// Medium rectangle ad size (300x250).
        /// </summary>
        public static AdSize MediumRectangle => new AdSize(300, 250);

        /// <summary>
        /// Full banner ad size (468x60).
        /// </summary>
        public static AdSize FullBanner => new AdSize(468, 60);

        /// <summary>
        /// Leaderboard ad size (728x90).
        /// </summary>
        public static AdSize Leaderboard => new AdSize(728, 90);

        /// <summary>
        /// Smart banner ad size that adapts to the screen width.
        /// </summary>
        public static AdSize SmartBanner => new AdSize(-1, -2);

        public override string ToString() => $"AdSize({Width}x{Height})";
    }
}
