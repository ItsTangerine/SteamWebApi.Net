namespace SteamWebAPI.Results;

/// <summary>
/// Classifies the kind of failure represented by an <see cref="Error"/>.
/// </summary>
public enum ErrorType
{
    /// <summary>The failure does not fit any other category.</summary>
    Unknown = 0,

    /// <summary>The HTTP transport succeeded but the response represents a transport/protocol-level failure (e.g. an unexpected 5xx status).</summary>
    Http,

    /// <summary>The steamwebapi.com API returned a domain-specific error (e.g. item not found, profile private, trade offer invalid).</summary>
    SteamApi,

    /// <summary>The request was rejected because of missing, invalid, or expired credentials (API key, steamLoginSecure session, Steam Guard secret, etc.).</summary>
    Authentication,

    /// <summary>The request was rejected because of invalid or missing parameters.</summary>
    Validation,

    /// <summary>The request was rejected because a rate limit or usage quota was exceeded.</summary>
    RateLimit,

    /// <summary>The request could not be completed because of a network-level failure (DNS, connection, timeout, etc.).</summary>
    Network,

    /// <summary>The response body could not be deserialized into the expected shape.</summary>
    Deserialization,
}
