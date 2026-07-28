using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.TradeOffers;

/// <summary>A single item within a <see cref="TradeItems"/> received/sent list.</summary>
public sealed class TradeHistoryItem
{
    /// <summary>
    /// The item's asset id. For received items this equals <see cref="NewAssetId"/> (kept for backwards
    /// compatibility); for sent items this is the asset id at the time of the trade.
    /// </summary>
    [JsonPropertyName("assetid")]
    public string? AssetId { get; set; }

    /// <summary>The item's asset id before the trade. Present on received items.</summary>
    [JsonPropertyName("originalassetid")]
    public string? OriginalAssetId { get; set; }

    /// <summary>The item's asset id after the trade.</summary>
    [JsonPropertyName("newassetid")]
    public string? NewAssetId { get; set; }

    /// <summary>Owner-applied description overrides (e.g. name tags, stickers), when present. Shape is otherwise untyped.</summary>
    [JsonPropertyName("ownerdescriptions")]
    public JsonElement? OwnerDescriptions { get; set; }
}
