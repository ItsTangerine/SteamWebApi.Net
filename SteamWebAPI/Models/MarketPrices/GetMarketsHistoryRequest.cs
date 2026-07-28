using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketsHistoryAsync"/>.</summary>
public sealed class GetMarketsHistoryRequest
{
    /// <summary>
    /// Restricts to these market idents, e.g. "buff", "csfloat". These are open-ended, not a closed set, so plain
    /// strings are used rather than <see cref="Common.Market"/>. Defaults to all active markets.
    /// </summary>
    public IReadOnlyList<string>? Markets { get; set; }

    /// <summary>The earliest date to include.</summary>
    public DateTime? StartDate { get; set; }

    /// <summary>The latest date to include.</summary>
    public DateTime? EndDate { get; set; }

    /// <summary>Converts the returned prices to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }
}
