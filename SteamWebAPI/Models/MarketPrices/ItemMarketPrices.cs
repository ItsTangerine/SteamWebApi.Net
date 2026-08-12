using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>
/// One item's current prices across all configured third-party markets, as returned by
/// <see cref="SteamWebApiClient.GetMarketsPricesAsync"/>.
/// </summary>
public sealed class ItemMarketPrices : BaseResponseDto
{
    /// <summary>The item's Steam Market hash name.</summary>
    [JsonPropertyName("market_hash_name")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>
    /// The item's price on each market that lists it, keyed by market ident (e.g. "buff", "csfloat", "youpin").
    /// Markets that do not currently list the item are omitted.
    /// </summary>
    [JsonPropertyName("prices")]
    public IReadOnlyDictionary<string, MarketPriceQuote>? Prices { get; set; }
}

/// <summary>A single market's current price quote for an item, embedded in an <see cref="ItemMarketPrices"/>'s <see cref="ItemMarketPrices.Prices"/> dictionary.</summary>
public sealed class MarketPriceQuote : BaseResponseDto
{
    /// <summary>The current price on this market.</summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }

    /// <summary>The number of listings available at or near this price.</summary>
    [JsonPropertyName("quantity")]
    public int? Quantity { get; set; }

    /// <summary>When this price was recorded.</summary>
    [JsonPropertyName("createdat")]
    public DateTimeOffset? CreatedAt { get; set; }
}
