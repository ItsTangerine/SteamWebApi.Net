using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>
/// A fully priced Steam Market item, as returned by <c>GET /steam/api/items</c> and <c>GET /steam/api/item</c>.
/// Only CS2 populates every field; Rust/Dota 2/TF2 items populate a smaller subset (identity and basic Steam pricing
/// fields only) and leave the CS2-specific fields (wear, float, variants, real-market pricing, tag1-tag7) null.
/// </summary>
public sealed class SteamItem : BaseResponseDto
{
    /// <summary>steamwebapi.com's internal short id for this item.</summary>
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    /// <summary>The canonical Steam Market hash name, e.g. "AK-47 | Redline (Field-Tested)".</summary>
    [JsonPropertyName("markethashname")]
    public string MarketHashName { get; set; } = string.Empty;

    /// <summary>A normalized (lowercased, punctuation-stripped) form of <see cref="MarketHashName"/> used for search.</summary>
    [JsonPropertyName("normalizedname")]
    public string? NormalizedName { get; set; }

    /// <summary>The item's display name as shown on the Steam Market.</summary>
    [JsonPropertyName("marketname")]
    public string? MarketName { get; set; }

    /// <summary>A URL-safe slug for this item.</summary>
    [JsonPropertyName("slug")]
    public string? Slug { get; set; }

    /// <summary>The quantity this row represents (relevant when items are grouped).</summary>
    [JsonPropertyName("count")]
    public int? Count { get; set; }

    /// <summary>The Steam economy class id shared by all instances of this item template.</summary>
    [JsonPropertyName("classid")]
    public string? ClassId { get; set; }

    /// <summary>The Steam economy instance id distinguishing variants of the same <see cref="ClassId"/>.</summary>
    [JsonPropertyName("instanceid")]
    public string? InstanceId { get; set; }

    /// <summary>Groups this item with its StatTrak/souvenir/wear siblings.</summary>
    [JsonPropertyName("groupid")]
    public string? GroupId { get; set; }

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

    /// <summary>Up to the last 10 daily Steam Market sale aggregates.</summary>
    [JsonPropertyName("latest10steamsales")]
    public IReadOnlyList<SteamSale>? Latest10SteamSales { get; set; }

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

    /// <summary>A smoothed price estimate intended for trading/inventory valuation, less sensitive to outlier sales than <see cref="PriceLatest"/>.</summary>
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
    public long? Points { get; set; }

    /// <summary>When Steam Market pricing for this item was last refreshed.</summary>
    [JsonPropertyName("priceupdatedat")]
    [JsonConverter(typeof(SteamDateWrapperConverter))]
    public DateTimeOffset? PriceUpdatedAt { get; set; }

    /// <summary>The custom name tag applied to this specific instance, if any.</summary>
    [JsonPropertyName("nametag")]
    public string? NameTag { get; set; }

    /// <summary>The item's border display color as a hex string without a leading '#'.</summary>
    [JsonPropertyName("bordercolor")]
    public string? BorderColor { get; set; }

    /// <summary>The item's display color as a hex string without a leading '#'.</summary>
    [JsonPropertyName("color")]
    public string? Color { get; set; }

    /// <summary>The item's quality label, e.g. "Normal", "StatTrak™".</summary>
    [JsonPropertyName("quality")]
    public string? Quality { get; set; }

    /// <summary>The item's rarity label, e.g. "Covert", "Base Grade".</summary>
    [JsonPropertyName("rarity")]
    public string? Rarity { get; set; }

    /// <summary>The Steam economy image URL.</summary>
    [JsonPropertyName("image")]
    public string? Image { get; set; }

    /// <summary>Whether the item can currently be listed on the Steam Market.</summary>
    [JsonPropertyName("marketable")]
    public bool? Marketable { get; set; }

    /// <summary>Whether the item can currently be traded.</summary>
    [JsonPropertyName("tradable")]
    public bool? Tradable { get; set; }

    /// <summary>Whether steamwebapi.com considers this item's pricing data unreliable.</summary>
    [JsonPropertyName("unstable")]
    public bool? Unstable { get; set; }

    /// <summary>Explains why <see cref="Unstable"/> is true, when applicable.</summary>
    [JsonPropertyName("unstablereason")]
    public string? UnstableReason { get; set; }

    /// <summary>The item's full Steam economy tag list.</summary>
    [JsonPropertyName("tags")]
    public IReadOnlyList<TagItem>? Tags { get; set; }

    /// <summary>The item's Steam economy description entries (including flavor text and StatTrak/HTML markup).</summary>
    [JsonPropertyName("descriptions")]
    public IReadOnlyList<DescriptionItem>? Descriptions { get; set; }

    /// <summary>When steamwebapi.com first indexed this item.</summary>
    [JsonPropertyName("createdat")]
    [JsonConverter(typeof(SteamDateWrapperConverter))]
    public DateTimeOffset? CreatedAt { get; set; }

    /// <summary>The earliest Unix timestamp (seconds) this item was seen listed anywhere.</summary>
    [JsonPropertyName("firstseentime")]
    public long? FirstSeenTime { get; set; }

    /// <summary>The earliest date/time this item was seen listed anywhere.</summary>
    [JsonPropertyName("firstseenat")]
    public DateTimeOffset? FirstSeenAt { get; set; }

    /// <summary>The direct Steam Community Market listing URL for this item.</summary>
    [JsonPropertyName("steamurl")]
    public string? SteamUrl { get; set; }

    /// <summary>The number of days a freshly-traded copy of this item must wait before it can be listed on the Steam Market.</summary>
    [JsonPropertyName("markettradablerestriction")]
    public int? MarketTradableRestriction { get; set; }

    /// <summary>Positional copy of the item's first tag name (typically item type, e.g. "Knife").</summary>
    [JsonPropertyName("tag1")]
    public string? Tag1 { get; set; }

    /// <summary>Positional copy of the item's second tag name (typically weapon, e.g. "Karambit").</summary>
    [JsonPropertyName("tag2")]
    public string? Tag2 { get; set; }

    /// <summary>Positional copy of the item's third tag name (typically skin name, e.g. "Doppler").</summary>
    [JsonPropertyName("tag3")]
    public string? Tag3 { get; set; }

    /// <summary>Positional copy of the item's fourth tag name (typically StatTrak/Souvenir quality).</summary>
    [JsonPropertyName("tag4")]
    public string? Tag4 { get; set; }

    /// <summary>Positional copy of the item's fifth tag name (typically exterior/wear).</summary>
    [JsonPropertyName("tag5")]
    public string? Tag5 { get; set; }

    /// <summary>Positional copy of the item's sixth tag name (typically rarity).</summary>
    [JsonPropertyName("tag6")]
    public string? Tag6 { get; set; }

    /// <summary>Positional copy of the item's seventh tag name, when present.</summary>
    [JsonPropertyName("tag7")]
    public string? Tag7 { get; set; }

    /// <summary>The lowest current price among third-party markets (subject to the <c>markets</c> filter).</summary>
    [JsonPropertyName("pricereal")]
    public double? PriceReal { get; set; }

    /// <summary>Lowest third-party market price observed in the last 24 hours.</summary>
    [JsonPropertyName("pricereal24h")]
    public double? PriceReal24h { get; set; }

    /// <summary>Lowest third-party market price observed in the last 7 days.</summary>
    [JsonPropertyName("pricereal7d")]
    public double? PriceReal7d { get; set; }

    /// <summary>Lowest third-party market price observed in the last 30 days.</summary>
    [JsonPropertyName("pricereal30d")]
    public double? PriceReal30d { get; set; }

    /// <summary>Lowest third-party market price observed in the last 90 days.</summary>
    [JsonPropertyName("pricereal90d")]
    public double? PriceReal90d { get; set; }

    /// <summary>Median third-party market price.</summary>
    [JsonPropertyName("pricerealmedian")]
    public double? PriceRealMedian { get; set; }

    /// <summary>The absolute price difference between Steam and third-party markets. Positive means Steam is cheaper.</summary>
    [JsonPropertyName("winloss")]
    public double? WinLoss { get; set; }

    /// <summary>The relative price difference between Steam and third-party markets.</summary>
    [JsonPropertyName("winlossprice")]
    public double? WinLossPrice { get; set; }

    /// <summary>Per-market current offers, one entry per third-party market that lists this item.</summary>
    [JsonPropertyName("prices")]
    public IReadOnlyList<MarketPriceEntry>? Prices { get; set; }

    /// <summary>The base display name shared across this item's wear/StatTrak/souvenir variants, e.g. "Karambit | Doppler".</summary>
    [JsonPropertyName("groupname")]
    public string? GroupName { get; set; }

    /// <summary>The item's exterior/wear category.</summary>
    [JsonPropertyName("wear")]
    public Wear? Wear { get; set; }

    /// <summary>Whether the item name carries the "★" (knife/glove) prefix.</summary>
    [JsonPropertyName("isstar")]
    public bool? IsStar { get; set; }

    /// <summary>Whether this is the StatTrak™ variant.</summary>
    [JsonPropertyName("isstattrak")]
    public bool? IsStatTrak { get; set; }

    /// <summary>Whether this is the Souvenir variant.</summary>
    [JsonPropertyName("issouvenir")]
    public bool? IsSouvenir { get; set; }

    /// <summary>The item category, e.g. "knife", "rifle", "case".</summary>
    [JsonPropertyName("itemgroup")]
    public string? ItemGroup { get; set; }

    /// <summary>The skin name, e.g. "doppler", "redline".</summary>
    [JsonPropertyName("itemname")]
    public string? ItemName { get; set; }

    /// <summary>The weapon/item type slug, e.g. "karambit", "ak-47".</summary>
    [JsonPropertyName("itemtype")]
    public string? ItemType { get; set; }

    /// <summary>The minimum possible float value for this skin pattern.</summary>
    [JsonPropertyName("minfloat")]
    public double? MinFloat { get; set; }

    /// <summary>The maximum possible float value for this skin pattern.</summary>
    [JsonPropertyName("maxfloat")]
    public double? MaxFloat { get; set; }

    /// <summary>The weapon/item definition index.</summary>
    [JsonPropertyName("defindex")]
    public int? DefIndex { get; set; }

    /// <summary>The skin paint index.</summary>
    [JsonPropertyName("paintindex")]
    public int? PaintIndex { get; set; }

    /// <summary>Doppler/Gamma Doppler/Marble Fade phase siblings of this item, present only when the base skin has phases.</summary>
    [JsonPropertyName("variants")]
    public IReadOnlyList<ItemVariant>? Variants { get; set; }

    /// <summary>The number of distinct third-party markets currently offering this item.</summary>
    [JsonPropertyName("realmarketsquantity")]
    public int? RealMarketsQuantity { get; set; }
}
