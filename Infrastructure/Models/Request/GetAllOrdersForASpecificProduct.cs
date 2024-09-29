namespace Infrastructure.Models.Request;

public class GetAllOrdersForASpecificProduct
{
    public Guid OrderId { get; set; }
    public Guid CustomerId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public string Status { get; set; } = null!;
}