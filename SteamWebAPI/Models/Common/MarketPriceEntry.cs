using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>A single third-party market's current offer for an item, as embedded in the <c>prices</c> array of an item/inventory payload.</summary>
public sealed class MarketPriceEntry
{
    /// <summary>The market ident, e.g. "skinbaron", "buff".</summary>
    [JsonPropertyName("market")]
    public string Market { get; set; } = string.Empty;

    /// <summary>The lowest current listing price on this market.</summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }

    /// <summary>The number of listings available at or near this price.</summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
