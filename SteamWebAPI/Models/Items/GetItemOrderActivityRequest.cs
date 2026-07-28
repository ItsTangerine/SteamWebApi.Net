namespace SteamWebAPI.Models.Items;

/// <summary>Options for <see cref="SteamWebApiClient.GetItemOrderActivityAsync"/>.</summary>
public sealed class GetItemOrderActivityRequest
{
    /// <summary>The Steam store country/region code. Defaults to "EN".</summary>
    public string? Country { get; set; }

    /// <summary>The Steam UI language for descriptive text. Defaults to "english".</summary>
    public string? Language { get; set; }

    /// <summary>Steam's numeric currency code. Defaults to 1 (USD).</summary>
    public int? Currency { get; set; }
}
