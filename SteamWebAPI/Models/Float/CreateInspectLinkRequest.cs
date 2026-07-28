using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Float;

/// <summary>Request body for <see cref="SteamWebApiClient.CreateInspectLinkAsync"/>.</summary>
public sealed class CreateInspectLinkRequest
{
    /// <summary>The weapon/item definition index. Required.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The skin paint index. Defaults to 0.</summary>
    [JsonPropertyName("paintindex")]
    public int? PaintIndex { get; set; }

    /// <summary>The paint seed. Defaults to 0.</summary>
    [JsonPropertyName("paintseed")]
    public int? PaintSeed { get; set; }

    /// <summary>The float value. Defaults to 0.</summary>
    [JsonPropertyName("float")]
    public double? Float { get; set; }

    /// <summary>The rarity code.</summary>
    [JsonPropertyName("rarity")]
    public int? Rarity { get; set; }

    /// <summary>The Steam economy quality code. Auto-derived from <see cref="StatTrak"/>/<see cref="Souvenir"/> (4/9/12) when omitted.</summary>
    [JsonPropertyName("quality")]
    public int? Quality { get; set; }

    /// <summary>Whether to encode this as the StatTrak™ variant. Defaults to false.</summary>
    [JsonPropertyName("stattrak")]
    public bool? StatTrak { get; set; }

    /// <summary>The StatTrak™ kill counter value. Defaults to 0.</summary>
    [JsonPropertyName("killeatercount")]
    public int? KillEaterCount { get; set; }

    /// <summary>Whether to encode this as the Souvenir variant. Defaults to false.</summary>
    [JsonPropertyName("souvenir")]
    public bool? Souvenir { get; set; }

    /// <summary>A custom name tag to encode.</summary>
    [JsonPropertyName("customname")]
    public string? CustomName { get; set; }

    /// <summary>The Steam item id to encode.</summary>
    [JsonPropertyName("itemid")]
    public string? ItemId { get; set; }

    /// <summary>The owning Steam account id (32-bit) to encode.</summary>
    [JsonPropertyName("accountid")]
    public int? AccountId { get; set; }

    /// <summary>The Steam economy origin code to encode.</summary>
    [JsonPropertyName("origin")]
    public int? Origin { get; set; }

    /// <summary>The music kit index to encode.</summary>
    [JsonPropertyName("musicindex")]
    public int? MusicIndex { get; set; }

    /// <summary>The Steam inventory flag/slot value to encode.</summary>
    [JsonPropertyName("inventory")]
    public int? Inventory { get; set; }

    /// <summary>The weapon finish style index to encode (for skins with multiple finish styles).</summary>
    [JsonPropertyName("style")]
    public int? Style { get; set; }

    /// <summary>The stickers to encode. Maximum 5.</summary>
    [JsonPropertyName("stickers")]
    public IReadOnlyList<CreateInspectSticker>? Stickers { get; set; }

    /// <summary>The keychains to encode. Maximum 1.</summary>
    [JsonPropertyName("keychains")]
    public IReadOnlyList<CreateInspectKeychain>? Keychains { get; set; }

    /// <summary>Doppler-style pattern/phase variation data to encode. Maximum 5. Shares the sticker slot shape per steamwebapi.com's description.</summary>
    [JsonPropertyName("variations")]
    public IReadOnlyList<CreateInspectSticker>? Variations { get; set; }
}

/// <summary>A single sticker slot in a <see cref="CreateInspectLinkRequest"/>.</summary>
public sealed class CreateInspectSticker
{
    /// <summary>The sticker's item id. Required.</summary>
    [JsonPropertyName("sticker_id")]
    public int StickerId { get; set; }

    /// <summary>The slot this sticker occupies.</summary>
    [JsonPropertyName("slot")]
    public int? Slot { get; set; }

    /// <summary>The sticker's wear/scuff value.</summary>
    [JsonPropertyName("wear")]
    public double? Wear { get; set; }

    /// <summary>The sticker's rotation.</summary>
    [JsonPropertyName("rotation")]
    public double? Rotation { get; set; }

    /// <summary>The sticker's X offset.</summary>
    [JsonPropertyName("offset_x")]
    public double? OffsetX { get; set; }

    /// <summary>The sticker's Y offset.</summary>
    [JsonPropertyName("offset_y")]
    public double? OffsetY { get; set; }

    /// <summary>The sticker's scale.</summary>
    [JsonPropertyName("scale")]
    public double? Scale { get; set; }

    /// <summary>The sticker's pattern (foil/holo variant), when applicable.</summary>
    [JsonPropertyName("pattern")]
    public int? Pattern { get; set; }

    /// <summary>The sticker's tint id, for tintable stickers.</summary>
    [JsonPropertyName("tint_id")]
    public int? TintId { get; set; }

    /// <summary>The sticker's Z offset.</summary>
    [JsonPropertyName("offset_z")]
    public double? OffsetZ { get; set; }
}

/// <summary>A single keychain slot in a <see cref="CreateInspectLinkRequest"/>.</summary>
public sealed class CreateInspectKeychain
{
    /// <summary>The keychain's charm item id. Required. Reuses the <c>sticker_id</c> wire field name.</summary>
    [JsonPropertyName("sticker_id")]
    public int StickerId { get; set; }

    /// <summary>The slot this keychain occupies.</summary>
    [JsonPropertyName("slot")]
    public int? Slot { get; set; }

    /// <summary>The keychain's charm pattern.</summary>
    [JsonPropertyName("pattern")]
    public int? Pattern { get; set; }
}
