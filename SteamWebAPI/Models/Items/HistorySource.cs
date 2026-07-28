using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Items;

/// <summary>The price basis used to aggregate history in <see cref="SteamWebApiClient.GetItemsAggregatedHistoryAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<HistorySource>))]
public enum HistorySource
{
    /// <summary>Steam Community Market's last sale price (default).</summary>
    [EnumMember(Value = "steam")]
    Steam = 0,

    /// <summary>The lowest offer among third-party markets.</summary>
    [EnumMember(Value = "markets")]
    Markets,
}
