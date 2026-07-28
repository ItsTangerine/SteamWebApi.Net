using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>A single entry in an item's Steam economy description list.</summary>
public sealed class DescriptionItem
{
    /// <summary>The content type, e.g. "html".</summary>
    [JsonPropertyName("type")]
    public string Type { get; set; } = string.Empty;

    /// <summary>The description text, which may contain inline HTML markup.</summary>
    [JsonPropertyName("value")]
    public string Value { get; set; } = string.Empty;

    /// <summary>Steam's internal name for this description entry, e.g. "description".</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;
}
