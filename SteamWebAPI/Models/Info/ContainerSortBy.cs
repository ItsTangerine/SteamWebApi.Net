using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Info;

/// <summary>Sort order for <see cref="SteamWebApiClient.GetContainersAsync"/> results.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ContainerSortBy>))]
public enum ContainerSortBy
{
    /// <summary>Alphabetical, A→Z.</summary>
    [EnumMember(Value = "nameAz")]
    NameAscending = 0,

    /// <summary>Alphabetical, Z→A.</summary>
    [EnumMember(Value = "nameZa")]
    NameDescending,

    /// <summary>Steam price ascending.</summary>
    [EnumMember(Value = "priceSteamAz")]
    SteamPriceAscending,

    /// <summary>Steam price descending.</summary>
    [EnumMember(Value = "priceSteamZa")]
    SteamPriceDescending,

    /// <summary>Third-party market price ascending.</summary>
    [EnumMember(Value = "priceRealAz")]
    RealPriceAscending,

    /// <summary>Third-party market price descending.</summary>
    [EnumMember(Value = "priceRealZa")]
    RealPriceDescending,

    /// <summary>Release date ascending (oldest first).</summary>
    [EnumMember(Value = "releasedatAz")]
    ReleaseDateAscending,

    /// <summary>Release date descending (newest first).</summary>
    [EnumMember(Value = "releasedatZa")]
    ReleaseDateDescending,
}
