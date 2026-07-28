namespace SteamWebAPI.Models.TradeOffers;

/// <summary>Pagination/filter query options for <see cref="SteamWebApiClient.GetTradeHistoryAsync"/>.</summary>
public sealed class GetTradeHistoryOptions
{
    /// <summary>
    /// Pagination cursor, from a prior response's <see cref="TradeHistoryResponse.NextHistoryAfterTimestamp"/>.
    /// Used together with <see cref="AfterTrade"/>.
    /// </summary>
    public string? AfterTime { get; set; }

    /// <summary>
    /// Pagination cursor, from a prior response's <see cref="TradeHistoryResponse.NextHistoryAfterTrade"/>.
    /// Used together with <see cref="AfterTime"/>.
    /// </summary>
    public string? AfterTrade { get; set; }

    /// <summary>Filters to trades involving this asset id (searches <c>assetid</c>, <c>originalassetid</c>, and <c>newassetid</c>).</summary>
    public string? AssetId { get; set; }
}
