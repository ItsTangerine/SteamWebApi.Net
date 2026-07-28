namespace SteamWebAPI.Models.Info;

/// <summary>Options for <see cref="SteamWebApiClient.GetContainersAsync"/>.</summary>
public sealed class GetContainersRequest
{
    /// <summary>Which kind of container to return.</summary>
    public ContainerType Type { get; set; } = ContainerType.All;

    /// <summary>Filters to containers whose name contains this text.</summary>
    public string? Search { get; set; }

    /// <summary>How to order the results.</summary>
    public ContainerSortBy? SortBy { get; set; }
}
