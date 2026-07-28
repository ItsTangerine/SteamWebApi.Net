using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Inventory;

/// <summary>Options for <see cref="SteamWebApiClient.GetInventoryBatchAsync"/>.</summary>
public sealed class GetInventoryBatchRequest
{
    /// <summary>The game to fetch inventories for. Defaults to <see cref="InventoryGame.Cs2"/>. A closed set, unlike the single-inventory endpoint.</summary>
    public InventoryGame? Game { get; set; }

    /// <summary>
    /// Restricts the returned JSON to only these field names, reducing payload size. Field names must match
    /// steamwebapi.com's lowercase wire names, not the C# property names.
    /// </summary>
    public IReadOnlyList<string>? SelectFields { get; set; }

    /// <summary>Converts all prices to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }

    /// <summary>The localization language for item names/descriptions. Defaults to <see cref="InventoryLanguage.English"/>.</summary>
    public InventoryLanguage? Language { get; set; }

    /// <summary>When true, bypasses steamwebapi.com's 3-day cache for this call (costs +1 credit per Steam ID).</summary>
    public bool? NoCache { get; set; }

    /// <summary>When true, includes non-tradeable/trade-locked items in the results.</summary>
    public bool? WithNoTradable { get; set; }

    /// <summary>When true, computes <c>pricereal</c> per Doppler phase instead of for the base skin.</summary>
    public bool? WithPhasePrice { get; set; }

    /// <summary>How to order each Steam ID's items. Defaults to <see cref="InventorySort.PriceMax"/>.</summary>
    public InventorySort? Sort { get; set; }

    /// <summary>When true, collapses duplicate items by market hash name, summing their <c>count</c>.</summary>
    public bool? Group { get; set; }

    /// <summary>Restricts third-party pricing (<c>pricereal</c>) to these markets. Defaults to all markets.</summary>
    public IReadOnlyList<Market>? Markets { get; set; }
}
