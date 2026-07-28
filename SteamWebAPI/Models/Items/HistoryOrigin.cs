using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Items;

/// <summary>The data source for <see cref="SteamWebApiClient.GetItemPriceHistoryAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<HistoryOrigin>))]
public enum HistoryOrigin
{
    /// <summary>steamwebapi.com's own aggregated Steam price history (default).</summary>
    [EnumMember(Value = "steamwebapi")]
    SteamWebApi = 0,

    /// <summary>Real third-party market price history. Slower, but reflects actual market data.</summary>
    [EnumMember(Value = "markets")]
    Markets,

    /// <summary>Direct passthrough to the Steam API.</summary>
    [EnumMember(Value = "direct")]
    Direct,
}
