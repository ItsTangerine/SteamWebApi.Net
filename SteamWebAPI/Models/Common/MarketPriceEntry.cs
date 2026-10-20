using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>A single third-party market's current offer for an item, as embedded in the <c>prices</c> array of an item/inventory payload.</summary>
public sealed class MarketPriceEntry : BaseResponseDto
{
    /// <summary>The market ident, e.g. "skinbaron", "buff".</summary>
    [Obsolete("steamwebapi.com no longer returns 'market'; the ident is in Source.")]
    [JsonPropertyName("market")]
    public string Market { get; set; } = string.Empty;

    /// <summary>The market ident, e.g. "skinbaron", "buff".</summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>The market's display name, e.g. "CSFloat".</summary>
    [JsonPropertyName("name")]
    public string? Name { get; set; }

    /// <summary>The market's logo image URL.</summary>
    [JsonPropertyName("logo")]
    public string? Logo { get; set; }

    /// <summary>The kind of price observation, e.g. "offer". Free text; not a closed set.</summary>
    [JsonPropertyName("type")]
    public string? Type { get; set; }

    /// <summary>A link to this item's listings on the market.</summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>When this price was recorded (UTC).</summary>
    [JsonPropertyName("created_at")]
    [JsonConverter(typeof(SteamDateWrapperConverter))]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>Whether the market is peer-to-peer. Sent by <c>GET /steam/api/item</c>.</summary>
    [JsonPropertyName("isp2p")]
    public bool? IsP2P { get; set; }

    /// <summary>Whether steamwebapi.com marks the market as trusted. Sent by <c>GET /steam/api/item</c>.</summary>
    [JsonPropertyName("istrusted")]
    public bool? IsTrusted { get; set; }

    /// <summary>The market's review score (e.g. 4.9), when known. Sent by <c>GET /steam/api/item</c>.</summary>
    [JsonPropertyName("trustscore")]
    public double? TrustScore { get; set; }

    /// <summary>The number of reviews behind <see cref="TrustScore"/>, when known. Sent by <c>GET /steam/api/item</c>.</summary>
    [JsonPropertyName("trustscorecount")]
    public int? TrustScoreCount { get; set; }

    /// <summary>A usage/popularity counter for the market. Sent by <c>GET /steam/api/item</c>.</summary>
    [JsonPropertyName("usagecount")]
    public long? UsageCount { get; set; }

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
