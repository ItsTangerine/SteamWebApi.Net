using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>
/// The game to fetch inventories for via <see cref="SteamWebApiClient.GetInventoryBatchAsync"/>. This set is
/// closed for the batch endpoint, unlike the single-inventory endpoint's free-text <c>game</c> parameter.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<InventoryGame>))]
public enum InventoryGame
{
    /// <summary>Counter-Strike 2. The default.</summary>
    [EnumMember(Value = "cs2")]
    Cs2 = 0,

    /// <summary>Dota 2.</summary>
    [EnumMember(Value = "dota2")]
    Dota2,

    /// <summary>Rust.</summary>
    [EnumMember(Value = "rust")]
    Rust,

    /// <summary>Team Fortress 2.</summary>
    [EnumMember(Value = "tf2")]
    Tf2,

    /// <summary>The Steam community inventory (non-game items).</summary>
    [EnumMember(Value = "steam")]
    Steam,
}
