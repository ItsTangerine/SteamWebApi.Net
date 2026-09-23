using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Profile;

/// <summary>Which handovers <see cref="SteamWebApiClient.GetProfileTradesAsync"/> returns.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ProfileTradeDirection>))]
public enum ProfileTradeDirection
{
    /// <summary>Both directions. The default.</summary>
    [EnumMember(Value = "all")]
    All = 0,

    /// <summary>Items the account received.</summary>
    [EnumMember(Value = "in")]
    In,

    /// <summary>Items the account gave away.</summary>
    [EnumMember(Value = "out")]
    Out,
}
