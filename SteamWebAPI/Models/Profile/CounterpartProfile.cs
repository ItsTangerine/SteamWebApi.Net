using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Profile;

/// <summary>
/// Who a trade counterpart or linked account is, as embedded in <see cref="ProfileTradePartner.Profile"/> and
/// <see cref="SmurfLinkedAccount.Profile"/>.
/// </summary>
public sealed class CounterpartProfile : BaseResponseDto
{
    /// <summary>The account's current display/persona name.</summary>
    [JsonPropertyName("personaname")]
    public string? PersonaName { get; set; }

    /// <summary>The account's small avatar image URL.</summary>
    [JsonPropertyName("avatar")]
    public string? Avatar { get; set; }

    /// <summary>The account's full-size avatar image URL.</summary>
    [JsonPropertyName("avatarfull")]
    public string? AvatarFull { get; set; }

    /// <summary>When the Steam account was created.</summary>
    [JsonPropertyName("timecreated")]
    [JsonConverter(typeof(UnixSecondsConverter))]
    public DateTimeOffset? TimeCreated { get; set; }

    /// <summary>When the Steam account was created, as an ISO 8601 timestamp.</summary>
    [JsonPropertyName("timecreatedat")]
    public DateTimeOffset? TimeCreatedAt { get; set; }

    /// <summary>The account's ISO 3166-1 alpha-2 country code, when public.</summary>
    [JsonPropertyName("loccountrycode")]
    public string? LocCountryCode { get; set; }

    /// <summary>Whether the account is VAC banned. steamwebapi.com sends this as 0/1 or true/false depending on the endpoint.</summary>
    [JsonPropertyName("vac")]
    [JsonConverter(typeof(FlexibleBooleanConverter))]
    public bool Vac { get; set; }

    /// <summary>Whether the account has a Steam Community trade ban, or <see langword="null"/> when unknown.</summary>
    [JsonPropertyName("tradeban")]
    [JsonConverter(typeof(FlexibleBooleanConverter))]
    public bool? TradeBan { get; set; }

    /// <summary>The account's inventory worth at Steam Market prices.</summary>
    [JsonPropertyName("inventoryworthpricesteam")]
    public double? InventoryWorthPriceSteam { get; set; }
}
