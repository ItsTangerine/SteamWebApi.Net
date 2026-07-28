using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Common;

/// <summary>A currency code accepted by steamwebapi.com's price-conversion parameters.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<SteamCurrency>))]
public enum SteamCurrency
{
    /// <summary>United States Dollar.</summary>
    [EnumMember(Value = "USD")]
    Usd = 0,

    /// <summary>Euro.</summary>
    [EnumMember(Value = "EUR")]
    Eur,

    /// <summary>British Pound.</summary>
    [EnumMember(Value = "GBP")]
    Gbp,

    /// <summary>Turkish Lira.</summary>
    [EnumMember(Value = "TRY")]
    Try,

    /// <summary>Russian Ruble.</summary>
    [EnumMember(Value = "RUB")]
    Rub,

    /// <summary>Chinese Yuan.</summary>
    [EnumMember(Value = "CNY")]
    Cny,

    /// <summary>Japanese Yen.</summary>
    [EnumMember(Value = "JPY")]
    Jpy,

    /// <summary>Brazilian Real.</summary>
    [EnumMember(Value = "BRL")]
    Brl,

    /// <summary>Polish Zloty.</summary>
    [EnumMember(Value = "PLN")]
    Pln,

    /// <summary>Canadian Dollar.</summary>
    [EnumMember(Value = "CAD")]
    Cad,

    /// <summary>Australian Dollar.</summary>
    [EnumMember(Value = "AUD")]
    Aud,

    /// <summary>Ukrainian Hryvnia.</summary>
    [EnumMember(Value = "UAH")]
    Uah,

    /// <summary>South Korean Won.</summary>
    [EnumMember(Value = "KRW")]
    Krw,
}
