using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Items;

/// <summary>
/// Request body for <see cref="SteamWebApiClient.GetItemsAggregatedHistoryAsync"/>. Costs 1 credit per call
/// regardless of how many items are listed. Duplicate market hash names in <see cref="Items"/> are counted
/// separately (e.g. listing the same item twice doubles its contribution to <see cref="AggregatedHistoryEntry.Worth"/>).
/// </summary>
public sealed class GetItemsAggregatedHistoryRequest
{
    /// <summary>The market hash names to aggregate. Required; must be non-empty.</summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<string> Items { get; set; } = Array.Empty<string>();

    /// <summary>The price basis to use. Defaults to <see cref="HistorySource.Steam"/>.</summary>
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
