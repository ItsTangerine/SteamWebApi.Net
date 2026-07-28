using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>A single Steam economy tag (category/type/rarity/exterior/quality) attached to an item.</summary>
public sealed class TagItem
{
    /// <summary>The tag category, e.g. "Type", "Weapon", "Rarity", "Exterior".</summary>
    [JsonPropertyName("category")]
    public string Category { get; set; } = string.Empty;

    /// <summary>Steam's internal identifier for this tag value, e.g. "CSGO_Type_Knife".</summary>
    [JsonPropertyName("internal_name")]
    public string InternalName { get; set; } = string.Empty;

    /// <summary>The localized display name of the category.</summary>
    [JsonPropertyName("localized_category_name")]
    public string LocalizedCategoryName { get; set; } = string.Empty;

    /// <summary>The localized display name of the tag value.</summary>
    [JsonPropertyName("localized_tag_name")]
    public string LocalizedTagName { get; set; } = string.Empty;

    /// <summary>The tag's display color as a hex string without a leading '#', when Steam provides one.</summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }
}
