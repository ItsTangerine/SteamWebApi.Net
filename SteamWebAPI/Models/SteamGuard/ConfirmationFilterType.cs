using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>Which category of pending mobile confirmations to act on.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ConfirmationFilterType>))]
public enum ConfirmationFilterType
{
    /// <summary>Only trade offer confirmations.</summary>
    [EnumMember(Value = "trade")]
    Trade = 0,

    /// <summary>Only Steam Market listing confirmations.</summary>
    [EnumMember(Value = "market")]
    Market,

    /// <summary>All pending confirmations, regardless of type.</summary>
    [EnumMember(Value = "all")]
    All,
}
