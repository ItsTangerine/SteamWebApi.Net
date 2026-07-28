namespace SteamWebAPI.Models.Items;

/// <summary>Options for <see cref="SteamWebApiClient.GetItemsPreviewAsync"/>.</summary>
public sealed class GetItemsPreviewRequest
{
    /// <summary>Filters to items whose name contains this text (case-insensitive).</summary>
    public string? Search { get; set; }

    /// <summary>Filters to a specific skin pattern id.</summary>
    public int? PaintIndex { get; set; }

    /// <summary>Filters to a specific weapon id.</summary>
    public int? DefIndex { get; set; }

    /// <summary>
    /// When true (the default), Doppler-style phase variants of the same skin are combined into one entry with a
    /// populated <see cref="ItemPreview.Variants"/> list. When false, each phase is returned as its own entry.
    /// </summary>
    public bool? Grouped { get; set; }

    /// <summary>When true, returns all supported catalog categories instead of only skins.</summary>
    public bool? ShowAll { get; set; }

    /// <summary>When true, returns only items that do not yet exist in the item database.</summary>
    public bool? PreviewOnly { get; set; }

    /// <summary>When true, returns only items that have Doppler-style phases.</summary>
    public bool? OnlyPhases { get; set; }
}
