using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>The action to take on one or more Steam mobile confirmations.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<ConfirmationOp>))]
public enum ConfirmationOp
{
    /// <summary>Approve the confirmation(s).</summary>
    [EnumMember(Value = "allow")]
    Allow = 0,

    /// <summary>Reject the confirmation(s).</summary>
    [EnumMember(Value = "cancel")]
    Cancel,

    /// <summary>Approve the confirmation(s). Alias observed alongside <see cref="Allow"/>.</summary>
    [EnumMember(Value = "accept")]
    Accept,

    /// <summary>Reject the confirmation(s). Alias observed alongside <see cref="Cancel"/>.</summary>
    [EnumMember(Value = "deny")]
    Deny,
}
