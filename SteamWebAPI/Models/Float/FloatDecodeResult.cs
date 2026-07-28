using System.Text.Json;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Float;

/// <summary>The decoded float/item state produced by <see cref="SteamWebApiClient.DecodeFloatAsync"/>.</summary>
public sealed class FloatDecodeResult
{
    /// <summary>steamwebapi.com's internal hash id for this decode.</summary>
    [JsonPropertyName("id")]
    public string Id { get; set; } = string.Empty;

    /// <summary>The inspect link that was decoded.</summary>
    [JsonPropertyName("inspectlink")]
    public string InspectLink { get; set; } = string.Empty;

    /// <summary>The Steam economy image URL, replaced with a phase-specific variant when a Doppler phase is detected.</summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    /// <summary>The Steam asset id.</summary>
    [JsonPropertyName("assetid")]
    public string AssetId { get; set; } = string.Empty;

    /// <summary>The canonical Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The item's float value.</summary>
    [JsonPropertyName("float")]
    public double Float { get; set; }

    /// <summary>
    /// The Doppler-style phase, when this skin has phases, e.g. "p1".."p4", "ruby", "sapphire", "black-pearl",
    /// "emerald". Free text; not a closed set.
    /// </summary>
    [JsonPropertyName("phase")]
    public string? Phase { get; set; }

    /// <summary>The decoded item type, e.g. "weapon".</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>The paint seed.</summary>
    [JsonPropertyName("paintseed")]
    public int PaintSeed { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int PaintIndex { get; set; }

    /// <summary>The weapon/item definition index.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The rarity code.</summary>
    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }

    /// <summary>The Steam economy quality code.</summary>
    [JsonPropertyName("quality")]
    public int Quality { get; set; }

    /// <summary>The Steam economy origin code.</summary>
    [JsonPropertyName("origin")]
    public int Origin { get; set; }

    /// <summary>The Steam inventory flag/slot value.</summary>
    [JsonPropertyName("inventory")]
    public int Inventory { get; set; }

    /// <summary>The exterior/wear category.</summary>
    [JsonPropertyName("wear")]
    public Wear Wear { get; set; }

    /// <summary>The wear-range minimum float value for this paint.</summary>
    [JsonPropertyName("min")]
    public double Min { get; set; }

    /// <summary>The wear-range maximum float value for this paint.</summary>
    [JsonPropertyName("max")]
    public double Max { get; set; }

    /// <summary>Whether this item has at least one sticker applied.</summary>
    [JsonPropertyName("hassticker")]
    public bool HasSticker { get; set; }

    /// <summary>Whether this item has a keychain attached.</summary>
    [JsonPropertyName("haskeychain")]
    public bool HasKeychain { get; set; }

    /// <summary>The number of stickers applied.</summary>
    [JsonPropertyName("stickeramount")]
    public int StickerAmount { get; set; }

    /// <summary>The number of keychains attached.</summary>
    [JsonPropertyName("keychainamount")]
    public int KeychainAmount { get; set; }

    /// <summary>The stickers applied to this item.</summary>
    [JsonPropertyName("stickers")]
    public IReadOnlyList<FloatSticker> Stickers { get; set; } = Array.Empty<FloatSticker>();

    /// <summary>The keychains attached to this item.</summary>
    [JsonPropertyName("keychains")]
    public IReadOnlyList<FloatSticker> Keychains { get; set; } = Array.Empty<FloatSticker>();

    /// <summary>Whether this is the StatTrak™ variant.</summary>
    [JsonPropertyName("stattrak")]
    public bool StatTrak { get; set; }

    /// <summary>The StatTrak™ kill counter value, when <see cref="StatTrak"/> is true.</summary>
    [JsonPropertyName("stattrakcount")]
    public int? StatTrakCount { get; set; }

    /// <summary>Whether this is the Souvenir variant.</summary>
    [JsonPropertyName("souvenir")]
    public bool Souvenir { get; set; }

    /// <summary>The custom name tag applied to this item, if any.</summary>
    [JsonPropertyName("nametag")]
    public string? NameTag { get; set; }

    /// <summary>The applied music kit index, if any.</summary>
    [JsonPropertyName("musicindex")]
    public int? MusicIndex { get; set; }

    /// <summary>The rarity display name, e.g. "restricted".</summary>
    [JsonPropertyName("grade")]
    public string? Grade { get; set; }
}

/// <summary>
/// A sticker or keychain slot decoded from a CS2 inspect certificate, as returned by
/// <see cref="SteamWebApiClient.DecodeFloatAsync"/>. This is a different shape from the inventory endpoints'
/// sticker/keychain references and from <see cref="FloatAssetSticker"/>/<see cref="FloatAssetKeychain"/>.
/// </summary>
public sealed class FloatSticker
{
    /// <summary>The sticker's display name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>The sticker's item id.</summary>
    [JsonPropertyName("stickerid")]
    public int StickerId { get; set; }

    /// <summary>The slot this sticker/keychain occupies.</summary>
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    /// <summary>The sticker's wear/scuff value.</summary>
    [JsonPropertyName("wear")]
    public double? Wear { get; set; }

    /// <summary>The sticker's scale.</summary>
    [JsonPropertyName("scale")]
    public double? Scale { get; set; }

    /// <summary>The sticker's rotation.</summary>
    [JsonPropertyName("rotation")]
    public double? Rotation { get; set; }

    /// <summary>The sticker's pattern (foil/holo variant), when applicable.</summary>
    [JsonPropertyName("pattern")]
    public int? Pattern { get; set; }

    /// <summary>The sticker's X offset.</summary>
    [JsonPropertyName("offsetx")]
    public double? OffsetX { get; set; }

    /// <summary>The sticker's Y offset.</summary>
    [JsonPropertyName("offsety")]
    public double? OffsetY { get; set; }

    /// <summary>The sticker's Z offset.</summary>
    [JsonPropertyName("offsetz")]
    public double? OffsetZ { get; set; }

    /// <summary>The sticker's tint id, for tintable stickers.</summary>
    [JsonPropertyName("tintid")]
    public int? TintId { get; set; }

    /// <summary>The POV/highlight reel index, for Danger Zone-style stickers.</summary>
    [JsonPropertyName("highlightreel")]
    public int? HighlightReel { get; set; }

    /// <summary>Unpublished nested data for wrapped gift-style stickers. Shape not published; returned as raw JSON.</summary>
    [JsonPropertyName("wrappedsticker")]
    public JsonElement? WrappedSticker { get; set; }
}
