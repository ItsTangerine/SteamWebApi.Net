using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Profile;

/// <summary>A Steam account's risk assessment, as returned by <see cref="SteamWebApiClient.GetProfileRiskAsync"/>.</summary>
public sealed class ProfileRiskResult : BaseResponseDto
{
    /// <summary>The request status, e.g. "success".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>The credits this call consumed.</summary>
    [JsonPropertyName("creditsused")]
    public int CreditsUsed { get; set; }

    /// <summary>The assessed account's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The overall assessment and its per-type breakdown.</summary>
    [JsonPropertyName("risk")]
    public ProfileRisk Risk { get; set; } = new();
}

/// <summary>
/// The overall risk assessment of a <see cref="ProfileRiskResult"/>. steamwebapi.com may add risk types alongside
/// <see cref="Smurf"/> later; those land in <see cref="BaseResponseDto.AdditionalData"/>.
/// </summary>
public sealed class ProfileRisk : BaseResponseDto
{
    /// <summary>The overall risk score, 0–100.</summary>
    [JsonPropertyName("score")]
    public int Score { get; set; }

    /// <summary>The overall risk level: "low", "medium" or "high".</summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>The secondary ("smurf") account assessment.</summary>
    [JsonPropertyName("smurf")]
    public SmurfRisk? Smurf { get; set; }
}

/// <summary>
/// How likely an account is a secondary ("smurf") account whose inventory was handed over from another account. A
/// probabilistic indicator, not proof of identity.
/// </summary>
public sealed class SmurfRisk : BaseResponseDto
{
    /// <summary>The verdict. <c>false</c> at <see cref="Confidence"/> "low" means "cannot tell", not "clean".</summary>
    [JsonPropertyName("detected")]
    public bool Detected { get; set; }

    /// <summary>The smurf score, 0–100.</summary>
    [JsonPropertyName("score")]
    public int Score { get; set; }

    /// <summary>The smurf level: "low", "medium" or "high". "high" only ever occurs with <see cref="Confidence"/> "high".</summary>
    [JsonPropertyName("level")]
    public string Level { get; set; } = string.Empty;

    /// <summary>How much is known about the account: "low", "medium" or "high".</summary>
    [JsonPropertyName("confidence")]
    public string Confidence { get; set; } = string.Empty;

    /// <summary>
    /// The stable signal codes that fired, e.g. "round_trip_partner", "dominant_destination",
    /// "single_origin_dominant", "bulk_acquisition_window", "limited_account_high_value".
    /// </summary>
    [JsonPropertyName("signals")]
    public IReadOnlyList<string> Signals { get; set; } = Array.Empty<string>();

    /// <summary>The date the pattern starts (yyyy-MM-dd), when one could be established.</summary>
    [JsonPropertyName("since")]
    public string? Since { get; set; }

    /// <summary>A plain-language conclusion suitable for showing to staff.</summary>
    [JsonPropertyName("summary")]
    public string? Summary { get; set; }

    /// <summary>The accounts this one is linked to, strongest first. Empty when nothing links it to anyone.</summary>
    [JsonPropertyName("linkedaccounts")]
    public IReadOnlyList<SmurfLinkedAccount> LinkedAccounts { get; set; } = Array.Empty<SmurfLinkedAccount>();
}

/// <summary>An account linked to the assessed one, as listed in <see cref="SmurfRisk.LinkedAccounts"/>.</summary>
public sealed class SmurfLinkedAccount : BaseResponseDto
{
    /// <summary>The linked account's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>Who the linked account is, or <see langword="null"/> when steamwebapi.com doesn't know it.</summary>
    [JsonPropertyName("profile")]
    public CounterpartProfile? Profile { get; set; }

    /// <summary>How strongly this account is linked, 0–100.</summary>
    [JsonPropertyName("matchscore")]
    public int MatchScore { get; set; }

    /// <summary>The link strength level: "low", "medium" or "high".</summary>
    [JsonPropertyName("matchlevel")]
    public string MatchLevel { get; set; } = string.Empty;

    /// <summary>The link's share as a 0–1 fraction. steamwebapi.com doesn't define it further.</summary>
    [JsonPropertyName("share")]
    public double Share { get; set; }

    /// <summary>The transfer direction: "incoming", "outgoing" or "both".</summary>
    [JsonPropertyName("direction")]
    public string Direction { get; set; } = string.Empty;

    /// <summary>Whether an item left the assessed account and came back from this one.</summary>
    [JsonPropertyName("roundtrip")]
    public bool RoundTrip { get; set; }

    /// <summary>The transfer counts between the two accounts.</summary>
    [JsonPropertyName("transfers")]
    public SmurfTransfers? Transfers { get; set; }

    /// <summary>When the first transfer was seen, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("firsttransfer")]
    public string? FirstTransfer { get; set; }

    /// <summary>When the last transfer was seen, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("lasttransfer")]
    public string? LastTransfer { get; set; }

    /// <summary>The number of days between <see cref="FirstTransfer"/> and <see cref="LastTransfer"/>.</summary>
    [JsonPropertyName("windowdays")]
    public double? WindowDays { get; set; }

    /// <summary>The plain-language evidence behind <see cref="MatchScore"/>, including what argued against it.</summary>
    [JsonPropertyName("reasons")]
    public IReadOnlyList<string> Reasons { get; set; } = Array.Empty<string>();
}

/// <summary>Transfer counts between two accounts, as embedded in <see cref="SmurfLinkedAccount.Transfers"/>.</summary>
public sealed class SmurfTransfers : BaseResponseDto
{
    /// <summary>Items the assessed account received from the linked account.</summary>
    [JsonPropertyName("received")]
    public int Received { get; set; }

    /// <summary>Items the assessed account sent to the linked account.</summary>
    [JsonPropertyName("sent")]
    public int Sent { get; set; }

    /// <summary>All transfers in either direction.</summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>Transfers that went straight from one inventory to the other.</summary>
    [JsonPropertyName("direct")]
    public int Direct { get; set; }

    /// <summary>Transfers that passed through a marketplace listing.</summary>
    [JsonPropertyName("viamarket")]
    public int ViaMarket { get; set; }
}
