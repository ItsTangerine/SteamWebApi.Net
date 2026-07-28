using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Float;

/// <summary>The paginated envelope returned by <see cref="SteamWebApiClient.SearchFloatAssetsAsync"/>.</summary>
public sealed class FloatAssetSearchResult
{
    /// <summary>The request status, e.g. "success".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>The total number of rows matching the filters, across all pages.</summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>The page size that was applied.</summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    /// <summary>The number of rows that were skipped before this page.</summary>
    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    /// <summary>The number of rows included in this page (<see cref="Data"/>'s length).</summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>The sort order that was applied.</summary>
    [JsonPropertyName("sort")]
    public string Sort { get; set; } = string.Empty;

    /// <summary>The number of API credits this call consumed.</summary>
    [JsonPropertyName("credits_used")]
    public int CreditsUsed { get; set; }

    /// <summary>The matching rows for this page.</summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<FloatAssetRecord> Data { get; set; } = Array.Empty<FloatAssetRecord>();
}
