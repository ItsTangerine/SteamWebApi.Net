using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// A market index metric that can be charted via <c>metric</c>/<c>metrics</c> on
/// <c>GET /steam/api/market-index/cs2/history</c>.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<MarketIndexMetric>))]
public enum MarketIndexMetric
{
    /// <summary>The blended listing price index. The default metric.</summary>
    [EnumMember(Value = "priceindex")]
    PriceIndex = 0,

    /// <summary>The blended buy-order price index.</summary>
    [EnumMember(Value = "buyorderpriceindex")]
    BuyOrderPriceIndex,

    /// <summary>Units sold in the last 24 hours.</summary>
    [EnumMember(Value = "sold24h")]
    Sold24h,

    /// <summary>Units sold in the last 7 days.</summary>
    [EnumMember(Value = "sold7d")]
    Sold7d,

    /// <summary>Units sold in the last 30 days.</summary>
    [EnumMember(Value = "sold30d")]
    Sold30d,

    /// <summary>Total sale turnover in the last 24 hours.</summary>
    [EnumMember(Value = "turnover24h")]
    Turnover24h,

    /// <summary>The number of active sell listings.</summary>
    [EnumMember(Value = "listings")]
    Listings,

    /// <summary>The number of active buy orders.</summary>
    [EnumMember(Value = "buyorders")]
    BuyOrders,

    /// <summary>The average bid/ask spread percentage.</summary>
    [EnumMember(Value = "avgspreadpct")]
    AvgSpreadPct,

    /// <summary>The average sell listing price.</summary>
    [EnumMember(Value = "avglistingprice")]
    AvgListingPrice,

    /// <summary>The percentage of listings that sold within 24 hours.</summary>
    [EnumMember(Value = "sellthrough24hpct")]
    SellThrough24hPct,

    /// <summary>The total value of active sell listings.</summary>
    [EnumMember(Value = "listingvalue")]
    ListingValue,

    /// <summary>The total value of active buy orders.</summary>
    [EnumMember(Value = "orderbookvalue")]
    OrderBookValue,

    /// <summary>The ratio of buy-order volume to sell-listing volume.</summary>
    [EnumMember(Value = "buypressureratio")]
    BuyPressureRatio,

    /// <summary>The number of segments/items whose price is trending up.</summary>
    [EnumMember(Value = "gainerscount")]
    GainersCount,

    /// <summary>The number of segments/items whose price is trending down.</summary>
    [EnumMember(Value = "loserscount")]
    LosersCount,

    /// <summary>The number of segments/items with no meaningful price change.</summary>
    [EnumMember(Value = "neutralcount")]
    NeutralCount,

    /// <summary>The total number of items covered by the index.</summary>
    [EnumMember(Value = "itemcount")]
    ItemCount,
}
