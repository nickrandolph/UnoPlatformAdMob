namespace Uno.AdMob;

/// <summary>
/// Represents a reward item.
/// </summary>
public interface IRewardItem
{
    /// <summary>
    /// Gets the reward amount.
    /// </summary>
    int Amount { get; }

    /// <summary>
    /// Gets the reward type.
    /// </summary>
    string Type { get; }
}

/// <summary>
/// Implementation of reward item.
/// </summary>
public class RewardItem : IRewardItem
{
    /// <inheritdoc/>
    public int Amount { get; set; }

    /// <inheritdoc/>
    public string Type { get; set; } = string.Empty;
}
