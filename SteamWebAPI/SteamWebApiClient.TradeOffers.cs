using SteamWebAPI.Models.TradeOffers;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Creates a new trade offer to a partner via an authenticated <c>steamLoginSecure</c> session.
    /// See <c>POST /steam/api/trade/create</c>.
    /// </summary>
    /// <param name="request">The partner, items, and message for the new offer.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<CreateTradeOfferResult>> CreateTradeOfferAsync(
        CreateTradeOfferRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.TradeLink))
            throw new ArgumentException("TradeLink must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.PartnerSteamId))
            throw new ArgumentException("PartnerSteamId must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.Message))
            throw new ArgumentException("Message must not be null or empty.", nameof(request));

        var query = NewQuery();
        query["production"] = "1";

        return await PostAsync<CreateTradeOfferResult>("/steam/api/trade/create", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Accepts a trade offer. May require a mobile authenticator confirmation when items are involved.
    /// See <c>PUT /steam/api/trade/accept</c>.
    /// </summary>
    /// <param name="request">The offer to accept and the accepting account's session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<AcceptTradeOfferResult>> AcceptTradeOfferAsync(
        AcceptTradeOfferRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.TradeOfferId))
            throw new ArgumentException("TradeOfferId must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.PartnerSteamId))
            throw new ArgumentException("PartnerSteamId must not be null or empty.", nameof(request));

        var query = NewQuery();
        query["production"] = "1";

        return await PutAsync<AcceptTradeOfferResult>("/steam/api/trade/accept", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Checks the status/details of a trade offer. Recipient-only — <c>steamLoginSecure</c> must belong to the
    /// offer's recipient, not its sender. See <c>POST /steam/api/trade/check</c>.
    /// </summary>
    /// <param name="request">The offer to check and the recipient's session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<TradeOfferCheckResult>> CheckTradeOfferAsync(
        CheckTradeOfferRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.TradeOfferId))
            throw new ArgumentException("TradeOfferId must not be null or empty.", nameof(request));

        var query = NewQuery();
        query["production"] = "1";

        return await PostAsync<TradeOfferCheckResult>("/steam/api/trade/check", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves a page of an account's trade history via Steam's official <c>IEconService/GetTradeHistory</c>.
    /// See <c>POST /steam/api/trade/history</c>.
    /// </summary>
    /// <param name="steamLoginSecure">The account's <c>steamLoginSecure</c> cookie value.</param>
    /// <param name="options">Pagination and asset-id filter options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<TradeHistoryResponse>> GetTradeHistoryAsync(
        string steamLoginSecure,
        GetTradeHistoryOptions? options = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(steamLoginSecure));

        options ??= new GetTradeHistoryOptions();
        var query = NewQuery();
        SetParam(query, "after_time", options.AfterTime);
        SetParam(query, "after_trade", options.AfterTrade);
        SetParam(query, "assetid", options.AssetId);
        query["production"] = "1";

        var body = new TradeSessionRequest { SteamLoginSecure = steamLoginSecure };

        return await PostAsync<TradeHistoryResponse>("/steam/api/trade/history", query, body, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Lists all outgoing (active) trade offers for an account. See <c>POST /steam/api/trade/sent</c>.</summary>
    /// <param name="steamLoginSecure">The account's <c>steamLoginSecure</c> cookie value.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<TradeOfferSummary>>> GetSentTradeOffersAsync(
        string steamLoginSecure,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(steamLoginSecure));

        var query = NewQuery();
        query["production"] = "1";

        var body = new TradeSessionRequest { SteamLoginSecure = steamLoginSecure };

        return await PostAsync<IReadOnlyList<TradeOfferSummary>>("/steam/api/trade/sent", query, body, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Lists all incoming trade offers for an account. See <c>POST /steam/api/trade/pending</c>.</summary>
    /// <param name="steamLoginSecure">The account's <c>steamLoginSecure</c> cookie value.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<TradeOfferSummary>>> GetPendingTradeOffersAsync(
        string steamLoginSecure,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(steamLoginSecure));

        var query = NewQuery();
        query["production"] = "1";

        var body = new TradeSessionRequest { SteamLoginSecure = steamLoginSecure };

        return await PostAsync<IReadOnlyList<TradeOfferSummary>>("/steam/api/trade/pending", query, body, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Lists historical (completed/expired/cancelled/declined) outgoing trade offers for an account. For active
    /// sent offers use <see cref="GetSentTradeOffersAsync"/> instead. See <c>POST /steam/api/trade/sent/history</c>.
    /// </summary>
    /// <param name="steamLoginSecure">The account's <c>steamLoginSecure</c> cookie value.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<TradeOfferSummary>>> GetSentTradeOfferHistoryAsync(
        string steamLoginSecure,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(steamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(steamLoginSecure));

        var query = NewQuery();
        query["production"] = "1";

        var body = new TradeSessionRequest { SteamLoginSecure = steamLoginSecure };

        return await PostAsync<IReadOnlyList<TradeOfferSummary>>("/steam/api/trade/sent/history", query, body, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Cancels a trade offer (sender-initiated). See <c>PUT /steam/api/trade/cancel</c>.</summary>
    /// <param name="request">The offer to cancel and the sender's session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<TradeOfferActionResult>> CancelTradeOfferAsync(
        TradeOfferActionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.TradeOfferId))
            throw new ArgumentException("TradeOfferId must not be null or empty.", nameof(request));

        var query = NewQuery();
        query["production"] = "1";

        return await PutAsync<TradeOfferActionResult>("/steam/api/trade/cancel", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Declines a trade offer (recipient-initiated). See <c>PUT /steam/api/trade/decline</c>.</summary>
    /// <param name="request">The offer to decline and the recipient's session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<TradeOfferActionResult>> DeclineTradeOfferAsync(
        TradeOfferActionRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.TradeOfferId))
            throw new ArgumentException("TradeOfferId must not be null or empty.", nameof(request));

        var query = NewQuery();
        query["production"] = "1";

        return await PutAsync<TradeOfferActionResult>("/steam/api/trade/decline", query, request, cancellationToken).ConfigureAwait(false);
    }
}
