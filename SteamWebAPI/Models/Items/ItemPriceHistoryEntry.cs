using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Items;

/// <summary>A single daily price point returned by <see cref="SteamWebApiClient.GetItemPriceHistoryAsync"/>.</summary>
public sealed class ItemPriceHistoryEntry : BaseResponseDto
{
    /// <summary>steamwebapi.com's internal id for this price point.</summary>
    [JsonPropertyName("id")]
    public long Id { get; set; }

    /// <summary>The date this price was recorded.</summary>
    [JsonPropertyName("createdat")]
    public DateTimeOffset CreatedAt { get; set; }

    /// <summary>The recorded price.</summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }

    /// <summary>The number of units sold at this price, when known.</summary>
    [JsonPropertyName("sold")]
    public int? Sold { get; set; }
}
