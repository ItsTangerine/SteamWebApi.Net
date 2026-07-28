using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Models.Float;

/// <summary>Filtering, sorting, and pagination options for <see cref="SteamWebApiClient.SearchFloatAssetsAsync"/>.</summary>
public sealed class GetFloatAssetsRequest
{
    /// <summary>Restricts results to assets owned by this SteamID64.</summary>
    public string? SteamId { get; set; }

    /// <summary>Restricts results to an exact Steam Market hash name match.</summary>
    public string? MarketHashName { get; set; }

    /// <summary>Restricts results to this weapon/item definition index.</summary>
    public int? DefIndex { get; set; }

    /// <summary>Restricts results to this skin/paint id.</summary>
    public int? PaintIndex { get; set; }

    /// <summary>The minimum float value (inclusive).</summary>
    public double? MinFloat { get; set; }

    /// <summary>The maximum float value (inclusive).</summary>
    public double? MaxFloat { get; set; }

    /// <summary>When set, restricts results to (or excludes) StatTrak™ items.</summary>
    public bool? IsStatTrak { get; set; }

    /// <summary>When set, restricts results to (or excludes) Souvenir items.</summary>
    public bool? IsSouvenir { get; set; }

    /// <summary>Restricts results to this rarity, typically 1-6.</summary>
    public int? Rarity { get; set; }

    /// <summary>Restricts results to this Steam economy quality code.</summary>
    public int? Quality { get; set; }

    /// <summary>Restricts results to this Steam economy origin. Free text; not a closed set.</summary>
    public string? Origin { get; set; }

    /// <summary>Restricts results to this paint seed.</summary>
    public int? PaintSeed { get; set; }

    /// <summary>Restricts results to this exterior/wear category.</summary>
    public Wear? Wear { get; set; }

    /// <summary>
    /// Restricts results to this Doppler-style phase, e.g. "p1".."p4", "ruby", "sapphire", "black-pearl",
    /// "emerald". Free text; not a closed set.
    /// </summary>
    public string? Phase { get; set; }

    /// <summary>Restricts results to this data source, e.g. "inventory", "csfloat", "youpin". Free text; not a closed set.</summary>
    public string? Source { get; set; }

    /// <summary>Restricts results to an exact Steam asset id match.</summary>
    public string? AssetId { get; set; }

    /// <summary>Restricts results to this date, as either <c>YYYY-MM-DD</c> or <c>DD.MM.YYYY</c>.</summary>
    public string? Date { get; set; }

    /// <summary>How to order the results. Defaults to <see cref="FloatAssetSort.Newest"/>.</summary>
    public FloatAssetSort? Sort { get; set; }

    /// <summary>The maximum number of rows to return. Defaults to 10.</summary>
    public int? Limit { get; set; }

    /// <summary>The number of rows to skip, for pagination. Defaults to 0.</summary>
    public int? Offset { get; set; }

    /// <summary>When true, only returns assets that currently have an owning SteamID (inventory items).</summary>
    public bool? OnlySteamId { get; set; }

    /// <summary>When true, only returns assets that are market listings without an owner.</summary>
    public bool? OnlyMarketId { get; set; }

    /// <summary>When true, includes sticker data for each row.</summary>
    public bool? WithStickers { get; set; }

    /// <summary>When true, includes keychain data for each row.</summary>
    public bool? WithKeychains { get; set; }

    /// <summary>When true, adds an <c>item</c> catalog join to each row.</summary>
    public bool? WithItems { get; set; }

    /// <summary>When true, adds a <c>profile</c> Steam profile join to each row.</summary>
    public bool? WithProfiles { get; set; }
}
