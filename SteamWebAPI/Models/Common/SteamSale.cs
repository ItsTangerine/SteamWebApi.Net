using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>A single day's aggregated sale from an item's <c>latest10steamsales</c> list.</summary>
/// <param name="Date">The UTC date the sale(s) were recorded.</param>
/// <param name="Price">The recorded sale price.</param>
/// <param name="Quantity">The number of units sold that day at this price.</param>
[JsonConverter(typeof(SteamSaleConverter))]
public sealed record SteamSale(DateTimeOffset Date, decimal Price, int Quantity);
