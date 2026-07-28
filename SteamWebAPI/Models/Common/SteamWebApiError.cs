using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Common;

/// <summary>The <c>{ "error": "..." }</c> body shape steamwebapi.com returns for most non-2xx responses.</summary>
public sealed class SteamWebApiError
{
    /// <summary>The server-provided error message.</summary>
    [JsonPropertyName("error")]
    public string? ErrorMessage { get; set; }
}
