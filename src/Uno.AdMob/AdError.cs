namespace Uno.AdMob;

/// <summary>
/// Interface for ad errors.
/// </summary>
public interface IAdError
{
    /// <summary>
    /// Gets the error code.
    /// </summary>
    int Code { get; }

    /// <summary>
    /// Gets the error message.
    /// </summary>
    string Message { get; }
}

/// <summary>
/// Represents an ad error.
/// </summary>
public class AdError : IAdError
{
    /// <inheritdoc/>
    public int Code { get; set; }

    /// <inheritdoc/>
    public string Message { get; set; } = string.Empty;
}
