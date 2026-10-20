namespace SteamWebAPI.Models.Inventory;

/// <summary>One page of a Steam inventory, as returned by <see cref="SteamWebApiClient.GetInventoryPageAsync"/>.</summary>
public sealed class InventoryPage
{
    /// <summary>Creates a new <see cref="InventoryPage"/>.</summary>
    /// <param name="items">The items on this page.</param>
    /// <param name="lastAssetId">The cursor for the next page, or <see langword="null"/> when this is the last page.</param>
    public InventoryPage(IReadOnlyList<InventoryItem> items, string? lastAssetId)
    {
        Items = items ?? throw new ArgumentNullException(nameof(items));
        LastAssetId = lastAssetId;
    }

    /// <summary>
    /// The items on this page. A page can hold fewer items than Steam's page size (e.g. because non-tradable assets
    /// are filtered out by default), so use <see cref="HasMore"/> rather than the item count to decide whether to
    /// continue.
    /// </summary>
    public IReadOnlyList<InventoryItem> Items { get; }

    /// <summary>
    /// The <c>last_assetid</c> response header: pass it as <see cref="GetInventoryRequest.StartAssetId"/> to fetch
    /// the next page. <see langword="null"/> when there are no more pages.
    /// </summary>
    public string? LastAssetId { get; }

    /// <summary>Whether another page follows this one.</summary>
    public bool HasMore => LastAssetId is not null;
}
