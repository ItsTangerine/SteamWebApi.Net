namespace SteamWebAPI.Models.Profile;

/// <summary>Options for <see cref="SteamWebApiClient.GetProfileBatchAsync"/>.</summary>
public sealed class GetProfileBatchRequest
{
    /// <summary>How much profile detail to return. Defaults to <see cref="ProfileState.Minimal"/>.</summary>
    public ProfileState? State { get; set; }

    /// <summary>When true, includes each profile's Steam group memberships.</summary>
    public bool? WithGroups { get; set; }
}
