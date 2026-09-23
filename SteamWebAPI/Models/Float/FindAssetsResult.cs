using System.Text.Json;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Float;

/// <summary>The ownership timelines returned by <see cref="SteamWebApiClient.FindAssetsAsync"/>.</summary>
public sealed class FindAssetsResult : BaseResponseDto
{
    /// <summary>The request status, e.g. "success".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>The number of entries sent.</summary>
    [JsonPropertyName("count")]
    public int Count { get; set; }

    /// <summary>The number of entries that matched at least one item.</summary>
    [JsonPropertyName("found")]
    public int Found { get; set; }

    /// <summary>The number of entries rejected as invalid.</summary>
    [JsonPropertyName("invalid")]
    public int Invalid { get; set; }

    /// <summary>The credits this call consumed (1 per entry sent).</summary>
    [JsonPropertyName("creditsused")]
    public int CreditsUsed { get; set; }

    /// <summary>One result per entry sent, in the same order.</summary>
    [JsonPropertyName("data")]
    public IReadOnlyList<AssetLookupResult> Data { get; set; } = Array.Empty<AssetLookupResult>();
}

/// <summary>The result for one <see cref="AssetLookup"/>, as listed in <see cref="FindAssetsResult.Data"/>.</summary>
public sealed class AssetLookupResult : BaseResponseDto
{
    /// <summary>The <see cref="AssetLookup.Ref"/> sent with this entry, if any.</summary>
    [JsonPropertyName("ref")]
    public string? Ref { get; set; }

    /// <summary>"found", "not_found" or "invalid".</summary>
    [JsonPropertyName("status")]
    public string Status { get; set; } = string.Empty;

    /// <summary>Why the entry was rejected, when <see cref="Status"/> is "invalid".</summary>
    [JsonPropertyName("error")]
    public string? Error { get; set; }

    /// <summary>The float that was searched for.</summary>
    [JsonPropertyName("float")]
    public string? Float { get; set; }

    /// <summary>The definition index that was searched for.</summary>
    [JsonPropertyName("defindex")]
    public int? DefIndex { get; set; }

    /// <summary>The pattern seed that was searched for.</summary>
    [JsonPropertyName("paintseed")]
    public int? PaintSeed { get; set; }

    /// <summary>The paint index that was filtered on.</summary>
    [JsonPropertyName("paintindex")]
    public int? PaintIndex { get; set; }

    /// <summary>How many physical items matched.</summary>
    [JsonPropertyName("matched")]
    public int? Matched { get; set; }

    /// <summary>Whether more than the 25 returned items matched. Narrow the key to see them.</summary>
    [JsonPropertyName("truncated")]
    public bool? Truncated { get; set; }

    /// <summary>One ownership timeline per matched item, at most 25.</summary>
    [JsonPropertyName("items")]
    public IReadOnlyList<AssetTimeline>? Items { get; set; }
}

/// <summary>The ownership timeline of one physical CS2 item, as listed in <see cref="AssetLookupResult.Items"/>.</summary>
public sealed class AssetTimeline : BaseResponseDto
{
    /// <summary>The stored float value, as a string to keep its full precision.</summary>
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

    /// <summary>The item's exterior/wear category.</summary>
    [JsonPropertyName("wear")]
    public Wear? Wear { get; set; }

    /// <summary>The current owner's SteamID64.</summary>
    [JsonPropertyName("currentsteamid")]
    public string? CurrentSteamId { get; set; }

    /// <summary>The previous owners' SteamID64s.</summary>
    [JsonPropertyName("previoussteamids")]
    public IReadOnlyList<string> PreviousSteamIds { get; set; } = Array.Empty<string>();

    /// <summary>Every owner segment, oldest first.</summary>
    [JsonPropertyName("owners")]
    public IReadOnlyList<AssetOwner> Owners { get; set; } = Array.Empty<AssetOwner>();

    /// <summary>
    /// The market listings between owners, oldest first. steamwebapi.com does not document a listing's shape, so
    /// each is returned as a raw <see cref="JsonElement"/>.
    /// </summary>
    [JsonPropertyName("listings")]
    public IReadOnlyList<JsonElement> Listings { get; set; } = Array.Empty<JsonElement>();
}

/// <summary>One owner segment of an <see cref="AssetTimeline"/>.</summary>
public sealed class AssetOwner : BaseResponseDto
{
    /// <summary>The owner's SteamID64.</summary>
    [JsonPropertyName("steamid")]
    public string SteamId { get; set; } = string.Empty;

    /// <summary>When the item was first seen with this owner, formatted "yyyy-MM-dd HH:mm:ss".</summary>
    [JsonPropertyName("seenat")]
    public string? SeenAt { get; set; }

    /// <summary>The item's asset id while this owner held it.</summary>
    [JsonPropertyName("assetid")]
    public string? AssetId { get; set; }

    /// <summary>Where the sighting came from, when known.</summary>
    [JsonPropertyName("source")]
    public string? Source { get; set; }

    /// <summary>How many times the item was seen with this owner.</summary>
    [JsonPropertyName("sightings")]
    public int? Sightings { get; set; }
}
