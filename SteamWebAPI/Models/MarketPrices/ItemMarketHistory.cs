using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketPrices;

/// <summary>
/// One item's daily price history across all configured third-party markets, as returned by
/// <see cref="SteamWebApiClient.GetMarketsHistoryAsync"/>. Unlike the Market Index endpoints, steamwebapi.com's
/// example for this endpoint has no top-level <c>success</c> field.
/// </summary>
public sealed class ItemMarketHistory : BaseResponseDto
{
    /// <summary>The item's Steam Market hash name.</summary>
    [JsonPropertyName("market_hash_name")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The item's daily price history on each market, keyed by market ident (e.g. "buff", "csfloat", "youpin").</summary>
    [JsonPropertyName("history")]
    public IReadOnlyDictionary<string, IReadOnlyList<MarketHistoryPoint>>? History { get; set; }
}
