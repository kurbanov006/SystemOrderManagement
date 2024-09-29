namespace Infrastructure.Models.Request;

public class ReceivingCustomersWithOrdersForACertainPeriod
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
}