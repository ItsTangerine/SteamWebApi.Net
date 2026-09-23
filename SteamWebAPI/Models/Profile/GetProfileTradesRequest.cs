namespace SteamWebAPI.Models.Profile;

/// <summary>Options for <see cref="SteamWebApiClient.GetProfileTradesAsync"/>. The filters apply to <see cref="ProfileTradesResult.Data"/> only.</summary>
public sealed class GetProfileTradesRequest
{
    /// <summary>Which handovers to return. Defaults to <see cref="ProfileTradeDirection.All"/>.</summary>
    public ProfileTradeDirection? Direction { get; set; }

    /// <summary>Restricts results to handovers with this counterpart (SteamID64).</summary>
    public string? Partner { get; set; }

    /// <summary>Only handovers first seen on or after this date.</summary>
    public DateTime? From { get; set; }

    /// <summary>Only handovers first seen on or before this date (inclusive).</summary>
    public DateTime? To { get; set; }

    /// <summary>The row order. Defaults to <see cref="ProfileTradeSort.Newest"/>.</summary>
    public ProfileTradeSort? Sort { get; set; }

    /// <summary>Rows per page, 1–200. Defaults to 50. Each started block of 50 returned rows costs 1 credit.</summary>
    public int? Limit { get; set; }

    /// <summary>Rows to skip. Defaults to 0.</summary>
    public int? Offset { get; set; }
}
