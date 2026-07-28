using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Float;

/// <summary>The background accent color for a <see cref="SteamWebApiClient.GetFloatScreenshotAsync"/> render.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<FloatScreenshotColor>))]
public enum FloatScreenshotColor
{
    /// <summary>Green. The default.</summary>
    [EnumMember(Value = "green")]
    Green = 0,

    /// <summary>Black.</summary>
    [EnumMember(Value = "black")]
    Black,

    /// <summary>Blue.</summary>
    [EnumMember(Value = "blue")]
    Blue,

    /// <summary>Orange.</summary>
    [EnumMember(Value = "orange")]
    Orange,

    /// <summary>Purple.</summary>
    [EnumMember(Value = "purple")]
    Purple,

    /// <summary>Red.</summary>
    [EnumMember(Value = "red")]
    Red,

    /// <summary>White.</summary>
    [EnumMember(Value = "white")]
    White,

    /// <summary>Yellow.</summary>
    [EnumMember(Value = "yellow")]
    Yellow,

    /// <summary>Gray.</summary>
    [EnumMember(Value = "gray")]
    Gray,
}
