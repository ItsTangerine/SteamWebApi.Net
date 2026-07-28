namespace SteamWebAPI.Results;

/// <summary>
/// Describes an expected failure returned instead of a value from a <see cref="Result"/> or <see cref="Result{T}"/>.
/// </summary>
public sealed class Error
{
    /// <summary>The category of failure.</summary>
    public ErrorType Type { get; }

    /// <summary>A human-readable description of what went wrong.</summary>
    public string Message { get; }

    /// <summary>The HTTP status code returned by the server, when the error originated from an HTTP response.</summary>
    public int? HttpStatusCode { get; }

    /// <summary>The raw response body returned by the server, when available. Useful for diagnostics.</summary>
    public string? RawResponse { get; }

    /// <summary>The exception that caused this error, when the error originated from a thrown exception (e.g. a network failure).</summary>
    public Exception? Exception { get; }

    /// <summary>Initializes a new <see cref="Error"/>.</summary>
    public Error(ErrorType type, string message, int? httpStatusCode = null, string? rawResponse = null, Exception? exception = null)
    {
        Type = type;
        Message = message ?? throw new ArgumentNullException(nameof(message));
        HttpStatusCode = httpStatusCode;
        RawResponse = rawResponse;
        Exception = exception;
    }

    /// <summary>Creates an <see cref="ErrorType.Http"/> error for an unexpected transport/protocol-level HTTP failure.</summary>
    public static Error Http(int httpStatusCode, string message, string? rawResponse = null) =>
        new(ErrorType.Http, message, httpStatusCode, rawResponse);

    /// <summary>Creates an <see cref="ErrorType.SteamApi"/> error for a domain-specific failure reported by steamwebapi.com or Steam itself.</summary>
    public static Error SteamApi(string message, int? httpStatusCode = null, string? rawResponse = null) =>
        new(ErrorType.SteamApi, message, httpStatusCode, rawResponse);

    /// <summary>Creates an <see cref="ErrorType.Authentication"/> error.</summary>
    public static Error Authentication(string message, int? httpStatusCode = null, string? rawResponse = null) =>
        new(ErrorType.Authentication, message, httpStatusCode, rawResponse);

    /// <summary>Creates an <see cref="ErrorType.Validation"/> error.</summary>
    public static Error Validation(string message, int? httpStatusCode = null, string? rawResponse = null) =>
        new(ErrorType.Validation, message, httpStatusCode, rawResponse);

    /// <summary>Creates an <see cref="ErrorType.RateLimit"/> error.</summary>
    public static Error RateLimit(string message, int? httpStatusCode = null, string? rawResponse = null) =>
        new(ErrorType.RateLimit, message, httpStatusCode, rawResponse);

    /// <summary>Creates an <see cref="ErrorType.Network"/> error, optionally wrapping the underlying exception.</summary>
    public static Error Network(string message, Exception? exception = null) =>
        new(ErrorType.Network, message, exception: exception);

    /// <summary>Creates an <see cref="ErrorType.Deserialization"/> error, optionally wrapping the underlying exception.</summary>
    public static Error Deserialization(string message, string? rawResponse = null, Exception? exception = null) =>
        new(ErrorType.Deserialization, message, rawResponse: rawResponse, exception: exception);

    /// <inheritdoc />
    public override string ToString() =>
        HttpStatusCode is { } code ? $"[{Type}] ({code}) {Message}" : $"[{Type}] {Message}";
}
