namespace SteamWebAPI.Models.Screenshot;

/// <summary>
/// The outcome of <see cref="SteamWebApiClient.GetScreenshotAsync"/> or
/// <see cref="SteamWebApiClient.GetScreenshotJobAsync"/>: either the rendered image, or a pending job to retrieve
/// later.
/// </summary>
public sealed class ScreenshotResult
{
    private ScreenshotResult(byte[]? image, string? contentType, string? jobId, string? idempotencyKey, TimeSpan? retryAfter)
    {
        Image = image;
        ContentType = contentType;
        JobId = jobId;
        IdempotencyKey = idempotencyKey;
        RetryAfter = retryAfter;
    }

    /// <summary>
    /// Whether the render is still in progress (HTTP 202). Wait for <see cref="RetryAfter"/>, then call
    /// <see cref="SteamWebApiClient.GetScreenshotJobAsync"/> with <see cref="JobId"/>.
    /// </summary>
    public bool IsPending => Image is null;

    /// <summary>The rendered image bytes (AVIF), or <see langword="null"/> while <see cref="IsPending"/>.</summary>
    public byte[]? Image { get; }

    /// <summary>The image media type, e.g. <c>image/avif</c>, or <see langword="null"/> while <see cref="IsPending"/>.</summary>
    public string? ContentType { get; }

    /// <summary>The job to retrieve while <see cref="IsPending"/>; expires after three hours.</summary>
    public string? JobId { get; }

    /// <summary>The idempotency key the server associated with this render, when it returned one.</summary>
    public string? IdempotencyKey { get; }

    /// <summary>How long to wait before retrieving <see cref="JobId"/>, when the server said.</summary>
    public TimeSpan? RetryAfter { get; }

    internal static ScreenshotResult Ready(byte[] image, string? contentType, string? idempotencyKey) =>
        new(image ?? throw new ArgumentNullException(nameof(image)), contentType, null, idempotencyKey, null);

    internal static ScreenshotResult Pending(string jobId, string? idempotencyKey, TimeSpan? retryAfter) =>
        new(null, null, jobId ?? throw new ArgumentNullException(nameof(jobId)), idempotencyKey, retryAfter);
}
