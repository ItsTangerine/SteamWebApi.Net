using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Items;

/// <summary>Filtering, sorting, and pagination options for <see cref="SteamWebApiClient.GetItemsAsync"/>.</summary>
public sealed class GetItemsRequest
{
    /// <summary>The game to return items for. Defaults to <see cref="Common.Game.Cs2"/>.</summary>
    public Game? Game { get; set; }

    /// <summary>The page number to return, used together with <see cref="Max"/>.</summary>
    public int? Page { get; set; }

    /// <summary>The maximum number of items to return per page (1–50000). Lower values improve response time.</summary>
    public int? Max { get; set; }

    /// <summary>How to order the results.</summary>
    public ItemsSortBy? SortBy { get; set; }

    /// <summary>Filters to items whose name contains this text (minimum 3 characters, case-insensitive).</summary>
    public string? Search { get; set; }

    /// <summary>Minimum Steam Market listing price in USD.</summary>
    public double? PriceMin { get; set; }

    /// <summary>Maximum Steam Market listing price in USD.</summary>
    public double? PriceMax { get; set; }

    /// <summary>Minimum third-party market price in USD.</summary>
    public double? PriceRealMin { get; set; }

    /// <summary>Maximum third-party market price in USD.</summary>
    public double? PriceRealMax { get; set; }

    /// <summary>Filters by item category, e.g. "knife", "gloves", "rifle", "case". Free text; not a closed set.</summary>
    public IReadOnlyList<string>? ItemGroups { get; set; }

    /// <summary>Filters to a specific weapon/item type within a group, e.g. "ak-47", "karambit".</summary>
    public string? ItemType { get; set; }

    /// <summary>Filters to a specific skin name, e.g. "redline", "doppler".</summary>
    public string? ItemName { get; set; }

    /// <summary>Filters by exterior/wear category.</summary>
    public IReadOnlyList<Wear>? Wears { get; set; }

    /// <summary>
    /// Restricts the returned JSON to only these field names, reducing payload size. Field names must match
    /// steamwebapi.com's lowercase wire names (e.g. "markethashname", "pricelatest"), not the C# property names.
    /// </summary>
    public IReadOnlyList<string>? SelectFields { get; set; }

    /// <summary>Converts all prices to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }

    /// <summary>Restricts third-party pricing (<c>pricereal</c>/<c>prices</c>) to these markets. Defaults to all markets.</summary>
    public IReadOnlyList<Market>? Markets { get; set; }

    /// <summary>
    /// When true, appends newly-discovered items that are not yet fully indexed. These carry limited metadata and
    /// no pricing.
    /// </summary>
    public bool? WithPreviewItems { get; set; }
}
