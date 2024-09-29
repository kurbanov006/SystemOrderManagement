namespace Infrastructure.Models.Request;

public class GetOrderInformationWithTotalAmountForEachProduct
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public decimal AllSum { get; set; }
}