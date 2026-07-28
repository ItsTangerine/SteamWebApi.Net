using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketHistoryAsync"/>.</summary>
public sealed class GetMarketHistoryRequest
{
    /// <summary>The earliest date to include.</summary>
    public DateTime? StartDate { get; set; }

    /// <summary>The latest date to include.</summary>
    public DateTime? EndDate { get; set; }

    /// <summary>Converts the returned prices to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }
}
