using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Float;

/// <summary>The generated inspect link, as returned by <see cref="SteamWebApiClient.CreateInspectLinkAsync"/>.</summary>
public sealed class CreateInspectLinkResult : BaseResponseDto
{
    /// <summary>The full <c>steam://rungame/...</c> inspect link.</summary>
    [JsonPropertyName("inspectlink")]
    public string InspectLink { get; set; } = string.Empty;

    /// <summary>The raw hex certificate embedded in <see cref="InspectLink"/>.</summary>
    [JsonPropertyName("certificate")]
    public string Certificate { get; set; } = string.Empty;

    /// <summary>A round-trip decode of what <see cref="Certificate"/> encodes, for verification.</summary>
    [JsonPropertyName("decoded")]
    public CreateInspectLinkDecoded Decoded { get; set; } = new();
}

/// <summary>
/// The round-trip decoded contents of a <see cref="CreateInspectLinkResult"/>. Only the fields steamwebapi.com's
/// example response exemplifies are modeled here; other input fields (e.g. stickers, StatTrak) likely also appear
/// when set on the request, but their echoed shape isn't documented.
/// </summary>
public sealed class CreateInspectLinkDecoded : BaseResponseDto
{
    /// <summary>The weapon/item definition index.</summary>
    [JsonPropertyName("defindex")]
    public int DefIndex { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int PaintIndex { get; set; }

    /// <summary>The paint seed.</summary>
    [JsonPropertyName("paintseed")]
    public int PaintSeed { get; set; }

    /// <summary>The float value.</summary>
    [JsonPropertyName("floatvalue")]
    public double FloatValue { get; set; }

    /// <summary>The rarity code.</summary>
    [JsonPropertyName("rarity")]
    public int Rarity { get; set; }

    /// <summary>The Steam economy quality code.</summary>
    [JsonPropertyName("quality")]
    public int Quality { get; set; }
}
