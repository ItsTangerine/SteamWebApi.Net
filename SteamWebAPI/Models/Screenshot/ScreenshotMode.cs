using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Screenshot;

/// <summary>Which side(s) of the item <see cref="SteamWebApiClient.GetScreenshotAsync"/> renders.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ScreenshotMode>))]
public enum ScreenshotMode
{
    /// <summary>The front side. The default.</summary>
    [EnumMember(Value = "front")]
    Front = 0,

    /// <summary>The back side.</summary>
    [EnumMember(Value = "back")]
    Back,

    /// <summary>Both sides. Not supported with <see cref="GetScreenshotRequest.Transparent"/>.</summary>
    [EnumMember(Value = "both")]
    Both,
}
