using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Screenshot;

/// <summary>Vertical alignment of the default or custom background image of a screenshot.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ScreenshotVerticalAlign>))]
public enum ScreenshotVerticalAlign
{
    /// <summary>Aligned to the top. The default.</summary>
    [EnumMember(Value = "top")]
    Top = 0,

    /// <summary>Centered.</summary>
    [EnumMember(Value = "center")]
    Center,

    /// <summary>Aligned to the bottom.</summary>
    [EnumMember(Value = "bottom")]
    Bottom,
}
