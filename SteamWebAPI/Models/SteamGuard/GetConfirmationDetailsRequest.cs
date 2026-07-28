using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>Request body for <see cref="SteamWebApiClient.GetConfirmationDetailsAsync"/>.</summary>
public sealed class GetConfirmationDetailsRequest
{
    /// <summary>The account's mobile authenticator identity secret. Required.</summary>
    [JsonPropertyName("identitysecret")]
    public string IdentitySecret { get; set; } = string.Empty;

    /// <summary>The account's <c>steamLoginSecure</c> cookie value. Required.</summary>
    [JsonPropertyName("steamloginsecure")]
    public string SteamLoginSecure { get; set; } = string.Empty;

    /// <summary>The account's SteamID64. Required.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The confirmation id to fetch details for. Required.</summary>
    [JsonPropertyName("confid")]
    public string ConfId { get; set; } = string.Empty;

    /// <summary>The mobile authenticator device id.</summary>
    [JsonPropertyName("deviceid")]
    public string? DeviceId { get; set; }
}
