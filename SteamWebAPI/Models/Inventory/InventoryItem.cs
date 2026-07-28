using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Inventory;

/// <summary>
/// A single enriched Steam inventory item (pricing, float, stickers, Doppler phase detection), as returned by
/// <see cref="SteamWebApiClient.GetInventoryAsync"/> and <see cref="SteamWebApiClient.GetInventoryBatchAsync"/>
/// when <c>parse=1</c> (the default). Field population varies by the <c>select</c>/<c>group</c>/<c>with_*</c>
/// request options, so every field except identity fields is treated as optional.
/// </summary>
public sealed class InventoryItem
{
    /// <summary>steamwebapi.com's internal short id for this item.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>The canonical Steam Market hash name.</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>The item's display name as shown on the Steam Market.</summary>
    [JsonPropertyName("marketname")]
    public string? MarketName { get; set; }

    /// <summary>A URL-safe slug for this item.</summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>A normalized (lowercased, punctuation-stripped) form of <see cref="MarketHashName"/> used for search.</summary>
    [JsonPropertyName("normalizedname")]
    public string? NormalizedName { get; set; }

    /// <summary>The stacked quantity this row represents (relevant when <c>group=1</c>).</summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    /// <summary>The Steam asset id.</summary>
    [JsonPropertyName("assetid")]
    public string AssetId { get; set; } = string.Empty;

    /// <summary>The Steam economy class id shared by all instances of this item template.</summary>
    [JsonPropertyName("classid")]
    public string? ClassId { get; set; }

    /// <summary>The Steam economy instance id distinguishing variants of the same <see cref="ClassId"/>.</summary>
    [JsonPropertyName("instanceid")]
    public string? InstanceId { get; set; }

    /// <summary>Groups this item with its StatTrak/souvenir/wear siblings.</summary>
    [JsonPropertyName("groupid")]
    public string? GroupId { get; set; }

    /// <summary>The certificate-format Steam inspect URL for this specific item.</summary>
    [JsonPropertyName("inspectlink")]
    public string? InspectLink { get; set; }

    /// <summary>Float, paint seed, phase, sticker, and keychain data for this item.</summary>
    [JsonPropertyName("float")]
    public InventoryFloatInfo? Float { get; set; }

    /// <summary>Disclaimer text about the pricing data. Not a numeric value.</summary>
    [JsonPropertyName("infoprice")]
    public string? InfoPrice { get; set; }

    /// <summary>Steam Market lowest current listing price.</summary>
    [JsonPropertyName("pricelatest")]
    public double? PriceLatest { get; set; }

    /// <summary>Price of the most recent Steam Market sale.</summary>
    [JsonPropertyName("pricelatestsell")]
    public double? PriceLatestSell { get; set; }

    /// <summary>Most recent Steam Market sale price observed in the last 24 hours.</summary>
    [JsonPropertyName("pricelatestsell24h")]
    public double? PriceLatestSell24h { get; set; }

    /// <summary>Most recent Steam Market sale price observed in the last 7 days.</summary>
    [JsonPropertyName("pricelatestsell7d")]
    public double? PriceLatestSell7d { get; set; }

    /// <summary>Most recent Steam Market sale price observed in the last 30 days.</summary>
    [JsonPropertyName("pricelatestsell30d")]
    public double? PriceLatestSell30d { get; set; }

    /// <summary>Most recent Steam Market sale price observed in the last 90 days.</summary>
    [JsonPropertyName("pricelatestsell90d")]
    public double? PriceLatestSell90d { get; set; }

    /// <summary>When the most recent Steam Market sale in <see cref="PriceLatestSell"/> occurred.</summary>
    [JsonPropertyName("lateststeamsellat")]
    [JsonConverter(typeof(SteamDateWrapperConverter))]
    public DateTimeOffset? LatestSteamSellAt { get; set; }

    /// <summary>Median Steam Market sale price (all-time).</summary>
    [JsonPropertyName("pricemedian")]
    public double? PriceMedian { get; set; }

    /// <summary>Median Steam Market sale price over the last 24 hours.</summary>
    [JsonPropertyName("pricemedian24h")]
    public double? PriceMedian24h { get; set; }

    /// <summary>Median Steam Market sale price over the last 7 days.</summary>
    [JsonPropertyName("pricemedian7d")]
    public double? PriceMedian7d { get; set; }

    /// <summary>Median Steam Market sale price over the last 30 days.</summary>
    [JsonPropertyName("pricemedian30d")]
    public double? PriceMedian30d { get; set; }

    /// <summary>Median Steam Market sale price over the last 90 days.</summary>
    [JsonPropertyName("pricemedian90d")]
    public double? PriceMedian90d { get; set; }

    /// <summary>Average Steam Market sale price (all-time).</summary>
    [JsonPropertyName("priceavg")]
    public double? PriceAvg { get; set; }

    /// <summary>Average Steam Market sale price over the last 24 hours.</summary>
    [JsonPropertyName("priceavg24h")]
    public double? PriceAvg24h { get; set; }

    /// <summary>Average Steam Market sale price over the last 7 days.</summary>
    [JsonPropertyName("priceavg7d")]
    public double? PriceAvg7d { get; set; }

    /// <summary>Average Steam Market sale price over the last 30 days.</summary>
    [JsonPropertyName("priceavg30d")]
    public double? PriceAvg30d { get; set; }

    /// <summary>Average Steam Market sale price over the last 90 days.</summary>
    [JsonPropertyName("priceavg90d")]
    public double? PriceAvg90d { get; set; }

    /// <summary>A smoothed price estimate intended for trading/inventory valuation.</summary>
    [JsonPropertyName("pricesafe")]
    public double? PriceSafe { get; set; }

    /// <summary>The lowest Steam Market sale price observed in the window covered by the response.</summary>
    [JsonPropertyName("pricemin")]
    public double? PriceMin { get; set; }

    /// <summary>The highest Steam Market sale price observed in the window covered by the response.</summary>
    [JsonPropertyName("pricemax")]
    public double? PriceMax { get; set; }

    /// <summary>The lowest price across Steam Market and third-party markets combined.</summary>
    [JsonPropertyName("pricemix")]
    public double? PriceMix { get; set; }

    /// <summary>
    /// The lowest price among third-party markets (subject to the <c>markets</c> filter). Phase-adjusted when
    /// <c>with_phase_price=1</c>.
    /// </summary>
    [JsonPropertyName("pricereal")]
    public double? PriceReal { get; set; }

    /// <summary>Lowest third-party market price observed in the last 24 hours. Forced to <see langword="null"/> when <c>with_phase_price=1</c>.</summary>
    [JsonPropertyName("pricereal24h")]
    public double? PriceReal24h { get; set; }

    /// <summary>Lowest third-party market price observed in the last 7 days. Forced to <see langword="null"/> when <c>with_phase_price=1</c>.</summary>
    [JsonPropertyName("pricereal7d")]
    public double? PriceReal7d { get; set; }

    /// <summary>Lowest third-party market price observed in the last 30 days. Forced to <see langword="null"/> when <c>with_phase_price=1</c>.</summary>
    [JsonPropertyName("pricereal30d")]
    public double? PriceReal30d { get; set; }

    /// <summary>Lowest third-party market price observed in the last 90 days. Forced to <see langword="null"/> when <c>with_phase_price=1</c>.</summary>
    [JsonPropertyName("pricereal90d")]
    public double? PriceReal90d { get; set; }

    /// <summary>Median third-party market price. Forced to <see langword="null"/> when <c>with_phase_price=1</c>.</summary>
    [JsonPropertyName("pricerealmedian")]
    public double? PriceRealMedian { get; set; }

    /// <summary>The highest active Steam Market buy-order price.</summary>
    [JsonPropertyName("buyorderprice")]
    public double? BuyOrderPrice { get; set; }

    /// <summary>The median active Steam Market buy-order price.</summary>
    [JsonPropertyName("buyordermedian")]
    public double? BuyOrderMedian { get; set; }

    /// <summary>The average active Steam Market buy-order price.</summary>
    [JsonPropertyName("buyorderavg")]
    public double? BuyOrderAvg { get; set; }

    /// <summary>The total number of open Steam Market buy orders.</summary>
    [JsonPropertyName("buyordervolume")]
    public int? BuyOrderVolume { get; set; }

    /// <summary>The total number of open Steam Market sell listings.</summary>
    [JsonPropertyName("offervolume")]
    public int? OfferVolume { get; set; }

    /// <summary>Units sold on Steam Market so far today.</summary>
    [JsonPropertyName("soldtoday")]
    public int? SoldToday { get; set; }

    /// <summary>Units sold on Steam Market in the last 24 hours.</summary>
    [JsonPropertyName("sold24h")]
    public int? Sold24h { get; set; }

    /// <summary>Units sold on Steam Market in the last 7 days.</summary>
    [JsonPropertyName("sold7d")]
    public int? Sold7d { get; set; }

    /// <summary>Units sold on Steam Market in the last 30 days.</summary>
    [JsonPropertyName("sold30d")]
    public int? Sold30d { get; set; }

    /// <summary>Units sold on Steam Market in the last 90 days.</summary>
    [JsonPropertyName("sold90d")]
    public int? Sold90d { get; set; }

    /// <summary>Total units ever sold on Steam Market.</summary>
    [JsonPropertyName("soldtotal")]
    public int? SoldTotal { get; set; }

    /// <summary>Estimated hours until the next sale, based on recent sale frequency.</summary>
    [JsonPropertyName("hourstosold")]
    public double? HoursToSold { get; set; }

    /// <summary>A market-cap-like score combining price and sale volume.</summary>
    [JsonPropertyName("points")]
    public int? Points { get; set; }

    /// <summary>When Steam Market pricing for this item was last refreshed.</summary>
    [JsonPropertyName("priceupdatedat")]
    [JsonConverter(typeof(SteamDateWrapperConverter))]
    public DateTimeOffset? PriceUpdatedAt { get; set; }

    /// <summary>The custom name tag applied to this specific instance, or <see langword="null"/> if none.</summary>
    [JsonPropertyName("nametag")]
    public string? NameTag { get; set; }

    /// <summary>The item's border display color as a hex string without a leading '#'.</summary>
    [JsonPropertyName("bordercolor")]
    public string? BorderColor { get; set; }

    /// <summary>The item's display color as a hex string without a leading '#'.</summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>The item's quality label, e.g. "StatTrak". Not a numeric code here.</summary>
    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    /// <summary>The item category, e.g. "rifle".</summary>
    [JsonPropertyName("itemgroup")]
    public string? ItemGroup { get; set; }

    /// <summary>The weapon/item type slug, e.g. "Rifle".</summary>
    [JsonPropertyName("itemtype")]
    public string? ItemType { get; set; }

    /// <summary>The Steam economy image URL, replaced with a phase-specific variant when a Doppler phase is detected.</summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;

    /// <summary>Per-market current offers, present only when <c>with_prices=1</c> was requested.</summary>
    [JsonPropertyName("prices")]
    public IReadOnlyList<InventoryMarketPrice>? Prices { get; set; }

    /// <summary>Alternate float/pattern observations for the same skin.</summary>
    [JsonPropertyName("variants")]
    public IReadOnlyList<InventoryVariant>? Variants { get; set; }

    /// <summary>Whether the item can currently be traded.</summary>
    [JsonPropertyName("tradable")]
    public bool Tradable { get; set; }

    /// <summary>
    /// When the item becomes tradable, or <see langword="null"/> if it is immediately tradable. Not exemplified in
    /// steamwebapi.com's documentation source; treated as free text rather than a parsed date.
    /// </summary>
    [JsonPropertyName("tradableafter")]
    public string? TradableAfter { get; set; }
}

/// <summary>Float, paint seed, phase, sticker, and keychain data nested on an <see cref="InventoryItem"/>.</summary>
public sealed class InventoryFloatInfo
{
    /// <summary>The item's float value.</summary>
    [JsonPropertyName("floatvalue")]
    public double? FloatValue { get; set; }

    /// <summary>The paint seed.</summary>
    [JsonPropertyName("paintseed")]
    public int? PaintSeed { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int? PaintIndex { get; set; }

    /// <summary>
    /// The Doppler-style phase, when this skin has phases: "p1".."p4", "ruby", "sapphire", "black-pearl",
    /// "emerald", or <see langword="null"/>. Free text; not a closed set.
    /// </summary>
    [JsonPropertyName("phase")]
    public string? Phase { get; set; }

    /// <summary>The stickers applied to this item.</summary>
    [JsonPropertyName("stickers")]
    public IReadOnlyList<InventoryStickerRef> Stickers { get; set; } = Array.Empty<InventoryStickerRef>();

    /// <summary>The keychains attached to this item.</summary>
    [JsonPropertyName("keychains")]
    public IReadOnlyList<InventoryStickerRef> Keychains { get; set; } = Array.Empty<InventoryStickerRef>();
}

/// <summary>
/// A sticker or keychain reference nested under an <see cref="InventoryItem"/>'s <see cref="InventoryFloatInfo"/>.
/// This is a different shape from the decoded-certificate <c>FloatSticker</c> and the search-endpoint
/// <c>FloatAssetSticker</c>/<c>FloatAssetKeychain</c> shapes.
/// </summary>
public sealed class InventoryStickerRef
{
    /// <summary>The slot this sticker/keychain occupies.</summary>
    [JsonPropertyName("slot")]
    public int Slot { get; set; }

    /// <summary>The sticker/keychain's item id.</summary>
    [JsonPropertyName("stickerId")]
    public int StickerId { get; set; }

    /// <summary>The sticker/keychain's display name.</summary>
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    /// <summary>The sticker/keychain's image URL.</summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}

/// <summary>A single third-party market's current offer for an <see cref="InventoryItem"/>, present only when <c>with_prices=1</c> was requested.</summary>
public sealed class InventoryMarketPrice
{
    /// <summary>The market ident, e.g. "skinport", "buff".</summary>
    [JsonPropertyName("market")]
    public string Market { get; set; } = string.Empty;

    /// <summary>The lowest current listing price on this market.</summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }

    /// <summary>The number of listings available at or near this price.</summary>
    [JsonPropertyName("quantity")]
    public int Quantity { get; set; }

    /// <summary>A direct URL to the listing on this market.</summary>
    [JsonPropertyName("link")]
    public string Link { get; set; } = string.Empty;
}

/// <summary>An alternate float/pattern observation for the same skin as an <see cref="InventoryItem"/>.</summary>
public sealed class InventoryVariant
{
    /// <summary>The float value of this variant.</summary>
    [JsonPropertyName("floatvalue")]
    public double FloatValue { get; set; }

    /// <summary>The price of this variant.</summary>
    [JsonPropertyName("price")]
    public double Price { get; set; }

    /// <summary>The paint seed of this variant.</summary>
    [JsonPropertyName("paintseed")]
    public int PaintSeed { get; set; }

    /// <summary>The image URL of this variant.</summary>
    [JsonPropertyName("image")]
    public string Image { get; set; } = string.Empty;
}
