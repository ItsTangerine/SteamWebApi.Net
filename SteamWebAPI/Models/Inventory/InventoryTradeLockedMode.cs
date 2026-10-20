using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>
/// Which CS2 inventory contexts to read: context 2 (the normal public inventory) and/or context 16 (which can expose
/// recently received, trade-protected skins not yet visible in the public inventory). Sent as <c>trade_locked</c>.
/// </summary>
/// <remarks>
/// Non-zero modes on another account's inventory require CS2 and <see cref="InventoryState.Active"/>. With
/// <see cref="GetInventoryRequest.SteamLoginSecure"/> set, only <see cref="ContextTwoOnly"/> and
/// <see cref="ContextSixteenOnly"/> are supported, without the partner surcharge. Context 16 reconstruction needs the
/// first page (no <see cref="GetInventoryRequest.StartAssetId"/>), and does not detect newly received cases.
/// </remarks>
[JsonConverter(typeof(JsonStringEnumMemberConverter<InventoryTradeLockedMode>))]
public enum InventoryTradeLockedMode
{
    /// <summary>Context 2 only. The default.</summary>
    [EnumMember(Value = "0")]
    ContextTwoOnly = 0,

    /// <summary>Context 16 only. Never falls back to the context-2 inventory. Costs +1 credit for another account's inventory.</summary>
    [EnumMember(Value = "1")]
    ContextSixteenOnly = 1,

    /// <summary>
    /// Starts both contexts and returns as soon as context 2 is usable, including context 16 only if it is already
    /// available. Costs +1 credit. Not supported together with <see cref="GetInventoryRequest.SteamLoginSecure"/>.
    /// </summary>
    [EnumMember(Value = "3")]
    BothFastReturn = 3,

    /// <summary>
    /// Requires both contexts within the request budget, retrying only the missing one; may fall back to the last
    /// successful trade-protected snapshot. Never falls back to the context-2 inventory alone. Costs +2 credits. Not
    /// supported together with <see cref="GetInventoryRequest.SteamLoginSecure"/>.
    /// </summary>
    [EnumMember(Value = "4")]
    BothRequired = 4,
}
