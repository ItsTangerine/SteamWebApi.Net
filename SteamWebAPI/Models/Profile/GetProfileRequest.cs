namespace SteamWebAPI.Models.Profile;

/// <summary>Options for <see cref="SteamWebApiClient.GetProfileAsync"/>.</summary>
public sealed class GetProfileRequest
{
    /// <summary>When true, bypasses steamwebapi.com's cache for this call.</summary>
    public bool? NoCache { get; set; }

    /// <summary>How much profile detail to return. Defaults to <see cref="ProfileState.Minimal"/>.</summary>
    public ProfileState? State { get; set; }

    /// <summary>When true, prefers steamwebapi.com's stored database copy over a fresh Steam lookup, if present.</summary>
    public bool? ForceFromDbIfExists { get; set; }

    /// <summary>When true, includes the profile's Steam group memberships.</summary>
    public bool? WithGroups { get; set; }
}
