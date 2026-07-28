using System.Text.Json.Serialization;
using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Items;

/// <summary>Lightweight CS2 item metadata without pricing, as returned by <see cref="SteamWebApiClient.GetItemsPreviewAsync"/>.</summary>
public sealed class ItemPreview
{
    /// <summary>steamwebapi.com's internal catalog id, e.g. "skin-...".</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>The canonical Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The direct Steam Community Market listing URL.</summary>
    [JsonPropertyName("steamurl")]
    public string? SteamUrl { get; set; }

    /// <summary>The weapon/item definition index.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int PaintIndex { get; set; }

    /// <summary>The weapon/item type slug, e.g. "karambit".</summary>
    [JsonPropertyName("itemtype")]
    public string? ItemType { get; set; }

    /// <summary>The skin name, e.g. "doppler".</summary>
    [JsonPropertyName("itemname")]
    public string? ItemName { get; set; }

    /// <summary>The rarity label, e.g. "Covert".</summary>
    [JsonPropertyName("rarity")]
    public string? Rarity { get; set; }

    /// <summary>The item's display color as a hex string without a leading '#'.</summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>Whether this is the StatTrak™ variant.</summary>
    [JsonPropertyName("isstattrak")]
    public bool IsStatTrak { get; set; }

    /// <summary>Whether this is the Souvenir variant.</summary>
    [JsonPropertyName("issouvenir")]
    public bool IsSouvenir { get; set; }

    /// <summary>The Doppler-style phase name, when this skin has phases.</summary>
    [JsonPropertyName("phase")]
    public string? Phase { get; set; }

    /// <summary>The exterior/wear category.</summary>
    [JsonPropertyName("wear")]
    public Wear? Wear { get; set; }

    /// <summary>The minimum possible float value for this skin pattern.</summary>
    [JsonPropertyName("minfloat")]
    public double MinFloat { get; set; }

    /// <summary>The maximum possible float value for this skin pattern.</summary>
    [JsonPropertyName("maxfloat")]
    public double MaxFloat { get; set; }

    /// <summary>The Steam economy (or steamwebapi.com preview proxy) image URL.</summary>
    [JsonPropertyName("itemimage")]
    public string? ItemImage { get; set; }

    /// <summary>Whether this catalog entry predates the current economy schema.</summary>
    [JsonPropertyName("legacy")]
    public bool? Legacy { get; set; }

    /// <summary>The collection this item belongs to, when applicable.</summary>
    [JsonPropertyName("collection")]
    public string? Collection { get; set; }

    /// <summary>The base display name shared across this item's wear/StatTrak/souvenir variants.</summary>
    [JsonPropertyName("groupname")]
    public string? GroupName { get; set; }

    /// <summary>True if this item has no live Steam image yet (newly discovered).</summary>
    [JsonPropertyName("new")]
    public bool IsNew { get; set; }

    /// <summary>True if this item does not yet exist in steamwebapi.com's item database.</summary>
    [JsonPropertyName("preview")]
    public bool IsPreview { get; set; }

    /// <summary>True if this item was first sold within the last 30 days.</summary>
    [JsonPropertyName("recent")]
    public bool IsRecent { get; set; }

    /// <summary>Doppler-style phase siblings of this item, present only when grouped and the skin has phases.</summary>
    [JsonPropertyName("variants")]
    public IReadOnlyList<ItemPreviewVariant>? Variants { get; set; }
}

/// <summary>A single Doppler-style phase variant embedded in an <see cref="ItemPreview"/>'s <see cref="ItemPreview.Variants"/> list.</summary>
public sealed class ItemPreviewVariant
{
    /// <summary>steamwebapi.com's internal catalog id for this specific phase.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>The canonical Steam Market hash name (shared across phases; only <see cref="Phase"/> differs).</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The phase name, e.g. "Phase 1", "Ruby".</summary>
    [JsonPropertyName("phase")]
    public string Phase { get; set; } = string.Empty;

    /// <summary>The paint index specific to this phase.</summary>
    [JsonPropertyName("paintindex")]
    public int PaintIndex { get; set; }

    /// <summary>The image URL for this phase.</summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}
