using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Profile;

/// <summary>
/// The observed trade history of a Steam account, as returned by <see cref="SteamWebApiClient.GetProfileTradesAsync"/>.
/// Reconstructed from inventory sightings of CS2 items, not from Steam trade logs.
/// </summary>
public sealed class ProfileTradesResult : BaseResponseDto
{
    /// <summary>The request status, e.g. "success".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>The account's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>The number of handovers matching the filters, across all pages.</summary>
    [JsonPropertyName("total")]
    public int Total { get; set; }

    /// <summary>The page size used.</summary>
    [JsonPropertyName("limit")]
    public int Limit { get; set; }

    /// <summary>The number of rows skipped.</summary>
    [JsonPropertyName("offset")]
    public int Offset { get; set; }

    /// <summary>The number of rows in <see cref="Data"/>.</summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>The sort order used: "newest" or "oldest".</summary>
    [JsonPropertyName("sort")]
    public string Sort { get; set; } = string.Empty;

    /// <summary>The direction filter used: "all", "in" or "out".</summary>
    [JsonPropertyName("direction")]
    public string Direction { get; set; } = string.Empty;

    /// <summary>The credits this call consumed (1 per started block of 50 returned rows).</summary>
    [JsonPropertyName("creditsused")]
    public int CreditsUsed { get; set; }

    /// <summary>How much of the account's history steamwebapi.com tracks.</summary>
    [JsonPropertyName("coverage")]
    public ProfileTradeCoverage? Coverage { get; set; }

    /// <summary>The real number of counterparts; <see cref="Partners"/> is capped at the strongest 100.</summary>
    [JsonPropertyName("partnerstotal")]
    public int PartnersTotal { get; set; }

    /// <summary>
    /// The full history rolled up per counterpart, strongest first. Always covers the whole history, regardless of
    /// the filters that apply to <see cref="Data"/>.
    /// </summary>
    [JsonPropertyName("partners")]
    public IReadOnlyList<ProfileTradePartner> Partners { get; set; } = Array.Empty<ProfileTradePartner>();

    /// <summary>One row per observed handover matching the filters.</summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<ProfileTrade> Data { get; set; } = Array.Empty<ProfileTrade>();
}

/// <summary>How much of an account's history is tracked, as embedded in <see cref="ProfileTradesResult.Coverage"/>.</summary>
public sealed class ProfileTradeCoverage : BaseResponseDto
{
    /// <summary>The number of the account's items steamwebapi.com tracks.</summary>
    [JsonPropertyName("itemstracked")]
    public int ItemsTracked { get; set; }

    /// <summary>How many of those items ever changed hands.</summary>
    [JsonPropertyName("itemswithtrades")]
    public int ItemsWithTrades { get; set; }

    /// <summary>
    /// Whether the account has more history than one response covers. When true, treat the result as a recent
    /// window rather than the complete record.
    /// </summary>
    [JsonPropertyName("truncated")]
    public bool Truncated { get; set; }
}

/// <summary>One counterpart's rolled-up trade history, as listed in <see cref="ProfileTradesResult.Partners"/>.</summary>
public sealed class ProfileTradePartner : BaseResponseDto
{
    /// <summary>The counterpart's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>All handovers with this counterpart.</summary>
    [JsonPropertyName("trades")]
    public int Trades { get; set; }

    /// <summary>Items the account received from this counterpart.</summary>
    [JsonPropertyName("received")]
    public int Received { get; set; }

    /// <summary>Items the account gave to this counterpart.</summary>
    [JsonPropertyName("sent")]
    public int Sent { get; set; }

    /// <summary>Handovers that went straight from one inventory to the other.</summary>
    [JsonPropertyName("direct")]
    public int Direct { get; set; }

    /// <summary>Handovers that passed through a marketplace listing.</summary>
    [JsonPropertyName("viamarket")]
    public int ViaMarket { get; set; }

    /// <summary>Items that left the account and came back from this counterpart.</summary>
    [JsonPropertyName("roundtrips")]
    public int RoundTrips { get; set; }

    /// <summary>When the first handover was seen, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("firsttrade")]
    public string? FirstTrade { get; set; }

    /// <summary>When the last handover was seen, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("lasttrade")]
    public string? LastTrade { get; set; }

    /// <summary>Who the counterpart is, or <see langword="null"/> when steamwebapi.com doesn't know it.</summary>
    [JsonPropertyName("profile")]
    public CounterpartProfile? Profile { get; set; }
}

/// <summary>
/// One observed item handover, as listed in <see cref="ProfileTradesResult.Data"/>. The handover happened
/// somewhere between <see cref="SenderLastSeenAt"/> and <see cref="SeenAt"/>.
/// </summary>
public sealed class ProfileTrade : BaseResponseDto
{
    /// <summary>"in" when the account received the item, "out" when it gave the item away.</summary>
    [JsonPropertyName("direction")]
    public string Direction { get; set; } = string.Empty;

    /// <summary>The SteamID64 of the other side.</summary>
    [JsonPropertyName("partner")]
    public string Partner { get; set; } = string.Empty;

    /// <summary>When the item was first seen with the receiver, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("seenat")]
    public string? SeenAt { get; set; }

    /// <summary>When the item was last seen with the giver, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("senderlastseenat")]
    public string? SenderLastSeenAt { get; set; }

    /// <summary>The width of the handover window in days (not the age of the trade).</summary>
    [JsonPropertyName("gapdays")]
    public double? GapDays { get; set; }

    /// <summary>Whether the item went straight from one inventory to the other.</summary>
    [JsonPropertyName("direct")]
    public bool Direct { get; set; }

    /// <summary>The marketplace leg the item travelled through, or <see langword="null"/> when <see cref="Direct"/> is true.</summary>
    [JsonPropertyName("market")]
    public ProfileTradeMarket? Market { get; set; }

    /// <summary>Whether the handover was reconstructed from a legacy previous-owner field rather than two sightings.</summary>
    [JsonPropertyName("inferred")]
    public bool Inferred { get; set; }

    /// <summary>Whether the handover is part of an item leaving the account and coming back.</summary>
    [JsonPropertyName("roundtrip")]
    public bool RoundTrip { get; set; }

    /// <summary>The item's asset id.</summary>
    [JsonPropertyName("assetid")]
    public string AssetId { get; set; } = string.Empty;

    /// <summary>The item that changed hands.</summary>
    [JsonPropertyName("item")]
    public ProfileTradeItem? Item { get; set; }
}

/// <summary>The marketplace leg of a <see cref="ProfileTrade"/>.</summary>
public sealed class ProfileTradeMarket : BaseResponseDto
{
    /// <summary>The market ident, e.g. "skinport".</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>The listing price.</summary>
    [JsonPropertyName("price")]
    public double? Price { get; set; }

    /// <summary>A deep link on some markets, a listing id on others, as the marketplace feed supplies it.</summary>
    [JsonPropertyName("link")]
    public string? Link { get; set; }

    /// <summary>When the listing was seen, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("seenat")]
    public string? SeenAt { get; set; }
}

/// <summary>The item of a <see cref="ProfileTrade"/>.</summary>
public sealed class ProfileTradeItem : BaseResponseDto
{
    /// <summary>The item's float value, as a string to keep its full precision.</summary>
    [JsonPropertyName("float")]
    public string? Float { get; set; }

    /// <summary>The weapon/item definition index.</summary>
    [JsonPropertyName("defindex")]
    public int? DefIndex { get; set; }

    /// <summary>The pattern seed.</summary>
    [JsonPropertyName("paintseed")]
    public int? PaintSeed { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int? PaintIndex { get; set; }

    /// <summary>The item's Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string? MarketHashName { get; set; }

    /// <summary>The exterior's display name, e.g. "Field-Tested".</summary>
    [JsonPropertyName("wear")]
    public string? Wear { get; set; }
}
