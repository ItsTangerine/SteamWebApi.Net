using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.Apps;

/// <summary>A platform filter for <see cref="GetAppsRequest.Platform"/>.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<AppPlatform>))]
public enum AppPlatform
{
    /// <summary>Windows.</summary>
    [EnumMember(Value = "windows")]
    Windows = 0,

    /// <summary>macOS.</summary>
    [EnumMember(Value = "mac")]
    Mac,

    /// <summary>Linux.</summary>
    [EnumMember(Value = "linux")]
    Linux,
}
