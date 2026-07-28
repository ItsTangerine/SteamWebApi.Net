using SteamWebAPI.Models.Explore;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Searches steamwebapi.com's indexed Steam profiles by name, country, inventory worth, VAC status, fame, and
    /// type, with pagination and sorting. Unifies the legacy <c>/random</c>, <c>/last</c>, and <c>/toplist</c>
    /// routes. See <c>GET /explore/api/profile</c>.
    /// </summary>
    /// <param name="request">Filtering, sorting, and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<IReadOnlyList<ExploreProfile>>> GetExploreProfilesAsync(
        GetExploreProfilesRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetExploreProfilesRequest();
        var query = NewQuery();
        SetParam(query, "search", request.Search);
        SetParam(query, "country", request.Country);
        SetParam(query, "worth", request.Worth);
        SetEnumParam(query, "type", request.Type);
        SetParam(query, "limit", request.Limit);
        SetParam(query, "page", request.Page);
        SetFlag(query, "vac", request.Vac);
        SetFlag(query, "fame", request.Fame);

        if (request.OrderByField is { } orderByField)
        {
            var suffix = orderByField == ExploreProfileOrderByField.Random
                ? string.Empty
                : request.OrderByDescending switch { true => "DESC", false => "ASC", null => string.Empty };
            query["order_by"] = EnumWireValue(orderByField) + suffix;
        }

        query["production"] = "1";

        return await GetAsync<IReadOnlyList<ExploreProfile>>("/explore/api/profile", query, cancellationToken).ConfigureAwait(false);
    }
}
