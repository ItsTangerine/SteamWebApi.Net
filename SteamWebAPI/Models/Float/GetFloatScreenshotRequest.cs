namespace SteamWebAPI.Models.Float;

/// <summary>Rendering options for <see cref="SteamWebApiClient.GetFloatScreenshotAsync"/>.</summary>
public sealed class GetFloatScreenshotRequest
{
    /// <summary>The background accent color. Defaults to <see cref="FloatScreenshotColor.Green"/>.</summary>
    public FloatScreenshotColor? Color { get; set; }

    /// <summary>A custom PNG background image URL, overriding <see cref="Color"/>.</summary>
    public string? BackgroundUrl { get; set; }

    /// <summary>A custom PNG logo image URL to overlay on the render.</summary>
    public string? LogoUrl { get; set; }

    /// <summary>The corner <see cref="LogoUrl"/> is anchored to. Defaults to <see cref="FloatScreenshotLogoOffset.TopLeft"/>.</summary>
    public FloatScreenshotLogoOffset? LogoOffsetStart { get; set; }

    /// <summary>The logo's horizontal offset from its anchor corner, in pixels. Defaults to 80.</summary>
    public double? LogoOffsetX { get; set; }

    /// <summary>The logo's vertical offset from its anchor corner, in pixels. Defaults to 80.</summary>
    public double? LogoOffsetY { get; set; }

    /// <summary>The logo's opacity, from 0 to 1. Defaults to 1.0.</summary>
    public double? LogoOpacity { get; set; }

    /// <summary>The logo's rendered width, in pixels (maximum 500). Defaults to 400.</summary>
    public int? LogoWidth { get; set; }
}
