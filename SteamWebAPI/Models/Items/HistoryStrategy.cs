using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Items;

/// <summary>How to pick a price for a given day when it doesn't have an exact recorded data point.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<HistoryStrategy>))]
public enum HistoryStrategy
{
    /// <summary>Use the last known price before or on the given date.</summary>
    [EnumMember(Value = "PAST_PRICE")]
    PastPrice = 0,

    /// <summary>Use the last known price before the date, or the first known price after it if none exists before.</summary>
    [EnumMember(Value = "PAST_FUTURE_PRICE")]
    PastFuturePrice,

    /// <summary>Use the price on the given date, or the nearest available price otherwise.</summary>
    [EnumMember(Value = "SAME_DATE")]
    SameDate,

    /// <summary>Only use a price that exists on the exact date; otherwise return no data for that day.</summary>
    [EnumMember(Value = "STRICT")]
    Strict,

    /// <summary>Use whichever available price (previous or next) is closest to the given date.</summary>
    [EnumMember(Value = "NEAREST")]
    Nearest,
}
