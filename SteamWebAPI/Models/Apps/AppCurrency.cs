using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Apps;

/// <summary>
/// A Steam storefront currency accepted by the Apps endpoints. This set differs from
/// <see cref="Common.SteamCurrency"/>, which covers the item-pricing endpoints.
/// </summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<AppCurrency>))]
public enum AppCurrency
{
    /// <summary>United States Dollar. The default.</summary>
    [EnumMember(Value = "USD")]
    Usd = 0,

    /// <summary>Euro.</summary>
    [EnumMember(Value = "EUR")]
    Eur,

    /// <summary>British Pound.</summary>
    [EnumMember(Value = "GBP")]
    Gbp,

    /// <summary>Brazilian Real.</summary>
    [EnumMember(Value = "BRL")]
    Brl,

    /// <summary>Russian Ruble.</summary>
    [EnumMember(Value = "RUB")]
    Rub,

    /// <summary>Japanese Yen.</summary>
    [EnumMember(Value = "JPY")]
    Jpy,

    /// <summary>Chinese Yuan.</summary>
    [EnumMember(Value = "CNY")]
    Cny,

    /// <summary>Indian Rupee.</summary>
    [EnumMember(Value = "INR")]
    Inr,

    /// <summary>Ukrainian Hryvnia.</summary>
    [EnumMember(Value = "UAH")]
    Uah,

    /// <summary>Kazakhstani Tenge.</summary>
    [EnumMember(Value = "KZT")]
    Kzt,

    /// <summary>Australian Dollar.</summary>
    [EnumMember(Value = "AUD")]
    Aud,

    /// <summary>Canadian Dollar.</summary>
    [EnumMember(Value = "CAD")]
    Cad,

    /// <summary>Polish Zloty.</summary>
    [EnumMember(Value = "PLN")]
    Pln,

    /// <summary>Swiss Franc.</summary>
    [EnumMember(Value = "CHF")]
    Chf,
}
