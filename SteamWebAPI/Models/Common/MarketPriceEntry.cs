using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>A single third-party market's current offer for an item, as embedded in the <c>prices</c> array of an item/inventory payload.</summary>
public sealed class MarketPriceEntry
{
    /// <summary>The market ident, e.g. "skinbaron", "buff".</summary>
    [JsonPropertyName("market")]
    public string Market { get; set; } = string.Empty;

    /// <summary>The market ident, e.g. "skinbaron", "buff". steamwebapi.com's current documentation names this field <c>source</c>.</summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>Per-phase prices keyed by phase name (e.g. "Phase 1"), for items with Doppler-style phases.</summary>
    [JsonPropertyName("variants")]
    public IReadOnlyDictionary<string, double>? Variants { get; set; }

    /// <summary>The lowest current listing price on this market.</summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }

    /// <summary>The number of listings available at or near this price.</summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }
}
