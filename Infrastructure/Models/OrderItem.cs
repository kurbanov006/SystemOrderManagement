using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class OrderItem
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class OrderItemFull
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid OrderId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
    public decimal Price { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}