using System.Reflection;
using System.Runtime.Serialization;
using System.Text;
using System.Text.Json;
using SteamWebAPI.Models.Common;
using SteamWebAPI.Results;

namespace SteamWebAPI;

/// <summary>
/// A strongly-typed .NET client for the steamwebapi.com Steam Web API. Every operation is asynchronous, accepts an
/// optional <see cref="CancellationToken"/>, and returns a <see cref="Result"/> or <see cref="Result{T}"/> instead
/// of throwing for expected API failures (validation errors, rate limits, authentication failures, etc.).
/// </summary>
/// <remarks>
/// Endpoints are grouped into partial class files by feature (Items, Info, Currency, Float, Profile, Inventory,
/// Explore, MarketIndex, MarketPrices, Account, SteamGuard, TradeOffers, Proxy). This file contains the shared
/// HTTP/JSON plumbing they all use.
/// </remarks>
public sealed partial class SteamWebApiClient : IDisposable
{
    /// <summary>The default steamwebapi.com base address, used when no <see cref="HttpClient.BaseAddress"/> is set.</summary>
    internal const string DefaultBaseUrl = "https://www.steamwebapi.com";

    private readonly HttpClient _httpClient;
    private readonly string _apiKey;
    private readonly bool _ownsHttpClient;

    /// <summary>The <see cref="JsonSerializerOptions"/> used to serialize requests and deserialize responses.</summary>
    internal static readonly JsonSerializerOptions JsonOptions = new()
    {
        PropertyNameCaseInsensitive = true,
    };

    /// <summary>
    /// Creates a new <see cref="SteamWebApiClient"/>.
    /// </summary>
    /// <param name="apiKey">Your steamwebapi.com API key, from Dashboard → Top-right corner → API Key.</param>
    /// <param name="httpClient">
    /// An optional <see cref="HttpClient"/> to use for all requests. When omitted, the client creates and owns its
    /// own instance (disposed with the client). When provided, the caller retains ownership and ordinary base
    /// address rules apply: if the client has no <see cref="HttpClient.BaseAddress"/> set, it is set to
    /// <c>https://www.steamwebapi.com</c>.
    /// </param>
    public SteamWebApiClient(string apiKey, HttpClient? httpClient = null)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key must not be null or empty.", nameof(apiKey));

        _apiKey = apiKey;

        if (httpClient is null)
        {
            _httpClient = new HttpClient { BaseAddress = new Uri(DefaultBaseUrl) };
            _ownsHttpClient = true;
        }
        else
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress ??= new Uri(DefaultBaseUrl);
            _ownsHttpClient = false;
        }
    }

    /// <summary>Disposes the underlying <see cref="HttpClient"/> if this client created it.</summary>
    public void Dispose()
    {
        if (_ownsHttpClient)
            _httpClient.Dispose();
    }

    // ---- query dictionary construction -------------------------------------------------------

    private Dictionary<string, string?> NewQuery() => new() { ["key"] = _apiKey };

    private static void SetParam(IDictionary<string, string?> query, string key, string? value)
    {
        if (value is not null)
            query[key] = value;
    }

    private static void SetParam(IDictionary<string, string?> query, string key, int? value)
    {
        if (value is not null)
            query[key] = value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    private static void SetParam(IDictionary<string, string?> query, string key, long? value)
    {
        if (value is not null)
            query[key] = value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    private static void SetParam(IDictionary<string, string?> query, string key, double? value)
    {
        if (value is not null)
            query[key] = value.Value.ToString(System.Globalization.CultureInfo.InvariantCulture);
    }

    /// <summary>Sets a boolean query flag using steamwebapi.com's "0"/"1" string convention.</summary>
    private static void SetFlag(IDictionary<string, string?> query, string key, bool? value)
    {
        if (value is not null)
            query[key] = value.Value ? "1" : "0";
    }

    /// <summary>Sets a date-only query parameter formatted as <c>yyyy-MM-dd</c>.</summary>
    private static void SetDateParam(IDictionary<string, string?> query, string key, DateTime? value)
    {
        if (value is not null)
            query[key] = value.Value.ToString("yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
    }

    private static void SetEnumParam<TEnum>(IDictionary<string, string?> query, string key, TEnum? value)
        where TEnum : struct, Enum
    {
        if (value is not null)
            query[key] = EnumWireValue(value.Value);
    }

    private static void SetCsvParam(IDictionary<string, string?> query, string key, IEnumerable<string>? values)
    {
        if (values is null)
            return;
        var joined = string.Join(",", values);
        if (joined.Length > 0)
            query[key] = joined;
    }

    private static void SetCsvEnumParam<TEnum>(IDictionary<string, string?> query, string key, IEnumerable<TEnum>? values)
        where TEnum : struct, Enum
    {
        if (values is null)
            return;
        var joined = string.Join(",", values.Select(EnumWireValue));
        if (joined.Length > 0)
            query[key] = joined;
    }

    private static readonly Dictionary<(Type, Enum), string> EnumWireValueCache = new();

    private static string EnumWireValue<TEnum>(TEnum value) where TEnum : struct, Enum
    {
        var cacheKey = (typeof(TEnum), (Enum)value);
        lock (EnumWireValueCache)
        {
            if (EnumWireValueCache.TryGetValue(cacheKey, out var cached))
                return cached;
        }

        var field = typeof(TEnum).GetField(value.ToString());
        var wireValue = field?.GetCustomAttribute<EnumMemberAttribute>()?.Value ?? value.ToString();

        lock (EnumWireValueCache)
        {
            EnumWireValueCache[cacheKey] = wireValue;
        }

        return wireValue;
    }

    // ---- request execution --------------------------------------------------------------------

    private Uri BuildUri(string path, IDictionary<string, string?>? query)
    {
        var builder = new StringBuilder(path);

        if (query is { Count: > 0 })
        {
            var pairs = new List<string>(query.Count);
            foreach (var kvp in query)
            {
                if (kvp.Value is null)
                    continue;
                pairs.Add($"{Uri.EscapeDataString(kvp.Key)}={Uri.EscapeDataString(kvp.Value)}");
            }

            if (pairs.Count > 0)
                builder.Append('?').Append(string.Join("&", pairs));
        }

        return new Uri(_httpClient.BaseAddress!, builder.ToString());
    }

    private Task<Result<T>> GetAsync<T>(string path, IDictionary<string, string?> query, CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Get, path, query, null, cancellationToken);

    private Task<Result<T>> PostAsync<T>(string path, IDictionary<string, string?> query, object? body, CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Post, path, query, body, cancellationToken);

    private Task<Result<T>> PutAsync<T>(string path, IDictionary<string, string?> query, object? body, CancellationToken cancellationToken) =>
        SendAsync<T>(HttpMethod.Put, path, query, body, cancellationToken);

    private async Task<Result<T>> SendAsync<T>(
        HttpMethod method,
        string path,
        IDictionary<string, string?> query,
        object? jsonBody,
        CancellationToken cancellationToken)
    {
        var uri = BuildUri(path, query);
        using var request = new HttpRequestMessage(method, uri);

        if (jsonBody is not null)
        {
            var json = JsonSerializer.Serialize(jsonBody, jsonBody.GetType(), JsonOptions);
            request.Content = new StringContent(json, Encoding.UTF8, "application/json");
        }

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return Result<T>.Failure(Error.Network($"The request to {path} timed out."));
        }
        catch (HttpRequestException ex)
        {
            return Result<T>.Failure(Error.Network($"A network error occurred while calling {path}.", ex));
        }

        using (response)
        {
            return await ReadResultAsync<T>(response, path).ConfigureAwait(false);
        }
    }

    /// <summary>Issues a raw GET request and returns the response body as bytes, for binary endpoints (e.g. float screenshots).</summary>
    private async Task<Result<byte[]>> GetBinaryAsync(string path, IDictionary<string, string?> query, CancellationToken cancellationToken)
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

    private async Task<Result<T>> ReadResultAsync<T>(HttpResponseMessage response, string path)
    {
        var content = await ReadContentSafeAsync(response).ConfigureAwait(false);

        if (!response.IsSuccessStatusCode)
            return Result<T>.Failure(BuildError((int)response.StatusCode, content));

        try
        {
            var value = JsonSerializer.Deserialize<T>(content, JsonOptions);
            if (value is null)
                return Result<T>.Failure(Error.Deserialization($"The response from {path} deserialized to null.", content));

            return Result<T>.Success(value);
        }
        catch (JsonException ex)
        {
            return Result<T>.Failure(Error.Deserialization($"Failed to deserialize the response from {path}.", content, ex));
        }
    }

    private static async Task<string> ReadContentSafeAsync(HttpResponseMessage response)
    {
        try
        {
            return await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        }
        catch
        {
            return string.Empty;
        }
    }

    private static Error BuildError(int statusCode, string content)
    {
        var message = ExtractErrorMessage(content) ?? $"Request failed with HTTP status code {statusCode}.";

        return statusCode switch
        {
            400 or 421 or 422 => Error.Validation(message, statusCode, content),
            401 or 410 or 413 => Error.Authentication(message, statusCode, content),
            402 or 429 or 430 => Error.RateLimit(message, statusCode, content),
            >= 500 => Error.Http(statusCode, message, content),
            _ => Error.SteamApi(message, statusCode, content),
        };
    }

    private static string? ExtractErrorMessage(string content)
    {
        if (string.IsNullOrWhiteSpace(content))
            return null;

        try
        {
            var error = JsonSerializer.Deserialize<SteamWebApiError>(content, JsonOptions);
            return error?.ErrorMessage;
        }
        catch (JsonException)
        {
            return null;
        }
    }
}