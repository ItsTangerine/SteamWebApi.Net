using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Profile;

/// <summary>The envelope returned by <see cref="SteamWebApiClient.GetProfileBatchAsync"/>.</summary>
public sealed class ProfileBatchResult
{
    /// <summary>The batch response payload.</summary>
    [JsonPropertyName("response")]
    public ProfileBatchResponse Response { get; set; } = new();
}

/// <summary>The <c>response</c> payload of a <see cref="ProfileBatchResult"/>.</summary>
public sealed class ProfileBatchResponse
{
    /// <summary>The requested profiles, in no guaranteed order.</summary>
    [JsonPropertyName("players")]
    public IReadOnlyList<SteamProfile> Players { get; set; } = Array.Empty<SteamProfile>();
}
