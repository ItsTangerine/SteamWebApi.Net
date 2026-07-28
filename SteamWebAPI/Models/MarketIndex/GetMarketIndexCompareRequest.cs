namespace SteamWebAPI.Models.MarketIndex;

/// <summary>Options for <see cref="SteamWebApiClient.GetMarketIndexCompareAsync"/>.</summary>
public sealed class GetMarketIndexCompareRequest
{
    /// <summary>Restricts the comparison to these segment keys, e.g. "knife", "glove", "rifle". Defaults to all segments of the given type.</summary>
    public IReadOnlyList<string>? Keys { get; set; }

    /// <summary>The metric to rank segments by. Defaults to <see cref="MarketIndexCompareMetric.PriceIndex"/>.</summary>
    public MarketIndexCompareMetric? Metric { get; set; }
}
