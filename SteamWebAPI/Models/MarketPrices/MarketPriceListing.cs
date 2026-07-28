using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>
/// A single item's current price on one named third-party market, as returned by
/// <see cref="SteamWebApiClient.GetMarketPricesAsync"/>.
/// </summary>
/// <remarks>
/// steamwebapi.com's documentation does not publish an example or schema for this endpoint. This shape is inferred
/// by analogy with the per-market entries returned by <c>GET /markets/prices</c> (see <see cref="ItemMarketPrices"/>),
/// with <see cref="MarketHashName"/> added since this endpoint's response is not keyed by item. When the
/// <c>market_hash_name</c> filter is applied, the live API may return a single object instead of a single-element
/// array; this client always deserializes the payload as a list. Verify this shape against a live call before
/// relying on it.
/// </remarks>
public sealed class MarketPriceListing
{
    /// <summary>The item's Steam Market hash name.</summary>
    [JsonPropertyName("market_hash_name")]
    public string? MarketHashName { get; set; }

    /// <summary>The item's current price on this market.</summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    /// <summary>The number of listings available at or near this price.</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>When this price was recorded.</summary>
    [JsonPropertyName("createdat")]
    public DateTimeOffset? CreatedAt { get; set; }
}
