using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Info;

/// <summary>An item group and the item type slugs that belong to it, as returned by <see cref="SteamWebApiClient.GetItemInfoStructureAsync"/>.</summary>
public sealed class ItemInfoGroup : BaseResponseDto
{
    /// <summary>The group's display name, e.g. "Knife".</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>The item type slugs belonging to this group.</summary>
    [JsonPropertyName("relation")]
    public IReadOnlyList<string> Relation { get; set; } = Array.Empty<string>();
}
