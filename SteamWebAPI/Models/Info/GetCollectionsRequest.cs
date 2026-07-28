namespace SteamWebAPI.Models.Info;

/// <summary>Options for <see cref="SteamWebApiClient.GetCollectionsAsync"/>.</summary>
public sealed class GetCollectionsRequest
{
    /// <summary>
    /// Restricts the returned JSON to only these field names, reducing payload size. Field names must match
    /// steamwebapi.com's wire names, not C# property names.
    /// </summary>
    public IReadOnlyList<string>? SelectFields { get; set; }

    /// <summary>The maximum number of collections to return. Defaults to 10000.</summary>
    public int? Limit { get; set; }

    /// <summary>The number of collections to skip, for pagination.</summary>
    public int? Offset { get; set; }

    /// <summary>When true, bypasses steamwebapi.com's cache for this call.</summary>
    public bool? NoCache { get; set; }
}
