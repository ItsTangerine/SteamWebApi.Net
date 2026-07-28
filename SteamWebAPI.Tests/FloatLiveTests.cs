using SteamWebAPI.Models.Float;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests;

/// <summary>
/// Calls the real steamwebapi.com API and checks that the response actually deserializes into the library's DTOs.
/// <see cref="SteamWebApiClient.DecodeFloatAsync"/> and <see cref="SteamWebApiClient.GetFloatScreenshotAsync"/> need
/// a real inspect-link certificate; rather than hardcoding one, these tests obtain it by first calling
/// <see cref="SteamWebApiClient.CreateInspectLinkAsync"/> with arbitrary (non-live) item data, which requires no
/// account/session. Skips automatically if no API key is configured — see <see cref="TestConfig"/>.
/// </summary>
public class FloatLiveTests
{
    [LiveFact]
    public async Task SearchFloatAssetsAsync_SmallLimit_ReturnsParsedResult()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.SearchFloatAssetsAsync(new GetFloatAssetsRequest { Limit = 5 });

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.NotNull(result.Value.Data);
    }

    [LiveFact]
    public async Task CreateInspectLinkAsync_KnifeSkinData_ReturnsLinkAndCertificate()
    {
        using var client = TestConfig.CreateClient();

        var result = await client.CreateInspectLinkAsync(SampleInspectLinkRequest());

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(string.IsNullOrWhiteSpace(result.Value.InspectLink));
        Assert.False(string.IsNullOrWhiteSpace(result.Value.Certificate));
        Assert.Equal(507, result.Value.Decoded.DefIndex);
        Assert.Equal(418, result.Value.Decoded.PaintIndex);
    }

    [LiveFact]
    public async Task DecodeFloatAsync_CertificateFromCreateInspectLink_RoundTripsItemData()
    {
        using var client = TestConfig.CreateClient();

        var createResult = await client.CreateInspectLinkAsync(SampleInspectLinkRequest());
        Assert.True(createResult.IsSuccess, createResult.IsFailure ? createResult.Error.ToString() : null);

        var decodeResult = await client.DecodeFloatAsync(certificate: createResult.Value.Certificate);

        Assert.True(decodeResult.IsSuccess, decodeResult.IsFailure ? decodeResult.Error.ToString() : null);
        Assert.Equal(507, decodeResult.Value.DefIndex);
        Assert.Equal(418, decodeResult.Value.PaintIndex);
        Assert.False(string.IsNullOrWhiteSpace(decodeResult.Value.MarketHashName));
    }

    [LiveFact]
    public async Task GetFloatScreenshotAsync_InspectLinkFromCreateInspectLink_ReturnsPngBytes()
    {
        using var client = TestConfig.CreateClient();

        var createResult = await client.CreateInspectLinkAsync(SampleInspectLinkRequest());
        Assert.True(createResult.IsSuccess, createResult.IsFailure ? createResult.Error.ToString() : null);

        var screenshotResult = await client.GetFloatScreenshotAsync(createResult.Value.InspectLink);

        Assert.True(screenshotResult.IsSuccess, screenshotResult.IsFailure ? screenshotResult.Error.ToString() : null);
        Assert.NotEmpty(screenshotResult.Value);
        // PNG file signature: 0x89 'P' 'N' 'G'.
        Assert.Equal((byte)0x89, screenshotResult.Value[0]);
        Assert.Equal((byte)'P', screenshotResult.Value[1]);
        Assert.Equal((byte)'N', screenshotResult.Value[2]);
        Assert.Equal((byte)'G', screenshotResult.Value[3]);
    }

    private static CreateInspectLinkRequest SampleInspectLinkRequest() => new()
    {
        DefIndex = 507, // Karambit.
        PaintIndex = 418, // Doppler.
        PaintSeed = 2,
        Float = 0.01,
    };
}
