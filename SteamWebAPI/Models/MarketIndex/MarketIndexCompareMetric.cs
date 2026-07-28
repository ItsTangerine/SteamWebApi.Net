using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>The metric used to rank segments on <c>GET /steam/api/market-index/cs2/compare</c>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<MarketIndexCompareMetric>))]
public enum MarketIndexCompareMetric
{
    /// <summary>The blended listing price index. The default metric.</summary>
    [EnumMember(Value = "price_index")]
    PriceIndex = 0,

    /// <summary>Total sale turnover.</summary>
    [EnumMember(Value = "turnover")]
    Turnover,

    /// <summary>Units sold in the last 24 hours.</summary>
    [EnumMember(Value = "sold24h")]
    Sold24h,

    /// <summary>Average listing price.</summary>
    [EnumMember(Value = "avg_price")]
    AvgPrice,

    /// <summary>Total value of active sell listings.</summary>
    [EnumMember(Value = "listing_value")]
    ListingValue,

    /// <summary>Total value of active buy orders.</summary>
    [EnumMember(Value = "order_book_value")]
    OrderBookValue,
}
