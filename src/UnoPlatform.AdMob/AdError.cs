namespace UnoPlatform.AdMob
{
    /// <summary>
    /// Represents an error that occurred during ad loading or display.
    /// </summary>
    public class AdError
    {
        /// <summary>
        /// Gets the error code.
        /// </summary>
        public int Code { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the error domain.
        /// </summary>
        public string Domain { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdError"/> class.
        /// </summary>
        public AdError(int code, string message, string domain = "AdMob")
        {
            Code = code;
            Message = message;
            Domain = domain;
        }

        public override string ToString() => $"AdError({Code}): {Message}";
    }
}
