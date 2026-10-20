using SteamWebAPI.Models.Float;

namespace SteamWebAPI.Models.Screenshot;

/// <summary>Rendering options for <see cref="SteamWebApiClient.GetScreenshotAsync"/>.</summary>
public sealed class GetScreenshotRequest
{
    /// <summary>Which side(s) to render. Defaults to <see cref="ScreenshotMode.Front"/>.</summary>
    public ScreenshotMode? Mode { get; set; }

    /// <summary>
    /// Image width in pixels (256–2048); the height scales proportionally. Defaults to 1920. Widths below 960 require
    /// <see cref="WithFloat"/> to be false.
    /// </summary>
    public int? Width { get; set; }

    /// <summary>
    /// When true, renders a transparent cutout (sent as <c>view=transparent</c>), which also disables the default
    /// background and float information. Cannot be combined with <see cref="ScreenshotMode.Both"/>,
    /// <see cref="BackgroundColor"/>, <see cref="BackgroundUrl"/>, <see cref="BackgroundVerticalAlign"/>, or
    /// <c>WithFloat = true</c>.
    /// </summary>
    public bool? Transparent { get; set; }

    /// <summary>A solid <c>#RRGGBB</c> background instead of the default background image.</summary>
    public string? BackgroundColor { get; set; }

    /// <summary>A custom HTTP(S) background image URL, without credentials or a fragment. Cannot be combined with <see cref="BackgroundColor"/>.</summary>
    public string? BackgroundUrl { get; set; }

    /// <summary>Vertical alignment of the background image. Defaults to <see cref="ScreenshotVerticalAlign.Top"/>.</summary>
    public ScreenshotVerticalAlign? BackgroundVerticalAlign { get; set; }

    /// <summary>A publicly accessible HTTP(S) logo image URL, without credentials or a fragment.</summary>
    public string? LogoUrl { get; set; }

    /// <summary>The corner the logo is positioned from. Requires <see cref="LogoUrl"/>. Defaults to top left.</summary>
    public FloatScreenshotLogoOffset? LogoOffsetStart { get; set; }

    /// <summary>Horizontal logo offset in pixels (0–4096). Defaults to 80.</summary>
    public int? LogoOffsetX { get; set; }

    /// <summary>Vertical logo offset in pixels (0–4096). Defaults to 80.</summary>
    public int? LogoOffsetY { get; set; }

    /// <summary>Logo opacity, from 0 (invisible) to 1 (opaque). Defaults to 1.</summary>
    public double? LogoOpacity { get; set; }

    /// <summary>Logo width in pixels (1–1024). Defaults to 400.</summary>
    public int? LogoWidth { get; set; }

    /// <summary>Whether to show float information. Defaults to true (false when <see cref="Transparent"/>). Requires a width of at least 960.</summary>
    public bool? WithFloat { get; set; }

    /// <summary>A custom item label (max 64 characters) instead of the original name.</summary>
    public string? ItemName { get; set; }

    /// <summary>A custom finish label (max 64 characters) instead of the original name.</summary>
    public string? PaintName { get; set; }

    /// <summary>
    /// A client-chosen key (max 128 characters of <c>A-Z a-z 0-9 _ . : -</c>). Reuse it with unchanged options after
    /// a timeout so the retry doesn't render twice.
    /// </summary>
    public string? IdempotencyKey { get; set; }
}
