using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Items;

/// <summary>Options for <see cref="SteamWebApiClient.GetItemAsync"/>.</summary>
public sealed class GetItemRequest
{
    /// <summary>Converts all prices to this currency. Defaults to USD.</summary>
    public SteamCurrency? Currency { get; set; }

    /// <summary>
    /// When true, also returns every StatTrak/wear/souvenir variant of this item as a group instead of just the
    /// exact match.
    /// </summary>
    public bool? WithGroups { get; set; }

    /// <summary>Restricts third-party pricing to these markets. Defaults to all markets.</summary>
    public IReadOnlyList<Market>? Markets { get; set; }
}
