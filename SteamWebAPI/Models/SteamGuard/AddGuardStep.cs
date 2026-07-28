using System.Runtime.Serialization;
using System.Text.Json.Serialization;
using SteamWebAPI.Models.Converters;

namespace SteamWebAPI.Models.SteamGuard;

/// <summary>Forces which step of the <c>/steam/api/guard/add</c> lifecycle to run, instead of letting the server auto-detect it.</summary>
[JsonConverter(typeof(JsonStringEnumMemberConverter<AddGuardStep>))]
public enum AddGuardStep
{
    /// <summary>Step 1: begin enrollment (login with username/password, request/consume an email code).</summary>
    [EnumMember(Value = "1")]
    Add = 0,

    /// <summary>Step 2: finalize enrollment with the SMS/email activation code.</summary>
    [EnumMember(Value = "2")]
    Finalize,
}
