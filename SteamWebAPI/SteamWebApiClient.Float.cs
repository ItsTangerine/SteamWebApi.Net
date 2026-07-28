using SteamWebAPI.Models.Float;
using SteamWebAPI.Results;

namespace SteamWebAPI;

public sealed partial class SteamWebApiClient
{
    /// <summary>
    /// Searches the stored CS asset database (inventory items and market listings) by owner, item, float, sticker,
    /// and keychain filters, with pagination. See <c>GET /steam/api/float/assets</c>.
    /// </summary>
    /// <param name="request">Filtering, sorting, and pagination options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<FloatAssetSearchResult>> SearchFloatAssetsAsync(
        GetFloatAssetsRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        request ??= new GetFloatAssetsRequest();
        var query = NewQuery();
        SetParam(query, "steam_id", request.SteamId);
        SetParam(query, "market_hash_name", request.MarketHashName);
        SetParam(query, "def_index", request.DefIndex);
        SetParam(query, "paint_index", request.PaintIndex);
        SetParam(query, "min_float", request.MinFloat);
        SetParam(query, "max_float", request.MaxFloat);
        SetFlag(query, "is_stattrak", request.IsStatTrak);
        SetFlag(query, "is_souvenir", request.IsSouvenir);
        SetParam(query, "rarity", request.Rarity);
        SetParam(query, "quality", request.Quality);
        SetParam(query, "origin", request.Origin);
        SetParam(query, "paint_seed", request.PaintSeed);
        SetEnumParam(query, "wear", request.Wear);
        SetParam(query, "phase", request.Phase);
        SetParam(query, "source", request.Source);
        SetParam(query, "asset_id", request.AssetId);
        SetParam(query, "date", request.Date);
        SetEnumParam(query, "sort", request.Sort);
        SetParam(query, "limit", request.Limit);
        SetParam(query, "offset", request.Offset);
        SetFlag(query, "only_steam_id", request.OnlySteamId);
        SetFlag(query, "only_market_id", request.OnlyMarketId);
        SetFlag(query, "with_stickers", request.WithStickers);
        SetFlag(query, "with_keychains", request.WithKeychains);
        SetFlag(query, "with_items", request.WithItems);
        SetFlag(query, "with_profiles", request.WithProfiles);

        return await GetAsync<FloatAssetSearchResult>("/steam/api/float/assets", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Decodes a CS2 inspect-link certificate (certificate-format links only; legacy S/M-style links are rejected)
    /// into full float/item state, without a Steam GC roundtrip. See <c>GET /steam/api/float</c>.
    /// </summary>
    /// <param name="url">The inspect link to decode, certificate format only. Exactly one of <paramref name="url"/>/<paramref name="certificate"/> must be provided.</param>
    /// <param name="certificate">The raw hex certificate to decode (no prefix). Exactly one of <paramref name="url"/>/<paramref name="certificate"/> must be provided.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<FloatDecodeResult>> DecodeFloatAsync(
        string? url = null,
        string? certificate = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(url) && string.IsNullOrWhiteSpace(certificate))
            throw new ArgumentException("Either url or certificate must be provided.", nameof(url));

        var query = NewQuery();
        SetParam(query, "url", url);
        SetParam(query, "certificate", certificate);
        query["production"] = "1";

        return await GetAsync<FloatDecodeResult>("/steam/api/float", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Renders a PNG image visualizing an item's float info (decoded from its inspect certificate) onto a
    /// customizable background, and returns the raw image bytes. See <c>GET /steam/api/float/screenshot</c>.
    /// </summary>
    /// <param name="url">The inspect link to render, certificate format only. Required.</param>
    /// <param name="request">Background/logo rendering options.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<byte[]>> GetFloatScreenshotAsync(
        string url,
        GetFloatScreenshotRequest? request = null,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(url))
            throw new ArgumentException("Inspect link URL must not be null or empty.", nameof(url));

        request ??= new GetFloatScreenshotRequest();
        var query = NewQuery();
        query["url"] = url;
        SetEnumParam(query, "color", request.Color);
        SetParam(query, "background_url", request.BackgroundUrl);
        SetParam(query, "logo_url", request.LogoUrl);
        SetEnumParam(query, "logo_offset_start", request.LogoOffsetStart);
        SetParam(query, "logo_offset_x", request.LogoOffsetX);
        SetParam(query, "logo_offset_y", request.LogoOffsetY);
        SetParam(query, "logo_opacity", request.LogoOpacity);
        SetParam(query, "logo_width", request.LogoWidth);

        return await GetBinaryAsync("/steam/api/float/screenshot", query, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Generates a self-contained CS2 inspect-link (certificate hex) from raw item data, without requiring a live
    /// item. The inverse of <see cref="DecodeFloatAsync"/>. See <c>POST /steam/api/float/create-inspectlink</c>.
    /// </summary>
    /// <param name="request">The item data to encode.</param>
    /// <param name="cancellationToken">A token to cancel the request.</param>
    public async Task<Result<CreateInspectLinkResult>> CreateInspectLinkAsync(
        CreateInspectLinkRequest request,
        CancellationToken cancellationToken = default)
    {
        if (request is null)
            throw new ArgumentNullException(nameof(request));

        var query = NewQuery();

        return await PostAsync<CreateInspectLinkResult>("/steam/api/float/create-inspectlink", query, request, cancellationToken).ConfigureAwait(false);
    }
}
