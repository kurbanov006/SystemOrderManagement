using System.Runtime;

namespace Infrastructure.Models.Request;

public class GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string Phone { get; set; } = null!;
    public DateTime CreatedAt { get; set; }
    public int CountOrder { get; set; }
    public decimal SumOrder { get; set; }
}