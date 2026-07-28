using System.Text.Json;
using SteamWebAPI.Models.Common;

namespace SteamWebAPI.Tests.Converters;

public class SteamSaleConverterTests
{
    [Fact]
    public void Deserializes_ThreeElementArray()
    {
        const string json = "[\"2025-10-24\", 1372.29, 5]";

        var sale = JsonSerializer.Deserialize<SteamSale>(json);

        Assert.NotNull(sale);
        Assert.Equal(new DateTimeOffset(2025, 10, 24, 0, 0, 0, TimeSpan.Zero), sale.Date);
        Assert.Equal(1372.29m, sale.Price);
        Assert.Equal(5, sale.Quantity);
    }

    [Fact]
    public void Deserializes_ArrayOfSales()
    {
        const string json = """
            [
                ["2025-10-24", 1372.29, 5],
                ["2025-10-23", 1594.68, 17]
            ]
            """;

        var sales = JsonSerializer.Deserialize<List<SteamSale>>(json);

        Assert.NotNull(sales);
        Assert.Equal(2, sales!.Count);
        Assert.Equal(17, sales[1].Quantity);
    }

    [Fact]
    public void Serializes_AsThreeElementArray()
    {
        var sale = new SteamSale(new DateTimeOffset(2025, 10, 24, 0, 0, 0, TimeSpan.Zero), 1372.29m, 5);

        var json = JsonSerializer.Serialize(sale);

        Assert.Equal("[\"2025-10-24\",1372.29,5]", json);
    }
}
