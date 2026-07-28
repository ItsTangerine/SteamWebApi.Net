using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Inventory;

/// <summary>Options for <see cref="SteamWebApiClient.GetInventoryAsync"/>.</summary>
public sealed class GetInventoryRequest
{
    /// <summary>How fresh the inventory data should be. Defaults to <see cref="InventoryState.Active"/>.</summary>
    public InventoryState? State { get; set; }

    /// <summary>
    /// A <c>steamLoginSecure</c> session cookie value for the inventory's own owner, used to bypass trade-lock
    /// restrictions. When provided, <c>steam_id</c> is ignored server-side.
    /// </summary>
    public string? SteamLoginSecure { get; set; }

    /// <summary>The game to fetch the inventory for. Free text; defaults to "cs2".</summary>
    public string? Game { get; set; }

    /// <summary>The localization language for item names/descriptions. Defaults to <see cref="InventoryLanguage.English"/>.</summary>
    public InventoryLanguage? Language { get; set; }

    /// <summary>When true, bypasses steamwebapi.com's 3-day cache for this call (costs +1 credit).</summary>
    public bool? NoCache { get; set; }

    /// <summary>When true, collapses duplicate items by market hash name, summing their <c>count</c>.</summary>
    public bool? Group { get; set; }

    /// <summary>How to order the results. Defaults to <see cref="InventorySort.PriceMax"/>.</summary>
    public InventorySort? Sort { get; set; }

    /// <summary>Converts all prices to this currency. Only applies when the enriched (<c>parse=1</c>) response is used. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }

    /// <summary>
    /// Restricts the returned JSON to only these field names, reducing payload size. Field names must match
    /// steamwebapi.com's lowercase wire names, not the C# property names.
    /// </summary>
    public IReadOnlyList<string>? SelectFields { get; set; }

    /// <summary>When true, includes non-tradeable/trade-locked items in the results.</summary>
    public bool? WithNoTradable { get; set; }

    /// <summary>A Steam trade URL. When provided, auto-includes CS2 items that are 7-10 days from becoming trade-unlocked.</summary>
    public string? TradeUrl { get; set; }

    /// <summary>The number of items to skip, for pagination. Defaults to 0.</summary>
    public int? Offset { get; set; }

    /// <summary>The maximum number of items to return (1-10000). Defaults to 10000.</summary>
    public int? Limit { get; set; }

    /// <summary>When true, attempts to include items still within their first 7 days of a trade lock (costs +1 credit).</summary>
    public bool? TryFirstSevenDaysBlockedItems { get; set; }

    /// <summary>Restricts third-party pricing (<c>pricereal</c>) to these markets. Defaults to all markets.</summary>
    public IReadOnlyList<Market>? Markets { get; set; }

    /// <summary>When true, includes the <c>prices</c> array (per-market current offers) on each item.</summary>
    public bool? WithPrices { get; set; }

    /// <summary>When true, computes <c>pricereal</c> per Doppler phase instead of for the base skin.</summary>
    public bool? WithPhasePrice { get; set; }

    /// <summary>Filters items to those whose name contains this text, case-insensitive.</summary>
    public string? Search { get; set; }

    /// <summary>
    /// When true, shows trade-locked items in the owner's own inventory. Requires <see cref="SteamLoginSecure"/>
    /// to be set.
    /// </summary>
    public bool? TradeLocked { get; set; }

    /// <summary>
    /// A pagination cursor: the <c>last_assetid</c> value from a previous response's <c>last_assetid</c> header.
    /// Repeat the call with this value while the response continues to include that header.
    /// </summary>
    public string? StartAssetId { get; set; }
}
