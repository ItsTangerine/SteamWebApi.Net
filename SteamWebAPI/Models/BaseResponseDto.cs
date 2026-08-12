using System.Text.Json;
using System.Text.Json.Serialization;

namespace SteamWebAPI.Models;

/// <summary>
/// Represents the base structure for a response, providing an extension point for additional data.
/// This class enables the deserialization of JSON properties not explicitly defined in the response model.
/// </summary>
public class BaseResponseDto
{
    /// <summary>
    /// Response fields not currently represented by strongly typed properties.
    /// This provides forward compatibility when the API adds new fields.
    /// </summary>
    [JsonExtensionData]
    public IDictionary<string, JsonElement>? AdditionalData { get; set; }
}