using System.Text.Json.Serialization;

namespace SteamWebAPI.Models.Float;

/// <summary>Request body for <see cref="SteamWebApiClient.FindAssetsAsync"/>. Costs 1 credit per entry in <see cref="Items"/>.</summary>
public sealed class FindAssetsRequest
{
    /// <summary>The item identities to look up, 1–5000 entries. Results come back in the same order.</summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<AssetLookup> Items { get; set; } = Array.Empty<AssetLookup>();

    /// <summary>
    /// The SteamID64 to treat as the current owner in each timeline. When omitted, the newest owner segment is
    /// treated as current.
    /// </summary>
    [JsonPropertyName("steam_id")]
    public string? SteamId { get; set; }
}

/// <summary>
/// One physical CS2 item to look up in a <see cref="FindAssetsRequest"/>. Provide <see cref="Float"/> alone,
/// <see cref="DefIndex"/> with <see cref="PaintSeed"/>, or any mix of the three; send all three to address exactly
/// one item. Invalid entries don't fail the batch; they come back with <see cref="AssetLookupResult.Status"/> "invalid".
/// </summary>
public sealed class AssetLookup
{
    /// <summary>The float value with at least 6 decimals, matched at the precision supplied.</summary>
    [JsonPropertyName("float")]
    public string? Float { get; set; }

    /// <summary>The weapon/item definition index. Only usable together with <see cref="PaintSeed"/> or <see cref="Float"/>.</summary>
    [JsonPropertyName("def_index")]
    public int? DefIndex { get; set; }

    /// <summary>The pattern seed. Only usable together with <see cref="DefIndex"/> or <see cref="Float"/>.</summary>
    [JsonPropertyName("paint_seed")]
    public int? PaintSeed { get; set; }

    /// <summary>The skin paint index. An extra filter, never a search key on its own.</summary>
    [JsonPropertyName("paint_index")]
    public int? PaintIndex { get; set; }

    /// <summary>An optional caller reference (max 64 characters), echoed back on the result.</summary>
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }
}
