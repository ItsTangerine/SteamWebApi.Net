using SteamWebAPI.Models.Account;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves the caller's account info and API usage statistics. This call itself generates a usage record.
    /// See <c>GET /account/me</c>.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<AccountInfo>> GetAccountAsync(CancellationToken cancellationToken = default)
    {
        var query = NewQuery();

        return await GetAsync<AccountInfo>("/account/me", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Performs a Steam login (username/password/Guard code, or a refresh token) and returns session cookies for
    /// use with the Trading API. See <c>POST /steam/api/steamloginsecure</c>.
    /// </summary>
    /// <param name="request">
    /// The login credentials. Exactly one authentication mode must be supplied: <see cref="SteamLoginSecureRequest.Username"/>
    /// + <see cref="SteamLoginSecureRequest.Password"/> (optionally with <see cref="SteamLoginSecureRequest.Code"/>),
    /// or <see cref="SteamLoginSecureRequest.SteamRefreshToken"/> alone.
    /// </param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<SteamLoginSecureResult>> SteamLoginSecureAsync(
        SteamLoginSecureRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var hasCredentials = !string.IsNullOrWhiteSpace(request.Username) || !string.IsNullOrWhiteSpace(request.Password);
        var hasRefreshToken = !string.IsNullOrWhiteSpace(request.SteamRefreshToken);

        if (hasCredentials && hasRefreshToken)
            throw new ArgumentException("Specify either username/password or SteamRefreshToken, not both.", nameof(request));
        if (!hasCredentials && !hasRefreshToken)
            throw new ArgumentException("Specify either username/password or SteamRefreshToken.", nameof(request));
        if (hasCredentials && (string.IsNullOrWhiteSpace(request.Username) || string.IsNullOrWhiteSpace(request.Password)))
            throw new ArgumentException("Both Username and Password must be provided together.", nameof(request));

        var query = NewQuery();

        return await PostAsync<SteamLoginSecureResult>("/steam/api/steamloginsecure", query, request, cancellationToken).ConfigureAwait(false);
    }
}
