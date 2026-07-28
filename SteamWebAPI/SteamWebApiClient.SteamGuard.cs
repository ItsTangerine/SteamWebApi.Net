using System.Text;
using System.Text.Json;
using SteamWebAPI.Models.SteamGuard;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Generates the current 5-character Steam Guard TOTP login code from a shared secret. Stateless — the secret
    /// is not persisted server-side. See <c>POST /steam/api/guard/code</c>.
    /// </summary>
    /// <param name="request">The shared secret and optional device identification.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<GuardCode>> GenerateGuardCodeAsync(
        GenerateGuardCodeRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.SharedSecret))
            throw new ArgumentException("Shared secret must not be null or empty.", nameof(request));

        var query = NewQuery();

        return await PostAsync<GuardCode>("/steam/api/guard/code", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Lists pending mobile trade/market confirmations for an account (same as the Steam mobile app's
    /// "Confirmations" tab). See <c>POST /steam/api/guard/confirmations/list</c>.
    /// </summary>
    /// <param name="request">The account's identity secret and session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<GuardConfirmation>>> ListConfirmationsAsync(
        ListConfirmationsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.IdentitySecret))
            throw new ArgumentException("Identity secret must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamId))
            throw new ArgumentException("SteamId must not be null or empty.", nameof(request));

        var query = NewQuery();

        return await PostAsync<IReadOnlyList<GuardConfirmation>>("/steam/api/guard/confirmations/list", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Accepts or denies one or more pending mobile confirmations. Charges 1 credit per confirmation acted on.
    /// See <c>POST /steam/api/guard/confirmations/confirm</c>.
    /// </summary>
    /// <param name="request">
    /// The confirmation(s) to act on. Either <see cref="ConfirmConfirmationsRequest.Confirmations"/> or both
    /// <see cref="ConfirmConfirmationsRequest.ConfId"/> and <see cref="ConfirmConfirmationsRequest.ConfKey"/> must
    /// be populated.
    /// </param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ConfirmationActionResult>>> ConfirmConfirmationsAsync(
        ConfirmConfirmationsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.IdentitySecret))
            throw new ArgumentException("Identity secret must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamId))
            throw new ArgumentException("SteamId must not be null or empty.", nameof(request));

        var hasBatch = request.Confirmations is { Count: > 0 };
        var hasSingle = !string.IsNullOrWhiteSpace(request.ConfId) && !string.IsNullOrWhiteSpace(request.ConfKey);
        if (!hasBatch && !hasSingle)
        {
            throw new ArgumentException(
                "Either Confirmations, or both ConfId and ConfKey, must be provided.", nameof(request));
        }

        var query = NewQuery();

        return await PostAsync<IReadOnlyList<ConfirmationActionResult>>("/steam/api/guard/confirmations/confirm", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Fetches the detail view of a single confirmation (mobileconf/details) to inspect trade contents before
    /// confirming. See <c>POST /steam/api/guard/confirmations/details</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="request">The confirmation to inspect and the account's identity secret and session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetConfirmationDetailsAsync(
        GetConfirmationDetailsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.IdentitySecret))
            throw new ArgumentException("Identity secret must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamId))
            throw new ArgumentException("SteamId must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.ConfId))
            throw new ArgumentException("ConfId must not be null or empty.", nameof(request));

        var query = NewQuery();

        return await PostAsync<JsonElement>("/steam/api/guard/confirmations/details", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// One-shot helper that lists confirmations, filters by type, and acts on all matching ones in a single
    /// request. Charges 1 credit per confirmation acted on. See <c>POST /steam/api/guard/confirmations/confirm-all</c>.
    /// </summary>
    /// <param name="request">The confirmation type filter, action, and the account's identity secret and session.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ConfirmAllConfirmationsResult>> ConfirmAllConfirmationsAsync(
        ConfirmAllConfirmationsRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.IdentitySecret))
            throw new ArgumentException("Identity secret must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamLoginSecure))
            throw new ArgumentException("SteamLoginSecure must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SteamId))
            throw new ArgumentException("SteamId must not be null or empty.", nameof(request));

        var query = NewQuery();

        return await PostAsync<ConfirmAllConfirmationsResult>("/steam/api/guard/confirmations/confirm-all", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Generates or advances enrollment of a Steam mobile authenticator (maFile) — the full 2-step lifecycle
    /// (login/email-code, then SMS/email activation) in one endpoint, with the step auto-detected from the fields
    /// supplied unless <see cref="AddGuardRequest.Step"/> forces it. See <c>POST /steam/api/guard/add</c>.
    /// </summary>
    /// <remarks>
    /// Do not set <see cref="AddGuardRequest.MaFileDownload"/> to <c>true</c> on this overload — that branch
    /// returns a raw file attachment instead of JSON and will fail to deserialize here. Use
    /// <see cref="AddGuardDownloadMaFileAsync"/> instead when a file download is wanted.
    /// </remarks>
    /// <param name="request">The step-appropriate credentials/codes for this call.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<AddGuardResult>> AddGuardAsync(
        AddGuardRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (request.MaFileDownload == true)
        {
            throw new ArgumentException(
                "MaFileDownload cannot be true for AddGuardAsync, which expects a JSON response. " +
                "Use AddGuardDownloadMaFileAsync instead.", nameof(request));
        }

        var query = NewQuery();

        return await PostAsync<AddGuardResult>("/steam/api/guard/add", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Same lifecycle as <see cref="AddGuardAsync"/>, but requests the maFile as a raw file attachment
    /// (<c>&lt;steamid&gt;.maFile</c> content) instead of a JSON body. See <c>POST /steam/api/guard/add</c> with
    /// <c>mafiledownload=true</c>.
    /// </summary>
    /// <param name="request">
    /// The step-appropriate credentials/codes for this call. <see cref="AddGuardRequest.MaFileDownload"/> is forced
    /// to <c>true</c> regardless of the value supplied.
    /// </param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<byte[]>> AddGuardDownloadMaFileAsync(
        AddGuardRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        request.MaFileDownload = true;

        var query = NewQuery();

        return await PostBinaryAsync("/steam/api/guard/add", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Deactivates a mobile authenticator: logs in with the current Guard code derived from
    /// <see cref="RemoveGuardRequest.SharedSecret"/>, then revokes using <see cref="RemoveGuardRequest.RevocationCode"/>.
    /// Reverts the account to email Steam Guard. See <c>POST /steam/api/guard/remove</c>.
    /// </summary>
    /// <param name="request">The account credentials, current shared secret, and revocation code.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<RemoveGuardResult>> RemoveGuardAsync(
        RemoveGuardRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));
        if (string.IsNullOrWhiteSpace(request.Username))
            throw new ArgumentException("Username must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.Password))
            throw new ArgumentException("Password must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.SharedSecret))
            throw new ArgumentException("Shared secret must not be null or empty.", nameof(request));
        if (string.IsNullOrWhiteSpace(request.RevocationCode))
            throw new ArgumentException("Revocation code must not be null or empty.", nameof(request));

        var query = NewQuery();

        return await PostAsync<RemoveGuardResult>("/steam/api/guard/remove", query, request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>Issues a raw POST request and returns the response body as bytes, for binary responses (e.g. maFile file downloads).</summary>
    private async Task<Result<byte[]>> PostBinaryAsync(
        string path,
        IDictionary<string, string?> query,
        object body,
        CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);
        using var request = new HttpRequestMessage(HttpMethod.Post, uri);

        var json = JsonSerializer.Serialize(body, body.GetType(), JsonOptions);
        request.Content = new StringContent(json, Encoding.UTF8, "application/json");

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return Result<byte[]>.Failure(Error.Network($"The request to {path} timed out."));
        }
        catch (HttpRequestException ex)
        {
            return Result<byte[]>.Failure(Error.Network($"A network error occurred while calling {path}.", ex));
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var content = await ReadContentSafeAsync(response).ConfigureAwait(false);
                return Result<byte[]>.Failure(BuildError((int)response.StatusCode, content));
            }

            try
            {
                var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return Result<byte[]>.Success(bytes);
            }
            catch (Exception ex)
            {
                return Result<byte[]>.Failure(Error.Network("Failed to read the response body.", ex));
            }
        }
    }
}
