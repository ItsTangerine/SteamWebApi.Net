using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>The outcome of a completed trade, as reported by <see cref="TradeHistoryEntry.Status"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<TradeHistoryStatus>))]
public enum TradeHistoryStatus
{
    /// <summary>The trade completed normally.</summary>
    [EnumMember(Value = "traded")]
    Traded = 0,

    /// <summary>The trade was reversed (e.g. reclaimed by Steam Support).</summary>
    [EnumMember(Value = "reversed")]
    Reversed,
}
