using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketsPricesAsync"/>.</summary>
public sealed class GetMarketsPricesRequest
{
    /// <summary>Restricts to a single item; recommended for low latency. When omitted, all items are returned.</summary>
    public string? MarketHashName { get; set; }

    /// <summary>
    /// Restricts to these market idents, e.g. "buff", "csfloat". These are open-ended, not a closed set, so plain
    /// strings are used rather than <see cref="Common.Market"/>. Defaults to all active markets.
    /// </summary>
    public IReadOnlyList<string>? Markets { get; set; }

    /// <summary>Converts the returned prices to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }
}
