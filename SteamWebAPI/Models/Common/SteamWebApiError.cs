using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>
/// The error body steamwebapi.com returns for non-2xx responses: <c>{ "error": "..." }</c> for most endpoints, or
/// <c>{ "status": "error", "code": "...", "message": "..." }</c> for the screenshot endpoints.
/// </summary>
public sealed class SteamWebApiError
{
    /// <summary>The server-provided error message.</summary>
    [JsonPropertyName("error")]
    public string? ErrorMessage { get; set; }

    /// <summary>The server-provided error message, for endpoints that use the <c>message</c> field instead of <c>error</c>.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
