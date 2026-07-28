using System.Net;
using Microsoft.Extensions.DependencyInjection;

namespace SteamWebAPI.Extensions;

/// <summary>Dependency injection registration for <see cref="SteamWebApiClient"/>.</summary>
public static class ServiceCollectionExtensions
{
    private const string HttpClientName = "SteamWebAPI";

    /// <summary>
    /// Registers <see cref="SteamWebApiClient"/> as a typed <see cref="IHttpClientFactory"/> client (transient,
    /// with pooled/rotated handlers managed by the factory).
    /// </summary>
    /// <param name="services">The service collection to add the client to.</param>
    /// <param name="apiKey">Your steamwebapi.com API key.</param>
    /// <param name="configureClient">Optional callback to further configure the underlying <see cref="HttpClient"/> (timeout, extra headers, etc.).</param>
    public static IServiceCollection AddSteamWebAPI(
        this IServiceCollection services,
        string apiKey,
        Action<HttpClient>? configureClient = null)
    {
        if (string.IsNullOrWhiteSpace(apiKey))
            throw new ArgumentException("API key must not be null or empty.", nameof(apiKey));

        services.AddHttpClient<SteamWebApiClient>(HttpClientName, client =>
            {
                client.BaseAddress = new Uri(SteamWebApiClient.DefaultBaseUrl);
                configureClient?.Invoke(client);
            })
            .ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate,
            })
            .AddTypedClient((httpClient, _) => new SteamWebApiClient(apiKey, httpClient));

        return services;
    }
}
