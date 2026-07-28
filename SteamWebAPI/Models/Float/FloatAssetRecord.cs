using System.Text.Json;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Common;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Float;

/// <summary>A single stored CS asset row (inventory item or market listing), as returned by <see cref="SteamWebApiClient.SearchFloatAssetsAsync"/>.</summary>
public sealed class FloatAssetRecord
{
    /// <summary>steamwebapi.com's internal short id for this row.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>A stable identifier unique to this specific float/asset observation.</summary>
    [JsonPropertyName("uniqueid")]
    public string UniqueId { get; set; } = string.Empty;

    /// <summary>The current Steam asset id.</summary>
    [JsonPropertyName("assetid")]
    public string AssetId { get; set; } = string.Empty;

    /// <summary>The market listing id, when this row is a market listing rather than an owned inventory item.</summary>
    [JsonPropertyName("marketid")]
    public string? MarketId { get; set; }

    /// <summary>The owning SteamID64, when this row is an owned inventory item rather than a pure market listing.</summary>
    [JsonPropertyName("steamid")]
    public string? SteamId { get; set; }

    /// <summary>The Steam "D" inspect-link parameter value.</summary>
    [JsonPropertyName("d")]
    public string D { get; set; } = string.Empty;

    /// <summary>The canonical Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The weapon/item definition index.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The Steam economy quality code.</summary>
    [JsonPropertyName("quality")]
    public int Quality { get; set; }

    /// <summary>The rarity code, typically 1-6.</summary>
    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }

    /// <summary>The Steam economy origin code.</summary>
    [JsonPropertyName("origin")]
    public int Origin { get; set; }

    /// <summary>Whether this is the StatTrak™ variant.</summary>
    [JsonPropertyName("isstattrak")]
    [JsonConverter(typeof(FlexibleBooleanConverter))]
    public bool IsStatTrak { get; set; }

    /// <summary>Whether this is the Souvenir variant.</summary>
    [JsonPropertyName("issouvenir")]
    [JsonConverter(typeof(FlexibleBooleanConverter))]
    public bool IsSouvenir { get; set; }

    /// <summary>The exterior/wear category.</summary>
    [JsonPropertyName("wear")]
    public Wear Wear { get; set; }

    /// <summary>The item's float value.</summary>
    [JsonPropertyName("float")]
    public double FloatValue { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int PaintIndex { get; set; }

    /// <summary>The paint seed.</summary>
    [JsonPropertyName("paintseed")]
    public int PaintSeed { get; set; }

    /// <summary>
    /// The Doppler-style phase, when this skin has phases, e.g. "p1".."p4", "ruby", "sapphire", "black-pearl",
    /// "emerald". Free text; not a closed set.
    /// </summary>
    [JsonPropertyName("phase")]
    public string? Phase { get; set; }

    /// <summary>The stickers applied to this item.</summary>
    [JsonPropertyName("stickers")]
    public IReadOnlyList<FloatAssetSticker> Stickers { get; set; } = Array.Empty<FloatAssetSticker>();

    /// <summary>The keychains attached to this item.</summary>
    [JsonPropertyName("keychains")]
    public IReadOnlyList<FloatAssetKeychain> Keychains { get; set; } = Array.Empty<FloatAssetKeychain>();

    /// <summary>The previously known asset id for this item, e.g. before a trade.</summary>
    [JsonPropertyName("prevassetid")]
    public string? PrevAssetId { get; set; }

    /// <summary>The previously known market listing id for this item.</summary>
    [JsonPropertyName("prevmarketid")]
    public string? PrevMarketId { get; set; }

    /// <summary>The previously known owning SteamID64 for this item.</summary>
    [JsonPropertyName("prevsteamid")]
    public string? PrevSteamId { get; set; }

    /// <summary>The previously known Steam "D" inspect-link parameter value for this item.</summary>
    [JsonPropertyName("prevd")]
    public string? PrevD { get; set; }

    /// <summary>The previously known stickers for this item.</summary>
    [JsonPropertyName("prevstickers")]
    public IReadOnlyList<FloatAssetSticker> PrevStickers { get; set; } = Array.Empty<FloatAssetSticker>();

    /// <summary>The previously known keychains for this item.</summary>
    [JsonPropertyName("prevkeychains")]
    public IReadOnlyList<FloatAssetKeychain> PrevKeychains { get; set; } = Array.Empty<FloatAssetKeychain>();

    /// <summary>The data source this row was observed on, e.g. "skinport", "csfloat", "inventory". Free text; not a closed set.</summary>
    [JsonPropertyName("source")]
    public string Source { get; set; } = string.Empty;

    /// <summary>A URL to view this listing/item at its source.</summary>
    [JsonPropertyName("link")]
    public string Link { get; set; } = string.Empty;

    /// <summary>
    /// Loosely-typed source-specific extra data, e.g. <c>{ "min_price": 1.23, "currency": "USD" }</c> for market
    /// listings. Shape varies by source; not a fixed schema.
    /// </summary>
    [JsonPropertyName("metadata")]
    public Dictionary<string, JsonElement>? Metadata { get; set; }

    /// <summary>When this row was first created, as <c>yyyy-MM-dd HH:mm:ss</c>.</summary>
    [JsonPropertyName("createdat")]
    public string CreatedAt { get; set; } = string.Empty;

    /// <summary>When this row was last updated, as <c>yyyy-MM-dd HH:mm:ss</c>.</summary>
    [JsonPropertyName("updatedat")]
    public string UpdatedAt { get; set; } = string.Empty;

    /// <summary>The date this row was first created, as <c>yyyy-MM-dd</c>.</summary>
    [JsonPropertyName("createddate")]
    public string CreatedDate { get; set; } = string.Empty;

    /// <summary>The items-database catalog entry for this row, present only when <c>with_items=1</c> was requested. Shape not published; returned as raw JSON.</summary>
    [JsonPropertyName("item")]
    public JsonElement? Item { get; set; }

    /// <summary>The owning Steam profile for this row, present only when <c>with_profiles=1</c> was requested. Shape not published; returned as raw JSON.</summary>
    [JsonPropertyName("profile")]
    public JsonElement? Profile { get; set; }
}

/// <summary>A sticker reference embedded in a <see cref="FloatAssetRecord"/>'s <see cref="FloatAssetRecord.Stickers"/>/<see cref="FloatAssetRecord.PrevStickers"/> lists.</summary>
public sealed class FloatAssetSticker
{
    /// <summary>The sticker's definition index.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The sticker's Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;
}

/// <summary>A keychain reference embedded in a <see cref="FloatAssetRecord"/>'s <see cref="FloatAssetRecord.Keychains"/>/<see cref="FloatAssetRecord.PrevKeychains"/> lists.</summary>
public sealed class FloatAssetKeychain
{
    /// <summary>The keychain's charm pattern.</summary>
    [JsonPropertyName("pattern")]
    public int Pattern { get; set; }

    /// <summary>The keychain's definition index.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The keychain's Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;
}
