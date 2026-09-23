using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>
/// Whether <see cref="SteamWebApiClient.GetInventoryAsync"/> reads the CS2 trading inventory to also return
/// trade-locked items. Sent as <c>try_first_seven_days_blocked_items</c>.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<TradingInventoryMode>))]
public enum TradingInventoryMode
{
    /// <summary>Normal inventory only. The default.</summary>
    [EnumMember(Value = "0")]
    Disabled = 0,

    /// <summary>Try the trading inventory first, falling back to the normal inventory when it yields nothing.</summary>
    [EnumMember(Value = "1")]
    PreferTradingInventory,

    /// <summary>Trading inventory only, without the normal-inventory fallback.</summary>
    [EnumMember(Value = "2")]
    TradingInventoryOnly,
}
