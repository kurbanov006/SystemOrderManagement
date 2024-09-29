namespace Infrastructure.Models.Request;

public class ReceivingOrdersFilteredByOrderStatusAndDate
{
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
    public Guid CustomerId { get; set; }
    public string FullName { get; set; } = null!;
    public string Email { get; set; } = null!;
}