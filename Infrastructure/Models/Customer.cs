using System.Linq.Expressions;
using System.Text.Json.Serialization;

namespace Infrastructure.Models;

public class Customer
{
    [JsonIgnore]
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    [JsonIgnore]
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}


public class CustomerFull
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.Now;
}