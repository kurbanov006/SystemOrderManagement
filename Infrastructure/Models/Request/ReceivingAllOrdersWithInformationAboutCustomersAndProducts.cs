namespace Infrastructure.Models.Request;

public class ReceivingAllOrdersWithInformationAboutCustomersAndProducts
{
    public Guid OrderId { get; set; }
    public decimal TotalAmount { get; set; }
    public DateTime OrderDate { get; set; }
    public Guid CustomerId { get; set; }
    public string FullName { get; set; } = null!;
    public Guid ProductId { get; set; }
    public string Name { get; set; } = null!;
}