using System.Text.Json;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Retrieves all currencies steamwebapi.com can convert prices into, relative to a base currency.
    /// See <c>GET /currency/api/list</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="baseCurrency">The ISO 4217 base currency code. Defaults to "USD".</param>
    /// <param name="source">The rate source. Defaults to "Steam".</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetCurrenciesAsync(
        string? baseCurrency = null,
        string? source = null,
        CancellationToken cancellationToken = default)
    {
        var query = NewQuery();
        SetParam(query, "base", baseCurrency);
        SetParam(query, "source", source);
        query["production"] = "1";

        return await GetAsync<JsonElement>("/currency/api/list", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the exchange rate between two currencies. See <c>GET /currency/api/exchange</c>.
    /// </summary>
    /// <remarks>
    /// steamwebapi.com does not publish a response schema for this endpoint, so the payload is returned as a raw
    /// <see cref="JsonElement"/> rather than a typed model.
    /// </remarks>
    /// <param name="targetCurrency">The ISO 4217 currency code to convert into, e.g. "EUR".</param>
    /// <param name="baseCurrency">The ISO 4217 base currency code. Defaults to "USD".</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<JsonElement>> GetExchangeRateAsync(
        string targetCurrency,
        string? baseCurrency = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(targetCurrency))
            throw new ArgumentException("Target currency must not be null or empty.", nameof(targetCurrency));

        var query = NewQuery();
        query["change"] = targetCurrency;
        SetParam(query, "base", baseCurrency);

        return await GetAsync<JsonElement>("/currency/api/exchange", query, cancellationToken).ConfigureAwait(false);
    }
}
