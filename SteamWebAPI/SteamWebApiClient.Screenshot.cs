using System.Net;
using System.Text.Json;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Screenshot;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    private const string ScreenshotPath = "/steam/api/screenshot";

    /// <summary>
    /// Renders a CS2 item screenshot (AVIF) from an inspect link. Rendering can be asynchronous: when
    /// <see cref="ScreenshotResult.IsPending"/> is true, wait for <see cref="ScreenshotResult.RetryAfter"/> and call
    /// <see cref="GetScreenshotJobAsync"/>. See <c>GET /steam/api/screenshot</c>.
    /// </summary>
    /// <remarks>
    /// Every screenshot request, including job retrievals and retries, consumes the separate screenshot quota (see
    /// <see cref="GetScreenshotUsageAsync"/>) rather than ordinary credits.
    /// </remarks>
    /// <param name="inspectLink">The CS2 inspect link to render. Required.</param>
    /// <param name="request">Rendering options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ScreenshotResult>> GetScreenshotAsync(
        string inspectLink,
        GetScreenshotRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(inspectLink))
            throw new ArgumentException("Inspect link must not be null or empty.", nameof(inspectLink));

        request ??= new GetScreenshotRequest();
        var query = NewQuery();
        query["url"] = inspectLink;
        SetEnumParam(query, "mode", request.Mode);
        SetParam(query, "width", request.Width);
        if (request.Transparent == true)
            query["view"] = "transparent";
        SetParam(query, "background_color", request.BackgroundColor);
        SetParam(query, "background_url", request.BackgroundUrl);
        SetEnumParam(query, "background_vertical_align", request.BackgroundVerticalAlign);
        SetParam(query, "logo_url", request.LogoUrl);
        SetEnumParam(query, "logo_offset_start", request.LogoOffsetStart);
        SetParam(query, "logo_offset_x", request.LogoOffsetX);
        SetParam(query, "logo_offset_y", request.LogoOffsetY);
        SetParam(query, "logo_opacity", request.LogoOpacity);
        SetParam(query, "logo_width", request.LogoWidth);
        SetFlag(query, "with_float", request.WithFloat);
        SetParam(query, "item_name", request.ItemName);
        SetParam(query, "paint_name", request.PaintName);
        SetParam(query, "idempotency_key", request.IdempotencyKey);

        return await SendScreenshotAsync(query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Retrieves the result of a pending screenshot render returned by <see cref="GetScreenshotAsync"/>. The result
    /// may still be pending. Jobs expire three hours after creation. See <c>GET /steam/api/screenshot</c>.
    /// </summary>
    /// <param name="jobId">The <see cref="ScreenshotResult.JobId"/> of the pending render. Required.</param>
    /// <param name="idempotencyKey">The idempotency key used for the original render, if any.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ScreenshotResult>> GetScreenshotJobAsync(
        string jobId,
        string? idempotencyKey = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(jobId))
            throw new ArgumentException("Job id must not be null or empty.", nameof(jobId));

        var query = NewQuery();
        query["job_id"] = jobId;
        SetParam(query, "idempotency_key", idempotencyKey);

        return await SendScreenshotAsync(query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Reads the current screenshot quota and consumption. Day/month windows use UTC calendar boundaries. This call
    /// counts against the screenshot quota too, so load it on demand rather than before every screenshot. See
    /// <c>GET /steam/api/screenshot/usage</c>.
    /// </summary>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<ScreenshotUsage>> GetScreenshotUsageAsync(CancellationToken cancellationToken = default)
    {
        var query = NewQuery();

        return await GetAsync<ScreenshotUsage>("/steam/api/screenshot/usage", query, cancellationToken).ConfigureAwait(false);
    }

    private async Task<Result<ScreenshotResult>> SendScreenshotAsync(IDictionary<string, string?> query, CancellationToken cancellationToken)
    {
        var uri = BuildUri(ScreenshotPath, query);
        using var request = new HttpRequestMessage(HttpMethod.Get, uri);

        HttpResponseMessage response;
        try
        {
            response = await _httpClient.SendAsync(request, cancellationToken).ConfigureAwait(false);
        }
        catch (OperationCanceledException) when (!cancellationToken.IsCancellationRequested)
        {
            return Result<ScreenshotResult>.Failure(Error.Network($"The request to {ScreenshotPath} timed out."));
        }
        catch (HttpRequestException ex)
        {
            return Result<ScreenshotResult>.Failure(Error.Network($"A network error occurred while calling {ScreenshotPath}.", ex));
        }

        using (response)
        {
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await ReadContentSafeAsync(response).ConfigureAwait(false);
                return Result<ScreenshotResult>.Failure(BuildError((int)response.StatusCode, errorContent));
            }

            var headerKey = GetHeaderValue(response, "Idempotency-Key");
            var mediaType = response.Content.Headers.ContentType?.MediaType;

            // 202 (pending) and format=base64 responses are JSON; everything else is the image itself.
            if (response.StatusCode == HttpStatusCode.Accepted ||
                string.Equals(mediaType, "application/json", StringComparison.OrdinalIgnoreCase))
            {
                var content = await ReadContentSafeAsync(response).ConfigureAwait(false);
                return ReadScreenshotJson(content, response, headerKey);
            }

            try
            {
                var bytes = await response.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return Result<ScreenshotResult>.Success(ScreenshotResult.Ready(bytes, mediaType, headerKey));
            }
            catch (Exception ex)
            {
                return Result<ScreenshotResult>.Failure(Error.Network("Failed to read the response body.", ex));
            }
        }
    }

    private static Result<ScreenshotResult> ReadScreenshotJson(string content, HttpResponseMessage response, string? headerKey)
    {
        ScreenshotJsonBody? body;
        try
        {
            body = JsonSerializer.Deserialize<ScreenshotJsonBody>(content, JsonOptions);
        }
        catch (JsonException ex)
        {
            return Result<ScreenshotResult>.Failure(Error.Deserialization($"Failed to deserialize the response from {ScreenshotPath}.", content, ex));
        }

        var idempotencyKey = body?.IdempotencyKey ?? headerKey;

        if (!string.IsNullOrEmpty(body?.JobId))
            return Result<ScreenshotResult>.Success(ScreenshotResult.Pending(body!.JobId!, idempotencyKey, ReadRetryAfter(response)));

        // format=base64: { "status": "success", "image": "data:image/avif;base64,..." }.
        var dataUri = body?.Image;
        var marker = dataUri?.IndexOf(";base64,", StringComparison.Ordinal) ?? -1;
        if (dataUri is not null && dataUri.StartsWith("data:", StringComparison.Ordinal) && marker > 0)
        {
            try
            {
                var bytes = Convert.FromBase64String(dataUri.Substring(marker + ";base64,".Length));
                return Result<ScreenshotResult>.Success(ScreenshotResult.Ready(bytes, dataUri.Substring(5, marker - 5), idempotencyKey));
            }
            catch (FormatException ex)
            {
                return Result<ScreenshotResult>.Failure(Error.Deserialization($"The response from {ScreenshotPath} contained an invalid base64 image.", content, ex));
            }
        }

        return Result<ScreenshotResult>.Failure(Error.Deserialization(
            $"The response from {ScreenshotPath} contained neither an image nor a job id.", content));
    }

    private static TimeSpan? ReadRetryAfter(HttpResponseMessage response)
    {
        var retryAfter = response.Headers.RetryAfter;
        if (retryAfter?.Delta is { } delta)
            return delta;
        if (retryAfter?.Date is { } date)
        {
            var wait = date - DateTimeOffset.UtcNow;
            return wait > TimeSpan.Zero ? wait : TimeSpan.Zero;
        }

        return null;
    }

    /// <summary>The JSON shape of a pending (202) or <c>format=base64</c> screenshot response.</summary>
    private sealed class ScreenshotJsonBody
    {
        [JsonPropertyName("status")]
        public string? Status { get; set; }

        [JsonPropertyName("image")]
        public string? Image { get; set; }

        [JsonPropertyName("job_id")]
        public string? JobId { get; set; }

        [JsonPropertyName("idempotency_key")]
        public string? IdempotencyKey { get; set; }
    }
}
