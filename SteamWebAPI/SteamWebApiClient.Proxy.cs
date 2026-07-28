using SteamWebAPI.Models.Common;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Fetches a whitelisted URL through steamwebapi.com's generic crawl proxy (no rate limits/blocking from the
    /// proxied site). Only whitelisted URLs may be crawled; contact steamwebapi.com's Discord to whitelist more.
    /// See <c>GET /proxy/api</c>.
    /// </summary>
    /// <remarks>
    /// The response body is the crawled resource's content, re-encoded into <paramref name="format"/>. There is no
    /// fixed schema — for <see cref="OutputFormat.Json"/> the body is usually (but not guaranteed to be) JSON text;
    /// for every other format it is CSV/XML/HTML/text or base64-ish binary content. The raw body is returned as-is
    /// rather than parsed, since it usually isn't JSON at all.
    /// </remarks>
    /// <param name="url">The target URL to crawl. Sent URI-encoded.</param>
    /// <param name="format">The output format to render the crawled content in. Defaults to JSON.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public Task<Result<string>> CrawlAsync(
        string url,
        OutputFormat? format = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL must not be null or empty.", nameof(url));

        var query = NewQuery();
        query["url"] = url;
        SetEnumParam(query, "format", format);

        return GetRawStringAsync("/proxy/api", query, cancellationToken);
    }

    /// <summary>
    /// Same as <see cref="CrawlAsync"/>, but backed by dedicated premium proxy servers for faster responses and
    /// higher rate limits. Requires premium access (contact steamwebapi.com's Discord). Subject to the same
    /// whitelist restriction. See <c>GET /proxy/api/premium</c>.
    /// </summary>
    /// <remarks>
    /// The response body is the crawled resource's content, re-encoded into <paramref name="format"/>. There is no
    /// fixed schema — see the remarks on <see cref="CrawlAsync"/> for details.
    /// </remarks>
    /// <param name="url">The target URL to crawl. Sent URI-encoded.</param>
    /// <param name="format">The output format to render the crawled content in. Defaults to JSON.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public Task<Result<string>> CrawlPremiumAsync(
        string url,
        OutputFormat? format = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("URL must not be null or empty.", nameof(url));

        var query = NewQuery();
        query["url"] = url;
        SetEnumParam(query, "format", format);

        return GetRawStringAsync("/proxy/api/premium", query, cancellationToken);
    }

    /// <summary>Issues a raw GET request and returns the response body as a string, without attempting JSON deserialization.</summary>
    private async Task<Result<string>> GetRawStringAsync(string path, IDictionary<string, string?> query, CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return Result<string>.Failure(Error.Network($"The request to {path} timed out."));
        }
        catch (HttpRequestException ex)
        {
            return Result<string>.Failure(Error.Network($"A network error occurred while calling {path}.", ex));
        }

        using (response)
        {
            var content = await ReadContentSafeAsync(response).ConfigureAwait(false);

            if (!response.IsSuccessStatusCode)
                return Result<string>.Failure(BuildError((int)response.StatusCode, content));

            return Result<string>.Success(content);
        }
    }
}
