using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Info;

/// <summary>Which kind of CS2/CS:GO container to return from <see cref="SteamWebApiClient.GetContainersAsync"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ContainerType>))]
public enum ContainerType
{
    /// <summary>Every container type.</summary>
    [EnumMember(Value = "all")]
    All = 0,

    /// <summary>Sticker capsules.</summary>
    [EnumMember(Value = "sticker")]
    Sticker,

    /// <summary>Weapon cases.</summary>
    [EnumMember(Value = "case")]
    Case,

    /// <summary>Souvenir/other packages.</summary>
    [EnumMember(Value = "package")]
    Package,
}
