using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// The CS2 global market index, or a single segment's stats, as returned by <see cref="SteamWebApiClient.GetMarketIndexAsync"/>.
/// </summary>
public sealed class MarketIndexResult
{
    /// <summary>Whether the request succeeded.</summary>
    [JsonPropertyName("success")]
    public bool Success { get; set; }

    /// <summary>Either "global" (whole-market overview) or "segment" (a single segment's stats).</summary>
    [JsonPropertyName("mode")]
    public string? Mode { get; set; }

    /// <summary>The Steam app id the index covers (730 for CS2).</summary>
    [JsonPropertyName("gameid")]
    public int GameId { get; set; }

    /// <summary>The game name.</summary>
    [JsonPropertyName("gamename")]
    public string? GameName { get; set; }

    /// <summary>When the index was last refreshed, formatted as <c>yyyy-MM-dd HH:mm:ss</c>.</summary>
    [JsonPropertyName("lastupdated")]
    public string? LastUpdated { get; set; }

    /// <summary>When the index was last refreshed, as a Unix timestamp in seconds.</summary>
    [JsonPropertyName("timestamp")]
    public long Timestamp { get; set; }

    /// <summary>The total number of items covered by the index.</summary>
    [JsonPropertyName("itemcount")]
    public int? ItemCount { get; set; }

    /// <summary>The number of covered items that are currently marketable.</summary>
    [JsonPropertyName("marketableitemcount")]
    public int? MarketableItemCount { get; set; }

    /// <summary>The number of covered items whose pricing data is considered unreliable.</summary>
    [JsonPropertyName("unstablecount")]
    public int? UnstableCount { get; set; }

    /// <summary>The blended listing price index.</summary>
    [JsonPropertyName("priceindex")]
    public decimal? PriceIndex { get; set; }

    /// <summary>The blended buy-order price index.</summary>
    [JsonPropertyName("buyorderpriceindex")]
    public decimal? BuyOrderPriceIndex { get; set; }

    /// <summary>The blended real (third-party market) price index.</summary>
    [JsonPropertyName("realpriceindex")]
    public decimal? RealPriceIndex { get; set; }

    /// <summary>The total value of active sell listings, in unit count terms.</summary>
    [JsonPropertyName("offervolumetotal")]
    public long? OfferVolumeTotal { get; set; }

    /// <summary>The total value of active buy orders, in unit count terms.</summary>
    [JsonPropertyName("buyordervolumetotal")]
    public long? BuyOrderVolumeTotal { get; set; }

    /// <summary>Units sold in the last 24 hours.</summary>
    [JsonPropertyName("sold24h")]
    public long? Sold24h { get; set; }

    /// <summary>Units sold in the last 7 days.</summary>
    [JsonPropertyName("sold7d")]
    public long? Sold7d { get; set; }

    /// <summary>Units sold in the last 30 days.</summary>
    [JsonPropertyName("sold30d")]
    public long? Sold30d { get; set; }

    /// <summary>Total sale turnover in the last 24 hours.</summary>
    [JsonPropertyName("turnover24h")]
    public decimal? Turnover24h { get; set; }

    /// <summary>The average bid/ask spread percentage.</summary>
    [JsonPropertyName("avgspreadpct")]
    public decimal? AvgSpreadPct { get; set; }

    /// <summary>The ratio of buy-order volume to sell-listing volume.</summary>
    [JsonPropertyName("buypressureratio")]
    public decimal? BuyPressureRatio { get; set; }

    /// <summary>The percentage of listings that sold within 24 hours.</summary>
    [JsonPropertyName("sellthrough24hpct")]
    public decimal? SellThrough24hPct { get; set; }

    /// <summary>The average sell listing price.</summary>
    [JsonPropertyName("avglistingprice")]
    public decimal? AvgListingPrice { get; set; }

    /// <summary>The average active buy-order price.</summary>
    [JsonPropertyName("avgbuyorderprice")]
    public decimal? AvgBuyOrderPrice { get; set; }

    /// <summary>The average third-party market price.</summary>
    [JsonPropertyName("avgrealprice")]
    public decimal? AvgRealPrice { get; set; }

    /// <summary>The ratio of Steam Market prices to third-party market prices.</summary>
    [JsonPropertyName("steammarkupratio")]
    public decimal? SteamMarkupRatio { get; set; }

    /// <summary>The total value of all active sell listings.</summary>
    [JsonPropertyName("listingtotalvalue")]
    public decimal? ListingTotalValue { get; set; }

    /// <summary>The total value of all active buy orders.</summary>
    [JsonPropertyName("buyordertotalvalue")]
    public decimal? BuyOrderTotalValue { get; set; }

    /// <summary>A breakdown of how many covered items/segments are gaining, losing, or holding value.</summary>
    [JsonPropertyName("marketsentiment")]
    public MarketSentiment? MarketSentiment { get; set; }

    /// <summary>The share of covered items currently priced at (or near) zero.</summary>
    [JsonPropertyName("zeropriceshare")]
    public decimal? ZeroPriceShare { get; set; }

    /// <summary>
    /// Period-over-period metric changes, keyed by period ("24h", "7d", "30d", "90d"). The shape is inconsistent
    /// across periods in steamwebapi.com's own example: "24h" is an object mapping metric name to
    /// <c>{ value, previous, change, trend }</c>, while "7d"/"30d"/"90d" were empty arrays. There is no fixed
    /// schema to model, so this is exposed as a raw <see cref="JsonElement"/>.
    /// </summary>
    [JsonPropertyName("changes")]
    public JsonElement Changes { get; set; }

    /// <summary>The biggest 24-hour gainers and losers among covered items.</summary>
    [JsonPropertyName("topmovers")]
    public TopMovers? TopMovers { get; set; }

    /// <summary>
    /// Historical index snapshots. Empty in steamwebapi.com's own example and the shape is otherwise unconfirmed,
    /// so this is exposed as a raw <see cref="JsonElement"/>.
    /// </summary>
    [JsonPropertyName("history")]
    public JsonElement History { get; set; }

    /// <summary>
    /// Per-segment breakdowns, populated when <see cref="Mode"/> is "segment". Empty in steamwebapi.com's own
    /// example and the shape is otherwise unconfirmed, so this is exposed as a raw <see cref="JsonElement"/>.
    /// </summary>
    [JsonPropertyName("segments")]
    public JsonElement Segments { get; set; }

    /// <summary>
    /// The valid segment keys for each <see cref="MarketIndexSegmentType"/>, populated only for the global-overview
    /// call per steamwebapi.com's docs. Empty in steamwebapi.com's own example and the shape is otherwise
    /// unconfirmed (possibly an object keyed by segment type), so this is exposed as a raw <see cref="JsonElement"/>.
    /// </summary>
    [JsonPropertyName("available_segments")]
    public JsonElement AvailableSegments { get; set; }
}

/// <summary>A breakdown of how many covered items/segments are gaining, losing, or holding value, embedded in a <see cref="MarketIndexResult"/>.</summary>
public sealed class MarketSentiment
{
    /// <summary>The number of items/segments trending up.</summary>
    [JsonPropertyName("gainers")]
    public int? Gainers { get; set; }

    /// <summary>The number of items/segments trending down.</summary>
    [JsonPropertyName("losers")]
    public int? Losers { get; set; }

    /// <summary>The number of items/segments with no meaningful price change.</summary>
    [JsonPropertyName("neutral")]
    public int? Neutral { get; set; }
}

/// <summary>The biggest 24-hour gainers and losers among covered items, embedded in a <see cref="MarketIndexResult"/>.</summary>
public sealed class TopMovers
{
    /// <summary>The items with the largest 24-hour price increases.</summary>
    [JsonPropertyName("gainers")]
    public IReadOnlyList<MoverItem>? Gainers { get; set; }

    /// <summary>The items with the largest 24-hour price decreases.</summary>
    [JsonPropertyName("losers")]
    public IReadOnlyList<MoverItem>? Losers { get; set; }
}

/// <summary>A single item's 24-hour price movement, embedded in a <see cref="TopMovers"/> list.</summary>
public sealed class MoverItem
{
    /// <summary>The item's Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The 24-hour price change.</summary>
    [JsonPropertyName("change24h")]
    public decimal? Change24h { get; set; }

    /// <summary>The item's current price.</summary>
    [JsonPropertyName("price")]
    public decimal? Price { get; set; }
}
