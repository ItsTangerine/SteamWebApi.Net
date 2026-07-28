using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Explore;

/// <summary>
/// The field to sort <see cref="SteamWebApiClient.GetExploreProfilesAsync"/> results by. Combined with
/// <see cref="GetExploreProfilesRequest.OrderByDescending"/> to build the wire <c>order_by</c> parameter
/// (e.g. "worth" + descending = "worthDESC").
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ExploreProfileOrderByField>))]
public enum ExploreProfileOrderByField
{
    /// <summary>Inventory worth. The default sort field.</summary>
    [EnumMember(Value = "worth")]
    Worth = 0,

    /// <summary>Persona/display name, alphabetically.</summary>
    [EnumMember(Value = "personaname")]
    PersonaName,

    /// <summary>Steam account creation time.</summary>
    [EnumMember(Value = "timecreated")]
    TimeCreated,

    /// <summary>Fame flag.</summary>
    [EnumMember(Value = "fame")]
    Fame,

    /// <summary>Inventory item count.</summary>
    [EnumMember(Value = "size")]
    Size,

    /// <summary>When the profile record was last updated.</summary>
    [EnumMember(Value = "updatedat")]
    UpdatedAt,

    /// <summary>When the profile's inventory was last updated.</summary>
    [EnumMember(Value = "inventoryupdatedat")]
    InventoryUpdatedAt,

    /// <summary>Total playtime across all games.</summary>
    [EnumMember(Value = "totalplaytime")]
    TotalPlaytime,

    /// <summary>Random order. Does not support an ascending/descending direction.</summary>
    [EnumMember(Value = "random")]
    Random,
}
