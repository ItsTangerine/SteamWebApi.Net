using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Account;

/// <summary>
/// The caller's account info and API usage statistics, as returned by <see cref="SteamWebApiClient.GetAccountAsync"/>.
/// </summary>
/// <remarks>
/// steamwebapi.com's documentation does not publish an example or schema for this endpoint, only a prose
/// description implying a usage breakdown across periods, subscription status/expiry, and the latest Steam Web API
/// status. The field names and shapes below are a best-effort inference, not confirmed field names. Verify this
/// shape against a live call before relying on it.
/// </remarks>
public sealed class AccountInfo : BaseResponseDto
{
    /// <summary>Whether the request succeeded.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>The caller's API request usage, broken down by rolling window.</summary>
    [JsonPropertyName("usage")]
    public AccountUsage? Usage { get; set; }

    /// <summary>The caller's subscription plan status.</summary>
    [JsonPropertyName("subscription")]
    public AccountSubscription? Subscription { get; set; }

    /// <summary>The latest known status of the underlying Steam Web API (e.g. "online", "degraded").</summary>
    [JsonPropertyName("steam_api_status")]
    public string? SteamApiStatus { get; set; }
}

/// <summary>The caller's API request usage broken down by rolling window, embedded in an <see cref="AccountInfo"/>.</summary>
/// <remarks>Inferred shape; field names are not confirmed by steamwebapi.com's documentation. Verify against a live call.</remarks>
public sealed class AccountUsage : BaseResponseDto
{
    /// <summary>Requests made in the current minute.</summary>
    [JsonPropertyName("minute")]
    public int? Minute { get; set; }

    /// <summary>Requests made in the current hour.</summary>
    [JsonPropertyName("hour")]
    public int? Hour { get; set; }

    /// <summary>Requests made in the current day.</summary>
    [JsonPropertyName("day")]
    public int? Day { get; set; }

    /// <summary>Requests made in the current week.</summary>
    [JsonPropertyName("week")]
    public int? Week { get; set; }

    /// <summary>Requests made in the current month.</summary>
    [JsonPropertyName("month")]
    public int? Month { get; set; }
}

/// <summary>The caller's subscription plan status, embedded in an <see cref="AccountInfo"/>.</summary>
/// <remarks>Inferred shape; field names are not confirmed by steamwebapi.com's documentation. Verify against a live call.</remarks>
public sealed class AccountSubscription : BaseResponseDto
{
    /// <summary>The subscription plan name or status label, e.g. "active", "free".</summary>
    [JsonPropertyName("status")]
    public string? Status { get; set; }

    /// <summary>When the current subscription period expires, when applicable.</summary>
    [JsonPropertyName("expires_at")]
    public DateTimeOffset? ExpiresAt { get; set; }
}
