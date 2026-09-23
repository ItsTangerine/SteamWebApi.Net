using System.Globalization;
using System.Text.Json;
using SteamWebAPI.Models.Apps;
using SteamWebAPI.Models.Common;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves a page of the Steam game catalog (games only) with prices in every storefront, genres, tags,
    /// reviews, and player counts. Pass the returned <c>pagination.nextcursor</c> as <see cref="GetAppsRequest.Cursor"/>
    /// to fetch the next page. See <c>GET /steam/api/apps</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="request">Filtering, sorting, and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetAppsAsync(
        GetAppsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildAppsQuery(request ?? new GetAppsRequest());

        return await GetAsync<JsonElement>("/steam/api/apps", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves compact player metrics for catalog games, without catalog metadata, prices, or chart series.
    /// Always returns a <c>data</c> array and a <c>pagination</c> object; pass <c>pagination.nextcursor</c> as
    /// <see cref="GetAppsRequest.Cursor"/> until it is null. See <c>GET /steam/api/apps/metrics</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="request">Filtering, sorting, and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetAppMetricsAsync(
        GetAppsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        var query = BuildAppsQuery(request ?? new GetAppsRequest());

        return await GetAsync<JsonElement>("/steam/api/apps/metrics", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Lists every value the catalog filters accept (genres, tags, categories, languages, platforms, currencies, and
    /// sort keys), with game counts. See <c>GET /steam/api/apps/filters</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="noCache">When true, bypasses steamwebapi.com's response cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetAppFiltersAsync(
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        var query = NewQuery();
        SetFlag(query, "no_cache", noCache);

        return await GetAsync<JsonElement>("/steam/api/apps/filters", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves one Steam game with every catalog field plus the detail-only fields (description, links,
    /// categories, languages, age ratings, DLC and demo app ids, packages). See <c>GET /steam/api/apps/{appid}</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="appId">The game's Steam app id, e.g. 730.</param>
    /// <param name="currency">The storefront used for <c>priceupdatedat</c>. <c>prices</c> always covers every storefront.</param>
    /// <param name="noCache">When true, bypasses steamwebapi.com's response cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetAppAsync(
        int appId,
        AppCurrency? currency = null,
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        if (appId <= 0)
            throw new ArgumentOutOfRangeException(nameof(appId), "App id must be positive.");

        var query = NewQuery();
        SetEnumParam(query, "currency", currency);
        SetFlag(query, "no_cache", noCache);

        return await GetAsync<JsonElement>($"/steam/api/apps/{appId.ToString(CultureInfo.InvariantCulture)}", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the player history of one Steam game: current players, peaks, averages, rank, a chart series for
    /// the requested range, and the monthly table. See <c>GET /steam/api/apps/{appid}/players</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="appId">The game's Steam app id, e.g. 730.</param>
    /// <param name="range">
    /// The chart range. 24h, 7d, and 30d are hourly, 90d and 1y daily, and all monthly. Defaults to
    /// <see cref="PlayerHistoryRange.Days30"/>.
    /// </param>
    /// <param name="noCache">When true, bypasses steamwebapi.com's response cache for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetAppPlayersAsync(
        int appId,
        PlayerHistoryRange? range = null,
        bool? noCache = null,
        CancellationToken cancellationToken = default)
    {
        if (appId <= 0)
            throw new ArgumentOutOfRangeException(nameof(appId), "App id must be positive.");

        var query = NewQuery();
        SetEnumParam(query, "range", range);
        SetFlag(query, "no_cache", noCache);

        return await GetAsync<JsonElement>($"/steam/api/apps/{appId.ToString(CultureInfo.InvariantCulture)}/players", query, cancellationToken).ConfigureAwait(false);
    }

    private Dictionary<string, string?> BuildAppsQuery(GetAppsRequest request)
    {
        var query = NewQuery();
        SetCsvParam(query, "appid", request.AppIds?.Select(id => id.ToString(CultureInfo.InvariantCulture)));
        SetParam(query, "limit", request.Limit);
        SetParam(query, "cursor", request.Cursor);
        SetEnumParam(query, "sort", request.SortBy);
        SetParam(query, "order", request.OrderDescending switch { true => "desc", false => "asc", null => null });
        SetParam(query, "search", request.Search);
        SetCsvParam(query, "genre", request.Genres);
        SetCsvParam(query, "tag", request.Tags);
        SetCsvParam(query, "exclude_tag", request.ExcludeTags);
        SetCsvParam(query, "category", request.Categories);
        SetCsvParam(query, "language", request.Languages);
        SetCsvParam(query, "audio_language", request.AudioLanguages);
        SetEnumParam(query, "platform", request.Platform);
        SetParam(query, "developer", request.Developer);
        SetParam(query, "publisher", request.Publisher);
        SetFlag(query, "is_free", request.IsFree);
        SetEnumParam(query, "currency", request.Currency);
        SetParam(query, "min_price", request.MinPrice);
        SetParam(query, "max_price", request.MaxPrice);
        SetFlag(query, "on_sale", request.OnSale);
        SetParam(query, "min_discount", request.MinDiscount);
        SetParam(query, "min_review_percent", request.MinReviewPercent);
        SetParam(query, "max_review_percent", request.MaxReviewPercent);
        SetParam(query, "min_reviews", request.MinReviews);
        SetParam(query, "min_reviews30d", request.MinReviews30d);
        SetParam(query, "min_players", request.MinPlayers);
        SetFlag(query, "coming_soon", request.ComingSoon);
        SetDateParam(query, "released_after", request.ReleasedAfter);
        SetDateParam(query, "released_before", request.ReleasedBefore);
        SetFlag(query, "adult", request.Adult);
        SetUtcTimestampParam(query, "metrics_updated_since", request.MetricsUpdatedSince);
        SetUtcTimestampParam(query, "updated_since", request.UpdatedSince);
        SetFlag(query, "no_cache", request.NoCache);
        return query;
    }
}
