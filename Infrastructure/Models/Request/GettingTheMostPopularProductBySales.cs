namespace Infrastructure.Models.Request;

public class GettingTheMostPopularProductBySales
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public int CountProduct { get; set; }
}