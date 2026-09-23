using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Apps;

/// <summary>
/// Sort key for <see cref="SteamWebApiClient.GetAppsAsync"/> and <see cref="SteamWebApiClient.GetAppMetricsAsync"/>.
/// Games without a value for the chosen key are left out, except for <see cref="AppId"/>.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<AppSortBy>))]
public enum AppSortBy
{
    /// <summary>By app id. The default, and the only key that includes every game.</summary>
    [EnumMember(Value = "appid")]
    AppId = 0,

    /// <summary>By name.</summary>
    [EnumMember(Value = "name")]
    Name,

    /// <summary>By current players.</summary>
    [EnumMember(Value = "players")]
    Players,

    /// <summary>By 30-day peak players.</summary>
    [EnumMember(Value = "peak30d")]
    Peak30d,

    /// <summary>By estimated player-hours over 30 days.</summary>
    [EnumMember(Value = "hoursplayed30d")]
    HoursPlayed30d,

    /// <summary>By all-time peak players.</summary>
    [EnumMember(Value = "peakalltime")]
    PeakAllTime,

    /// <summary>By 24-hour player change (trending).</summary>
    [EnumMember(Value = "change24h")]
    Change24h,

    /// <summary>By review count.</summary>
    [EnumMember(Value = "reviews")]
    Reviews,

    /// <summary>By review count over the last 30 days.</summary>
    [EnumMember(Value = "reviews30d")]
    Reviews30d,

    /// <summary>By rating (Wilson score lower bound).</summary>
    [EnumMember(Value = "rating")]
    Rating,

    /// <summary>By 30-day rating (Wilson score lower bound).</summary>
    [EnumMember(Value = "rating30d")]
    Rating30d,

    /// <summary>By price in the storefront of <see cref="GetAppsRequest.Currency"/>.</summary>
    [EnumMember(Value = "price")]
    Price,

    /// <summary>By discount in the storefront of <see cref="GetAppsRequest.Currency"/>.</summary>
    [EnumMember(Value = "discount")]
    Discount,

    /// <summary>By release date.</summary>
    [EnumMember(Value = "releasedate")]
    ReleaseDate,

    /// <summary>By last catalog update.</summary>
    [EnumMember(Value = "updatedat")]
    UpdatedAt,

    /// <summary>By last player-metrics update.</summary>
    [EnumMember(Value = "metricsupdatedat")]
    MetricsUpdatedAt,
}
