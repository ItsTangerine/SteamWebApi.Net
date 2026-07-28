using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>
/// Request body for <see cref="SteamWebApiClient.AddGuardAsync"/> and
/// <see cref="SteamWebApiClient.AddGuardDownloadMaFileAsync"/>. Covers the full two-step maFile enrollment
/// lifecycle; which fields are required depends on <see cref="Step"/> and how far enrollment has progressed:
/// <list type="bullet">
/// <item>Step 1, first call: <see cref="Username"/> + <see cref="Password"/>.</item>
/// <item>Step 1, after a <c>NEED_EMAIL_CODE</c> response: <see cref="EmailCode"/> + <see cref="LoginSession"/>.</item>
/// <item>Step 2 (finalize): <see cref="ActivationCode"/>, plus either <see cref="MaFile"/> or
/// <see cref="SharedSecret"/> + <see cref="AccessToken"/> from the step 1 response.</item>
/// </list>
/// </summary>
public sealed class AddGuardRequest
{
    /// <summary>The Steam account login name. Required for step 1.</summary>
    [JsonPropertyName("username")]
    public string? Username { get; set; }

    /// <summary>The Steam account password. Required for step 1.</summary>
    [JsonPropertyName("password")]
    public string? Password { get; set; }

    /// <summary>The Steam Guard code sent by email, after a <c>NEED_EMAIL_CODE</c> response. Required for the step 1 follow-up call.</summary>
    [JsonPropertyName("emailcode")]
    public string? EmailCode { get; set; }

    /// <summary>The continuation token returned by a <c>NEED_EMAIL_CODE</c> response. Required for the step 1 follow-up call.</summary>
    [JsonPropertyName("loginsession")]
    public string? LoginSession { get; set; }

    /// <summary>The full maFile from the step 1 response. Alternative to <see cref="SharedSecret"/>/<see cref="AccessToken"/> for step 2.</summary>
    [JsonPropertyName("mafile")]
    public MaFile? MaFile { get; set; }

    /// <summary>The shared secret from the step 1 response's maFile. Used with <see cref="AccessToken"/> as an alternative to <see cref="MaFile"/> for step 2.</summary>
    [JsonPropertyName("sharedsecret")]
    public string? SharedSecret { get; set; }

    /// <summary>The access token from the step 1 response. Used with <see cref="SharedSecret"/> as an alternative to <see cref="MaFile"/> for step 2.</summary>
    [JsonPropertyName("accesstoken")]
    public string? AccessToken { get; set; }

    /// <summary>
    /// The activation code Steam sent via SMS or email for finalization. Required for step 2. (The API also
    /// accepts this under the aliases <c>smscode</c>/<c>emailcode</c>; this field is sent as <c>activationcode</c>.)
    /// </summary>
    [JsonPropertyName("activationcode")]
    public string? ActivationCode { get; set; }

    /// <summary>Forces which step to run, instead of letting the server auto-detect it from the supplied fields.</summary>
    [JsonPropertyName("step")]
    public AddGuardStep? Step { get; set; }

    /// <summary>
    /// When true, the server responds with the maFile as a raw file attachment instead of JSON. Use
    /// <see cref="SteamWebApiClient.AddGuardDownloadMaFileAsync"/> rather than
    /// <see cref="SteamWebApiClient.AddGuardAsync"/> when set.
    /// </summary>
    [JsonPropertyName("mafiledownload")]
    public bool? MaFileDownload { get; set; }
}
