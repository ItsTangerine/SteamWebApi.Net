using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Items;

/// <summary>
/// The game shortname accepted by <see cref="SteamWebApiClient.GetItemsAggregatedHistoryAsync"/>. This endpoint's
/// closed set of game identifiers differs from <see cref="Common.Game"/> (it uses "csgo" instead of a
/// CS2-equivalent value being distinct, and has no TF2 option).
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ItemsHistoryGame>))]
public enum ItemsHistoryGame
{
    /// <summary>Counter-Strike 2 (default).</summary>
    [EnumMember(Value = "cs2")]
    Cs2 = 0,

    /// <summary>Counter-Strike: Global Offensive / CS2 legacy alias.</summary>
    [EnumMember(Value = "csgo")]
    Csgo,

    /// <summary>Dota 2.</summary>
    [EnumMember(Value = "dota")]
    Dota,

    /// <summary>Rust.</summary>
    [EnumMember(Value = "rust")]
    Rust,
}
