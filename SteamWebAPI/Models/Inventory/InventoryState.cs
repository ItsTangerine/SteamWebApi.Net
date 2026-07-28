using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>How fresh the inventory data returned by <see cref="SteamWebApiClient.GetInventoryAsync"/> should be.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<InventoryState>))]
public enum InventoryState
{
    /// <summary>Always fetch live from Steam.</summary>
    [EnumMember(Value = "active")]
    Active = 0,

    /// <summary>Fetch live from Steam, falling back to steamwebapi.com's cache on failure.</summary>
    [EnumMember(Value = "fallback")]
    Fallback,

    /// <summary>Serve only from steamwebapi.com's cache, never hitting Steam directly.</summary>
    [EnumMember(Value = "takedb")]
    TakeDb,
}
