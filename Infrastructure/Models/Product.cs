using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class Product
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class ProductFull
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}