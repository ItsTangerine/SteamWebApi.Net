using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Screenshot;

/// <summary>
/// Screenshot quota and consumption, as returned by <see cref="SteamWebApiClient.GetScreenshotUsageAsync"/>.
/// Screenshot requests (including retries and job retrievals) are counted separately from ordinary credits.
/// </summary>
public sealed class ScreenshotUsage : BaseResponseDto
{
    /// <summary>The response status, "success".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>Whether screenshot access is active for this key.</summary>
    [JsonPropertyName("active")]
    public bool Active { get; set; }

    /// <summary>The quota per window; a <see langword="null"/> limit means unlimited.</summary>
    [JsonPropertyName("limits")]
    public ScreenshotLimits Limits { get; set; } = new();

    /// <summary>The requests consumed per window.</summary>
    [JsonPropertyName("usage")]
    public ScreenshotUsageCounters Usage { get; set; } = new();

    /// <summary>The timezone of the day/month windows, "UTC".</summary>
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; } = string.Empty;

    /// <summary>How the day/month windows are bounded, "calendar".</summary>
    [JsonPropertyName("period")]
    public string Period { get; set; } = string.Empty;

    /// <summary>What is counted, "authenticated_requests".</summary>
    [JsonPropertyName("unit")]
    public string Unit { get; set; } = string.Empty;
}

/// <summary>Screenshot request limits per window. A <see langword="null"/> value means unlimited.</summary>
public sealed class ScreenshotLimits : BaseResponseDto
{
    /// <summary>Requests allowed per minute.</summary>
    [JsonPropertyName("minute")]
    public int? Minute { get; set; }

    /// <summary>Requests allowed per UTC calendar day.</summary>
    [JsonPropertyName("day")]
    public int? Day { get; set; }

    /// <summary>Requests allowed per UTC calendar month.</summary>
    [JsonPropertyName("month")]
    public int? Month { get; set; }
}

/// <summary>Screenshot requests consumed per window.</summary>
public sealed class ScreenshotUsageCounters : BaseResponseDto
{
    /// <summary>Requests in the current minute.</summary>
    [JsonPropertyName("minute")]
    public int Minute { get; set; }

    /// <summary>Requests in the current UTC calendar day.</summary>
    [JsonPropertyName("day")]
    public int Day { get; set; }

    /// <summary>Requests in the current UTC calendar month.</summary>
    [JsonPropertyName("month")]
    public int Month { get; set; }

    /// <summary>Requests across the retained usage history (not necessarily lifetime).</summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>Requests that were blocked by the quota.</summary>
    [JsonPropertyName("blocked")]
    public int Blocked { get; set; }
}
