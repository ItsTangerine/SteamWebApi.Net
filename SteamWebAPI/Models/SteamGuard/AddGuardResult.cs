using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>
/// The response of <c>POST /steam/api/guard/add</c>, covering every branch of its multi-step, multi-shape lifecycle
/// (the <c>NEED_EMAIL_CODE</c> branch, the <c>AWAITING_FINALIZATION</c>/success branch, and the finalize-confirmation
/// branch). Fields are nullable and populated only by the branch that produced the response; check
/// <see cref="State"/> and which fields are non-null to determine which branch was returned.
/// </summary>
/// <remarks>
/// steamwebapi.com does not publish a formal schema for this response; field names are reconstructed from prose
/// description only and should be verified against a live response. Not used when
/// <see cref="AddGuardRequest.MaFileDownload"/> is <c>true</c> — that branch returns a raw file attachment; use
/// <see cref="SteamWebApiClient.AddGuardDownloadMaFileAsync"/> for it instead.
/// </remarks>
public sealed class AddGuardResult
{
    /// <summary>The enrollment state, e.g. <c>"NEED_EMAIL_CODE"</c> or <c>"AWAITING_FINALIZATION"</c>.</summary>
    [JsonPropertyName("state")]
    public string? State { get; set; }

    /// <summary>A continuation token to pass back as <see cref="AddGuardRequest.LoginSession"/> on the next call. Present on the <c>NEED_EMAIL_CODE</c> branch.</summary>
    [JsonPropertyName("loginsession")]
    public string? LoginSession { get; set; }

    /// <summary>An echo of what should be sent on the next call. Present on the <c>NEED_EMAIL_CODE</c> branch.</summary>
    [JsonPropertyName("nextrequest")]
    public JsonElement? NextRequest { get; set; }

    /// <summary>The generated maFile, with <see cref="Models.SteamGuard.MaFile.FullyEnrolled"/> already <c>true</c>. Present on the step 1 success branch.</summary>
    [JsonPropertyName("mafile")]
    public MaFile? MaFile { get; set; }

    /// <summary>An access token to pass to step 2. Present on the step 1 success branch.</summary>
    [JsonPropertyName("accesstoken")]
    public string? AccessToken { get; set; }

    /// <summary>A stateless <c>data:</c> URI download link for the maFile. Present on the step 1 success branch.</summary>
    [JsonPropertyName("mafiledownloadlink")]
    public string? MaFileDownloadLink { get; set; }

    /// <summary>The suggested filename for the maFile, in the form <c>&lt;steamid&gt;.maFile</c>. Present on the step 1 success branch.</summary>
    [JsonPropertyName("mafilefilename")]
    public string? MaFileFileName { get; set; }

    /// <summary>Whether finalization succeeded. Present on the step 2 (finalize) branch.</summary>
    [JsonPropertyName("success")]
    public bool? Success { get; set; }

    /// <summary>A human-readable status/error message. Present on the step 2 (finalize) branch.</summary>
    [JsonPropertyName("message")]
    public string? Message { get; set; }
}
