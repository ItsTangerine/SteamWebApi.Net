using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Float;

/// <summary>The corner a custom logo is anchored to in a <see cref="SteamWebApiClient.GetFloatScreenshotAsync"/> render.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<FloatScreenshotLogoOffset>))]
public enum FloatScreenshotLogoOffset
{
    /// <summary>Top-left corner. The default.</summary>
    [EnumMember(Value = "top left")]
    TopLeft = 0,

    /// <summary>Top-right corner.</summary>
    [EnumMember(Value = "top right")]
    TopRight,

    /// <summary>Bottom-left corner.</summary>
    [EnumMember(Value = "bottom left")]
    BottomLeft,

    /// <summary>Bottom-right corner.</summary>
    [EnumMember(Value = "bottom right")]
    BottomRight,
}
