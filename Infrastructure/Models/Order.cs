using System.Runtime.InteropServices.JavaScript;
using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class Order
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    [JsonIgnore]
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string Status { get; set; } = null!;
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

public class OrderFull
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; } = DateTime.Now;
    public string Status { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}