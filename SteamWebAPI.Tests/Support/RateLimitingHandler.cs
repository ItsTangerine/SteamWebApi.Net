namespace SteamWebAPI.Tests.Support;

/// <summary>
/// Throttles outgoing requests to at most <see cref="TestConfig.MaxRequestsPerMinute"/> per rolling 60-second
/// window, shared statically across every <see cref="SteamWebApiClient"/> the live tests create. steamwebapi.com's
/// lower-tier plans allow only a handful of requests per minute, and xUnit fires tests fast enough to blow through
/// that instantly without this — leading to a cascade of 429s that have nothing to do with the DTOs actually being
/// wrong.
/// </summary>
internal sealed class RateLimitingHandler : DelegatingHandler
{
    private static readonly SemaphoreSlim Gate = new(1, 1);
    private static readonly Queue<DateTimeOffset> RecentRequestTimestamps = new();
    private static readonly TimeSpan Window = TimeSpan.FromSeconds(60);

    public RateLimitingHandler(HttpMessageHandler innerHandler) : base(innerHandler)
    {
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        await ThrottleAsync(cancellationToken).ConfigureAwait(false);
        return await base.SendAsync(request, cancellationToken).ConfigureAwait(false);
    }

    private static async Task ThrottleAsync(CancellationToken cancellationToken)
    {
        while (true)
        {
            TimeSpan waitFor;

            await Gate.WaitAsync(cancellationToken).ConfigureAwait(false);
            try
            {
                var now = DateTimeOffset.UtcNow;

                while (RecentRequestTimestamps.Count > 0 && now - RecentRequestTimestamps.Peek() > Window)
                    RecentRequestTimestamps.Dequeue();

                if (RecentRequestTimestamps.Count < TestConfig.MaxRequestsPerMinute)
                {
                    RecentRequestTimestamps.Enqueue(now);
                    return;
                }

                waitFor = Window - (now - RecentRequestTimestamps.Peek()) + TimeSpan.FromMilliseconds(250);
            }
            finally
            {
                Gate.Release();
            }

            if (waitFor > TimeSpan.Zero)
                await Task.Delay(waitFor, cancellationToken).ConfigureAwait(false);
        }
    }
}
