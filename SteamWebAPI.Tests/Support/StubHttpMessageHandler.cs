using System.Collections.Specialized;
using System.Net;
using System.Text;
using System.Web;

namespace SteamWebAPI.Tests.Support;

/// <summary>
/// An in-memory <see cref="HttpMessageHandler"/> for offline tests: records every request (and its body) and answers
/// with a canned response, so client query building and DTO mapping can be tested without calling steamwebapi.com.
/// </summary>
internal sealed class StubHttpMessageHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, HttpResponseMessage> _respond;

    public StubHttpMessageHandler(Func<HttpRequestMessage, HttpResponseMessage> respond) => _respond = respond;

    public List<HttpRequestMessage> Requests { get; } = new();

    public List<string?> RequestBodies { get; } = new();

    /// <summary>The query string of the most recent request, parsed.</summary>
    public NameValueCollection LastQuery => HttpUtility.ParseQueryString(Requests[^1].RequestUri!.Query);

    /// <summary>The path of the most recent request.</summary>
    public string LastPath => Requests[^1].RequestUri!.AbsolutePath;

    /// <summary>Answers every request with <paramref name="json"/>, optionally adding response headers.</summary>
    public static StubHttpMessageHandler Json(
        string json,
        HttpStatusCode statusCode = HttpStatusCode.OK,
        IDictionary<string, string>? headers = null) =>
        new(_ =>
        {
            var response = new HttpResponseMessage(statusCode)
            {
                Content = new StringContent(json, Encoding.UTF8, "application/json"),
            };
            foreach (var header in headers ?? new Dictionary<string, string>())
                response.Headers.TryAddWithoutValidation(header.Key, header.Value);
            return response;
        });

    /// <summary>Creates a client that sends through this handler with a fake API key.</summary>
    public SteamWebApiClient CreateClient() =>
        new("test-api-key", new HttpClient(this) { BaseAddress = new Uri("https://steamwebapi.test") });

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        Requests.Add(request);
        RequestBodies.Add(request.Content is null ? null : await request.Content.ReadAsStringAsync(cancellationToken));
        return _respond(request);
    }
}
