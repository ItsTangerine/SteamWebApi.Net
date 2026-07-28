using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>A Doppler/Gamma Doppler/Marble Fade phase variant of a base item, as embedded in a full item payload's <c>variants</c> array.</summary>
public sealed class ItemVariant
{
    /// <summary>The phase name, e.g. "Phase 1", "Ruby", "Sapphire", "Black Pearl".</summary>
    [JsonPropertyName("phase")]
    public string Phase { get; set; } = string.Empty;

    /// <summary>The paint index specific to this phase.</summary>
    [JsonPropertyName("paintindex")]
    public int PaintIndex { get; set; }

    /// <summary>The Steam economy image URL for this phase.</summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}
