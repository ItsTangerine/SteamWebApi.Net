using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.MarketIndex;

/// <summary>
/// The aggregation interval for <c>GET /steam/api/market-index/cs2/history</c>. Aggregated intervals return OHLC
/// (open/high/low/close) points; <see cref="Raw"/> presumably returns plain value points, though this is not
/// confirmed by the source documentation, whose examples only show daily/hourly OHLC output.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<MarketIndexInterval>))]
public enum MarketIndexInterval
{
    /// <summary>Unaggregated raw data points. The default.</summary>
    [EnumMember(Value = "raw")]
    Raw = 0,

    /// <summary>5-minute OHLC buckets.</summary>
    [EnumMember(Value = "fivemin")]
    FiveMinutes,

    /// <summary>10-minute OHLC buckets.</summary>
    [EnumMember(Value = "tenmin")]
    TenMinutes,

    /// <summary>1-hour OHLC buckets.</summary>
    [EnumMember(Value = "hourly")]
    Hourly,

    /// <summary>6-hour OHLC buckets.</summary>
    [EnumMember(Value = "sixhours")]
    SixHours,

    /// <summary>1-day OHLC buckets.</summary>
    [EnumMember(Value = "daily")]
    Daily,

    /// <summary>3-day OHLC buckets.</summary>
    [EnumMember(Value = "threedays")]
    ThreeDays,

    /// <summary>1-week OHLC buckets.</summary>
    [EnumMember(Value = "weekly")]
    Weekly,

    /// <summary>1-month OHLC buckets.</summary>
    [EnumMember(Value = "monthly")]
    Monthly,

    /// <summary>3-month OHLC buckets.</summary>
    [EnumMember(Value = "threemonths")]
    ThreeMonths,

    /// <summary>6-month OHLC buckets.</summary>
    [EnumMember(Value = "sixmonths")]
    SixMonths,

    /// <summary>1-year OHLC buckets.</summary>
    [EnumMember(Value = "yearly")]
    Yearly,
}
