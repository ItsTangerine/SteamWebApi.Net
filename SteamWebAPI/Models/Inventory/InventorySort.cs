using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>Sort order for inventory item results.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<InventorySort>))]
public enum InventorySort
{
    /// <summary>Highest Steam Market price first. The default.</summary>
    [EnumMember(Value = "price_max")]
    PriceMax = 0,

    /// <summary>Lowest Steam Market price first.</summary>
    [EnumMember(Value = "price_min")]
    PriceMin,

    /// <summary>Highest third-party market price first.</summary>
    [EnumMember(Value = "price_real_max")]
    PriceRealMax,

    /// <summary>Lowest third-party market price first.</summary>
    [EnumMember(Value = "price_real_min")]
    PriceRealMin,

    /// <summary>Highest mixed (Steam/third-party) price first.</summary>
    [EnumMember(Value = "price_mix_max")]
    PriceMixMax,

    /// <summary>Lowest mixed (Steam/third-party) price first.</summary>
    [EnumMember(Value = "price_mix_min")]
    PriceMixMin,

    /// <summary>Highest stacked item count first (relevant when <c>group=1</c>).</summary>
    [EnumMember(Value = "count")]
    Count,

    /// <summary>Alphabetical by market hash name.</summary>
    [EnumMember(Value = "name")]
    Name,
}
