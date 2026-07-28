using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Items;

/// <summary>Sort order for <see cref="SteamWebApiClient.GetItemsAsync"/> results.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ItemsSortBy>))]
public enum ItemsSortBy
{
    /// <summary>Steam listing price ascending (cheapest first). Fastest option; the default.</summary>
    [EnumMember(Value = "priceAz")]
    PriceAscending = 0,

    /// <summary>Steam listing price descending (most expensive first).</summary>
    [EnumMember(Value = "priceZa")]
    PriceDescending,

    /// <summary>Third-party market price ascending.</summary>
    [EnumMember(Value = "priceRealAz")]
    RealPriceAscending,

    /// <summary>Third-party market price descending.</summary>
    [EnumMember(Value = "priceRealZa")]
    RealPriceDescending,

    /// <summary>Best deals first (Steam cheaper than third-party markets).</summary>
    [EnumMember(Value = "winner")]
    BestDealsFirst,

    /// <summary>Worst deals first (Steam more expensive than third-party markets).</summary>
    [EnumMember(Value = "loser")]
    WorstDealsFirst,

    /// <summary>Winners first, randomized within the group.</summary>
    [EnumMember(Value = "winnerRandom")]
    BestDealsFirstRandomized,

    /// <summary>Losers first, randomized within the group.</summary>
    [EnumMember(Value = "loserRandom")]
    WorstDealsFirstRandomized,

    /// <summary>Win/loss margin ascending.</summary>
    [EnumMember(Value = "winLossAz")]
    WinLossAscending,

    /// <summary>Win/loss margin descending.</summary>
    [EnumMember(Value = "winLossZa")]
    WinLossDescending,

    /// <summary>Least-sold items first.</summary>
    [EnumMember(Value = "soldAz")]
    SoldAscending,

    /// <summary>Most-sold (most popular) items first.</summary>
    [EnumMember(Value = "soldZa")]
    SoldDescending,

    /// <summary>Alphabetical by market hash name.</summary>
    [EnumMember(Value = "name")]
    Name,

    /// <summary>Lowest market-cap score first.</summary>
    [EnumMember(Value = "pointsAz")]
    PointsAscending,

    /// <summary>Highest market-cap score first.</summary>
    [EnumMember(Value = "pointsZa")]
    PointsDescending,

    /// <summary>Random order. Slowest option; useful for discovery.</summary>
    [EnumMember(Value = "random")]
    Random,
}
