using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketPricesAsync"/>.</summary>
public sealed class GetMarketPricesRequest
{
    /// <summary>Filters to a single item's price on the market. When omitted, prices for all items are returned.</summary>
    public string? MarketHashName { get; set; }

    /// <summary>Converts the returned price(s) to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }
}
