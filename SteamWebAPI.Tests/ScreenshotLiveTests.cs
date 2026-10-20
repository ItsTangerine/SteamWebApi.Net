using SteamWebAPI.Models.Float;
using SteamWebAPI.Models.Screenshot;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com screenshot endpoints. These consume the separate screenshot quota (every render,
/// retry, and job retrieval counts), not ordinary credits. The inspect link comes from
/// <see cref="SteamWebApiClient.CreateInspectLinkAsync"/>, as in <see cref="FloatLiveTests"/>. Skips automatically if
/// no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class ScreenshotLiveTests
{
    private const int MaxJobPolls = 4;

    [LiveFact]
    public async Task GetScreenshotAsync_InspectLinkFromCreateInspectLink_ReturnsAvif()
    {
        using var client = TestConfig.CreateClient();

        var createResult = await client.CreateInspectLinkAsync(new CreateInspectLinkRequest
        {
            DefIndex = 507, // Karambit.
            PaintIndex = 418, // Doppler.
            PaintSeed = 2,
            Float = 0.01,
        });
        Assert.True(createResult.IsSuccess, createResult.IsFailure ? createResult.Error.ToString() : null);

        // A sub-960 width with with_float=0 keeps the render small and verifies the boolean wire format is accepted.
        var result = await client.GetScreenshotAsync(createResult.Value.InspectLink, new GetScreenshotRequest
        {
            Width = 512,
            WithFloat = false,
        });

        for (var poll = 0; poll < MaxJobPolls && result.IsSuccess && result.Value.IsPending; poll++)
        {
            var wait = result.Value.RetryAfter ?? TimeSpan.FromSeconds(5);
            await Task.Delay(wait > TimeSpan.FromSeconds(15) ? TimeSpan.FromSeconds(15) : wait);
            result = await client.GetScreenshotJobAsync(result.Value.JobId!, result.Value.IdempotencyKey);
        }

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(result.Value.IsPending, $"Render still pending after {MaxJobPolls} polls (job {result.Value.JobId}).");
        var image = result.Value.Image!;
        Assert.True(image.Length > 12);
        // ISO-BMFF 'ftyp' box at offset 4, with an AVIF major brand.
        Assert.Equal("ftyp", System.Text.Encoding.ASCII.GetString(image, 4, 4));
        Assert.Contains(System.Text.Encoding.ASCII.GetString(image, 8, 4), new[] { "avif", "avis" });
    }

    [LiveFact]
    public async Task GetScreenshotUsageAsync_ReturnsQuota()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.GetScreenshotUsageAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal("success", result.Value.Status);
        Assert.Equal("UTC", result.Value.Timezone);
        Assert.True(result.Value.Usage.Day >= 0);
        Assert.True(result.Value.AdditionalData is null || result.Value.AdditionalData.Count == 0);
    }
}
