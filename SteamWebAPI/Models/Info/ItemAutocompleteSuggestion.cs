using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Info;

/// <summary>A single search-input suggestion returned by <see cref="SteamWebApiClient.AutocompleteItemsAsync"/>.</summary>
public sealed class ItemAutocompleteSuggestion : BaseResponseDto
{
    /// <summary>The canonical Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>
    /// A Steam CDN image identifier for this item. steamwebapi.com's examples return a bare image-hash fragment
    /// rather than a complete URL, so this may need to be combined with a Steam economy image base URL before use.
    /// </summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }
}
