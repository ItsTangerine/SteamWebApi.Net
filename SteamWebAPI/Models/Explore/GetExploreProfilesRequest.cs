namespace SteamWebAPI.Models.Explore;

/// <summary>Filtering, sorting, and pagination options for <see cref="SteamWebApiClient.GetExploreProfilesAsync"/>.</summary>
public sealed class GetExploreProfilesRequest
{
    /// <summary>Filters to profiles whose persona/account/display name partially matches this text.</summary>
    public string? Search { get; set; }

    /// <summary>Filters to profiles located in this ISO 3166-1 alpha-2 country.</summary>
    public string? Country { get; set; }

    /// <summary>Filters to profiles with at least this much inventory worth.</summary>
    public double? Worth { get; set; }

    /// <summary>Filters to profiles of this classification.</summary>
    public ExploreProfileType? Type { get; set; }

    /// <summary>The maximum number of profiles to return (1-100). Defaults to 20.</summary>
    public int? Limit { get; set; }

    /// <summary>The page number to return (1-10). Defaults to 1.</summary>
    public int? Page { get; set; }

    /// <summary>When true, restricts results to VAC-banned profiles.</summary>
    public bool? Vac { get; set; }

    /// <summary>When true, restricts results to fame-flagged profiles.</summary>
    public bool? Fame { get; set; }

    /// <summary>The field to sort by. Defaults to <see cref="ExploreProfileOrderByField.Worth"/>.</summary>
    public ExploreProfileOrderByField? OrderByField { get; set; }

    /// <summary>
    /// The sort direction for <see cref="OrderByField"/>: <see langword="true"/> for descending,
    /// <see langword="false"/> for ascending, or <see langword="null"/> for the field's default direction. Ignored
    /// when <see cref="OrderByField"/> is <see cref="ExploreProfileOrderByField.Random"/>, which has no direction.
    /// </summary>
    public bool? OrderByDescending { get; set; }
}
