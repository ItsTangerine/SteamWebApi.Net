namespace SteamWebAPI.Models.Apps;

/// <summary>
/// Filtering, sorting, and cursor-pagination options for <see cref="SteamWebApiClient.GetAppsAsync"/> and
/// <see cref="SteamWebApiClient.GetAppMetricsAsync"/>. Every multi-value filter accepts up to 5 values.
/// </summary>
public sealed class GetAppsRequest
{
    /// <summary>
    /// Restricts results to these app ids (up to 100). On <see cref="SteamWebApiClient.GetAppsAsync"/> a single id
    /// returns one game with its detail fields instead of a list; prefer <see cref="SteamWebApiClient.GetAppAsync"/> for that.
    /// </summary>
    public IReadOnlyList<int>? AppIds { get; set; }

    /// <summary>Games per page, 1–500. Defaults to 100.</summary>
    public int? Limit { get; set; }

    /// <summary>The <c>pagination.nextcursor</c> value from the previous page. Only valid with the same sort, order, and filters.</summary>
    public string? Cursor { get; set; }

    /// <summary>The sort key. Defaults to <see cref="AppSortBy.AppId"/>.</summary>
    public AppSortBy? SortBy { get; set; }

    /// <summary>
    /// When true, sorts descending; when false, ascending. When null, steamwebapi.com sorts ascending for app id,
    /// name, price, and metrics-updated-at, and descending for every other key.
    /// </summary>
    public bool? OrderDescending { get; set; }

    /// <summary>Free-text search (at least 2 characters) over names, developers, publishers, genres, and tags.</summary>
    public string? Search { get; set; }

    /// <summary>Steam genres, e.g. "Action". All must match.</summary>
    public IReadOnlyList<string>? Genres { get; set; }

    /// <summary>Steam user tags, e.g. "FPS", "Roguelike". All must match. See <see cref="SteamWebApiClient.GetAppFiltersAsync"/>.</summary>
    public IReadOnlyList<string>? Tags { get; set; }

    /// <summary>Leaves out games with any of these Steam user tags.</summary>
    public IReadOnlyList<string>? ExcludeTags { get; set; }

    /// <summary>Steam store categories, e.g. "Co-op". All must match.</summary>
    public IReadOnlyList<string>? Categories { get; set; }

    /// <summary>Supported languages (interface, subtitles, or audio), e.g. "German". All must match.</summary>
    public IReadOnlyList<string>? Languages { get; set; }

    /// <summary>Languages with full audio. All must match.</summary>
    public IReadOnlyList<string>? AudioLanguages { get; set; }

    /// <summary>Only games released for this platform.</summary>
    public AppPlatform? Platform { get; set; }

    /// <summary>An exact developer name, e.g. "Valve".</summary>
    public string? Developer { get; set; }

    /// <summary>An exact publisher name.</summary>
    public string? Publisher { get; set; }

    /// <summary>When true, only free-to-play games; when false, only paid games.</summary>
    public bool? IsFree { get; set; }

    /// <summary>The storefront used for price filters, price sorts, and <c>priceupdatedat</c>. Defaults to <see cref="AppCurrency.Usd"/>.</summary>
    public AppCurrency? Currency { get; set; }

    /// <summary>Minimum final price in minor units of <see cref="Currency"/> (1999 = 19.99).</summary>
    public int? MinPrice { get; set; }

    /// <summary>Maximum final price in minor units of <see cref="Currency"/>. Free games are included unless <see cref="IsFree"/> is false.</summary>
    public int? MaxPrice { get; set; }

    /// <summary>When true, only discounted games; when false, only games at full price.</summary>
    public bool? OnSale { get; set; }

    /// <summary>Minimum discount in percent, 0–100.</summary>
    public int? MinDiscount { get; set; }

    /// <summary>Minimum share of positive reviews, 0–100.</summary>
    public double? MinReviewPercent { get; set; }

    /// <summary>Maximum share of positive reviews, 0–100.</summary>
    public double? MaxReviewPercent { get; set; }

    /// <summary>Only games with at least this many reviews.</summary>
    public int? MinReviews { get; set; }

    /// <summary>Only games with at least this many reviews in the last 30 days.</summary>
    public int? MinReviews30d { get; set; }

    /// <summary>Only games with at least this many current players.</summary>
    public int? MinPlayers { get; set; }

    /// <summary>When true, only unreleased games; when false, only released games.</summary>
    public bool? ComingSoon { get; set; }

    /// <summary>Only games released on or after this date.</summary>
    public DateTime? ReleasedAfter { get; set; }

    /// <summary>Only games released on or before this date.</summary>
    public DateTime? ReleasedBefore { get; set; }

    /// <summary>When true, only games with sexual content (by Steam content descriptors); when false, only games without.</summary>
    public bool? Adult { get; set; }

    /// <summary>Only games whose player metrics changed at or after this time (inclusive).</summary>
    public DateTimeOffset? MetricsUpdatedSince { get; set; }

    /// <summary>Only games whose catalog data changed at or after this time. Player samples don't advance it.</summary>
    public DateTimeOffset? UpdatedSince { get; set; }

    /// <summary>When true, bypasses steamwebapi.com's response cache for this call.</summary>
    public bool? NoCache { get; set; }
}
