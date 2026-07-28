using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>
/// A Steam mobile authenticator credential bundle, in the standard Steam Desktop Authenticator "maFile" JSON format.
/// Produced by <c>POST /steam/api/guard/add</c> and optionally supplied back to it (step 2, as an alternative to
/// <see cref="AddGuardRequest.SharedSecret"/>/<see cref="AddGuardRequest.AccessToken"/>).
/// </summary>
public sealed class MaFile
{
    /// <summary>The base64 TOTP shared secret used to generate Steam Guard codes.</summary>
    [JsonPropertyName("shared_secret")]
    public string? SharedSecret { get; set; }

    /// <summary>The authenticator's serial number.</summary>
    [JsonPropertyName("serial_number")]
    public string? SerialNumber { get; set; }

    /// <summary>
    /// The code required to deactivate the authenticator (e.g. <c>"R12345"</c>). Cannot be recovered after
    /// enrollment completes — callers must persist it immediately.
    /// </summary>
    [JsonPropertyName("revocation_code")]
    public string? RevocationCode { get; set; }

    /// <summary>The <c>otpauth://</c> URI encoding this authenticator's TOTP parameters.</summary>
    [JsonPropertyName("uri")]
    public string? Uri { get; set; }

    /// <summary>The server time (Unix timestamp, as a string) at which this maFile was generated.</summary>
    [JsonPropertyName("server_time")]
    public string? ServerTime { get; set; }

    /// <summary>The Steam account login name.</summary>
    [JsonPropertyName("account_name")]
    public string? AccountName { get; set; }

    /// <summary>The authenticator's token group id.</summary>
    [JsonPropertyName("token_gid")]
    public string? TokenGid { get; set; }

    /// <summary>The identity secret used to sign mobile confirmation requests.</summary>
    [JsonPropertyName("identity_secret")]
    public string? IdentitySecret { get; set; }

    /// <summary>An additional authenticator secret.</summary>
    [JsonPropertyName("secret_1")]
    public string? Secret1 { get; set; }

    /// <summary>The authenticator's enrollment status code.</summary>
    [JsonPropertyName("status")]
    public int? Status { get; set; }

    /// <summary>The mobile authenticator device id.</summary>
    [JsonPropertyName("device_id")]
    public string? DeviceId { get; set; }

    /// <summary>Whether enrollment has fully completed.</summary>
    [JsonPropertyName("fully_enrolled")]
    public bool? FullyEnrolled { get; set; }

    /// <summary>The web session captured at enrollment time.</summary>
    [JsonPropertyName("Session")]
    public MaFileSession? Session { get; set; }
}
