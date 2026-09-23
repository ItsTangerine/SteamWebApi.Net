using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Items;

/// <summary>
/// Request body for <see cref="SteamWebApiClient.GetItemsAggregatedHistoryAsync"/>. Costs 1 credit per call
/// regardless of how many items are listed. Duplicate entries in <see cref="Entries"/> are counted separately
/// (e.g. listing the same item twice doubles its contribution to <see cref="AggregatedHistoryEntry.Worth"/>).
/// </summary>
public sealed class GetItemsAggregatedHistoryRequest
{
    /// <summary>The market hash names to aggregate. Setting this replaces <see cref="Entries"/>.</summary>
    [Obsolete("Use Entries, which also supports Doppler phases, paint indexes, and counts.")]
    [JsonIgnore]
    public IReadOnlyList<string> Items
    {
        get => Entries.Select(e => e.MarketHashName).ToList();
        set => Entries = value?.Select(name => new AggregatedHistoryItem { MarketHashName = name }).ToList()
                         ?? (IReadOnlyList<AggregatedHistoryItem>)Array.Empty<AggregatedHistoryItem>();
    }

    /// <summary>The items to aggregate. Required; must be non-empty.</summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<AggregatedHistoryItem> Entries { get; set; } = Array.Empty<AggregatedHistoryItem>();

    /// <summary>
    /// The market idents whose lowest positive daily close is used, e.g. "steam", "dmarket", "skinport". "steam" can
    /// be combined with third-party markets. Defaults to "steam".
    /// </summary>
    [JsonIgnore]
    public IReadOnlyList<string>? Markets { get; set; }

    /// <summary>The price basis to use. steamwebapi.com no longer reads this field.</summary>
    [Obsolete("steamwebapi.com no longer reads 'source'. Use Markets instead.")]
    [JsonPropertyName("source")]
    public HistorySource? Source { get; set; }

    /// <summary>The game the items belong to. Defaults to <see cref="ItemsHistoryGame.Cs2"/>.</summary>
    [JsonPropertyName("game")]
    public ItemsHistoryGame? Game { get; set; }

    /// <summary>The earliest date to include. Defaults to the beginning of all available history.</summary>
    [JsonPropertyName("from_date")]
    [JsonConverter(typeof(Converters.DateOnlyStringConverter))]
    public DateTime? FromDate { get; set; }

    /// <summary>The latest date to include. Defaults to the current date.</summary>
    [JsonPropertyName("to_date")]
    [JsonConverter(typeof(Converters.DateOnlyStringConverter))]
    public DateTime? ToDate { get; set; }

    /// <summary>How to pick a price for days without an exact recorded data point.</summary>
    [JsonPropertyName("strategy")]
    public HistoryStrategy? Strategy { get; set; }
}
