using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>
/// A single daily price observation on one market, embedded in an <see cref="ItemMarketHistory"/>'s
/// <see cref="ItemMarketHistory.History"/> dictionary, and also used directly as the (inferred) response element
/// type for <see cref="SteamWebApiClient.GetMarketHistoryAsync"/>.
/// </summary>
public sealed class MarketHistoryPoint
{
    /// <summary>When this price was recorded.</summary>
    [JsonPropertyName("createdat")]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>The recorded price.</summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    /// <summary>The number of listings available at or near this price.</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }
}
