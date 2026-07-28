namespace SteamWebAPI.Models.Items;

/// <summary>Options for <see cref="SteamWebApiClient.GetItemPriceHistoryAsync"/>.</summary>
public sealed class GetItemPriceHistoryRequest
{
    /// <summary>Where to source price history from. Defaults to <see cref="HistoryOrigin.SteamWebApi"/>.</summary>
    public HistoryOrigin? Origin { get; set; }

    /// <summary>The interval, in days, between returned data points. Defaults to 10.</summary>
    public int? IntervalDays { get; set; }

    /// <summary>The earliest date to include. Only the date component is used.</summary>
    public DateTime? StartDate { get; set; }

    /// <summary>The latest date to include. Only the date component is used.</summary>
    public DateTime? EndDate { get; set; }
}
