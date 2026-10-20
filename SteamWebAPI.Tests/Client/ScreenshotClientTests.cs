using System.Net;
using System.Net.Http.Headers;
using System.Text;
using SteamWebAPI.Models.Float;
using SteamWebAPI.Models.Screenshot;
using SteamWebAPI.Results;
using SteamWebAPI.Tests.Support;

namespace SteamWebAPI.Tests.Client;

/// <summary>Offline tests for the screenshot endpoints: query building and the 200 / 202 / error response shapes.</summary>
public class ScreenshotClientTests
{
    private const string InspectLink = "steam://run/730//+csgo_econ_action_preview%20C4D4C4DC7DCDE4C";

    // The first bytes of an AVIF file: a 'ftyp' box with the 'avif' brand.
    private static readonly byte[] AvifBytes = { 0, 0, 0, 0x1C, (byte)'f', (byte)'t', (byte)'y', (byte)'p', (byte)'a', (byte)'v', (byte)'i', (byte)'f' };

    [Fact]
    public async Task GetScreenshotAsync_ImageResponse_ReturnsBytes()
    {
        var stub = new StubHttpMessageHandler(_ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.OK) { Content = new ByteArrayContent(AvifBytes) };
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/avif");
            response.Headers.Add("Idempotency-Key", "render-1");
            return response;
        });
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotAsync(InspectLink);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.False(result.Value.IsPending);
        Assert.Equal(AvifBytes, result.Value.Image);
        Assert.Equal("image/avif", result.Value.ContentType);
        Assert.Equal("render-1", result.Value.IdempotencyKey);
        Assert.Null(result.Value.JobId);
        Assert.Equal("/steam/api/screenshot", stub.LastPath);
    }

    [Fact]
    public async Task GetScreenshotAsync_Accepted_ReturnsPendingJob()
    {
        var stub = new StubHttpMessageHandler(_ =>
        {
            var response = new HttpResponseMessage(HttpStatusCode.Accepted)
            {
                Content = new StringContent("""{"status":"pending","job_id":"job_123","idempotency_key":"render-1"}""", Encoding.UTF8, "application/json"),
            };
            response.Headers.RetryAfter = new RetryConditionHeaderValue(TimeSpan.FromSeconds(5));
            return response;
        });
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotAsync(InspectLink);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.True(result.Value.IsPending);
        Assert.Null(result.Value.Image);
        Assert.Equal("job_123", result.Value.JobId);
        Assert.Equal("render-1", result.Value.IdempotencyKey);
        Assert.Equal(TimeSpan.FromSeconds(5), result.Value.RetryAfter);
    }

    [Fact]
    public async Task GetScreenshotAsync_Base64Json_DecodesImage()
    {
        var json = $$"""{"status":"success","image":"data:image/avif;base64,{{Convert.ToBase64String(AvifBytes)}}"}""";
        var stub = StubHttpMessageHandler.Json(json);
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotAsync(InspectLink);

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal(AvifBytes, result.Value.Image);
        Assert.Equal("image/avif", result.Value.ContentType);
    }

    [Fact]
    public async Task GetScreenshotAsync_JsonWithoutImageOrJob_ReturnsDeserializationError()
    {
        var stub = StubHttpMessageHandler.Json("""{"status":"success"}""");
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotAsync(InspectLink);

        Assert.True(result.IsFailure);
        Assert.Equal(ErrorType.Deserialization, result.Error.Type);
    }

    [Fact]
    public async Task GetScreenshotAsync_ErrorBody_UsesMessageField()
    {
        var stub = StubHttpMessageHandler.Json(
            """{"status":"error","code":"invalid_request","message":"Transparent screenshots require mode=front or mode=back."}""",
            HttpStatusCode.UnprocessableEntity);
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotAsync(InspectLink, new GetScreenshotRequest { Transparent = true, Mode = ScreenshotMode.Both });

        Assert.True(result.IsFailure);
        Assert.Equal(ErrorType.Validation, result.Error.Type);
        Assert.Equal("Transparent screenshots require mode=front or mode=back.", result.Error.Message);
        Assert.Equal(422, result.Error.HttpStatusCode);
    }

    [Fact]
    public async Task GetScreenshotAsync_RenderOptions_AreSentWithWireNames()
    {
        var stub = StubHttpMessageHandler.Json("""{"status":"pending","job_id":"j"}""", HttpStatusCode.Accepted);
        using var client = stub.CreateClient();

        await client.GetScreenshotAsync(InspectLink, new GetScreenshotRequest
        {
            Mode = ScreenshotMode.Both,
            Width = 800,
            BackgroundColor = "#112233",
            BackgroundVerticalAlign = ScreenshotVerticalAlign.Center,
            LogoUrl = "https://example.test/logo.png",
            LogoOffsetStart = FloatScreenshotLogoOffset.BottomRight,
            LogoOffsetX = 10,
            LogoOffsetY = 20,
            LogoOpacity = 0.5,
            LogoWidth = 300,
            WithFloat = false,
            ItemName = "My Knife",
            PaintName = "My Paint",
            IdempotencyKey = "render-1",
        });

        var query = stub.LastQuery;
        Assert.Equal(InspectLink, query["url"]);
        Assert.Equal("both", query["mode"]);
        Assert.Equal("800", query["width"]);
        Assert.Equal("#112233", query["background_color"]);
        Assert.Equal("center", query["background_vertical_align"]);
        Assert.Equal("https://example.test/logo.png", query["logo_url"]);
        Assert.Equal("bottom right", query["logo_offset_start"]);
        Assert.Equal("10", query["logo_offset_x"]);
        Assert.Equal("20", query["logo_offset_y"]);
        Assert.Equal("0.5", query["logo_opacity"]);
        Assert.Equal("300", query["logo_width"]);
        Assert.Equal("0", query["with_float"]);
        Assert.Equal("My Knife", query["item_name"]);
        Assert.Equal("My Paint", query["paint_name"]);
        Assert.Equal("render-1", query["idempotency_key"]);
        Assert.Equal("test-api-key", query["key"]);
        Assert.Null(query["view"]);
    }

    [Fact]
    public async Task GetScreenshotAsync_Transparent_SendsView()
    {
        var stub = StubHttpMessageHandler.Json("""{"status":"pending","job_id":"j"}""", HttpStatusCode.Accepted);
        using var client = stub.CreateClient();

        await client.GetScreenshotAsync(InspectLink, new GetScreenshotRequest { Transparent = true });

        Assert.Equal("transparent", stub.LastQuery["view"]);
    }

    [Fact]
    public async Task GetScreenshotAsync_EmptyInspectLink_Throws()
    {
        using var client = StubHttpMessageHandler.Json("{}").CreateClient();

        await Assert.ThrowsAsync<ArgumentException>(() => client.GetScreenshotAsync(" "));
    }

    [Fact]
    public async Task GetScreenshotJobAsync_SendsOnlyJobIdAndKey()
    {
        var stub = StubHttpMessageHandler.Json("""{"status":"pending","job_id":"job_123"}""", HttpStatusCode.Accepted);
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotJobAsync("job_123", "render-1");

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal("/steam/api/screenshot", stub.LastPath);
        Assert.Equal(new[] { "idempotency_key", "job_id", "key" }, stub.LastQuery.AllKeys.OrderBy(k => k));
        Assert.Equal("job_123", stub.LastQuery["job_id"]);
        Assert.Equal("render-1", stub.LastQuery["idempotency_key"]);
    }

    [Fact]
    public async Task GetScreenshotUsageAsync_MapsQuotaAndUnlimitedLimits()
    {
        var stub = StubHttpMessageHandler.Json(
            """
            {"status":"success","active":true,"limits":{"minute":100,"day":1000,"month":null},
             "usage":{"minute":1,"day":25,"month":240,"total":500,"blocked":0},
             "timezone":"UTC","period":"calendar","unit":"authenticated_requests"}
            """);
        using var client = stub.CreateClient();

        var result = await client.GetScreenshotUsageAsync();

        Assert.True(result.IsSuccess, result.IsFailure ? result.Error.ToString() : null);
        Assert.Equal("/steam/api/screenshot/usage", stub.LastPath);
        Assert.True(result.Value.Active);
        Assert.Equal(100, result.Value.Limits.Minute);
        Assert.Equal(1000, result.Value.Limits.Day);
        Assert.Null(result.Value.Limits.Month);
        Assert.Equal(25, result.Value.Usage.Day);
        Assert.Equal(500, result.Value.Usage.Total);
        Assert.Equal(0, result.Value.Usage.Blocked);
        Assert.Equal("UTC", result.Value.Timezone);
        Assert.Equal("calendar", result.Value.Period);
        Assert.Equal("authenticated_requests", result.Value.Unit);
    }
}
