using Infrastructure.Models;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.ProductService;

public interface IProductService
{
    Task<bool> Create(Product product);
    Task<bool> Update(Product product);
    Task<bool> Delete(Guid id);
    Task<ProductFull?> GetById(Guid id);
    Task<IEnumerable<ProductFull?>> GetAll();
    Task<IEnumerable<ProductFull?>> ReceivingProductsWithZeroQuantityInStock();
    Task<IEnumerable<GettingTheMostPopularProductBySales?>> GettingTheMostPopularProductBySales();

    Task<IEnumerable<GetOrderInformationWithTotalAmountForEachProduct?>>
        GetOrderInformationWithTotalAmountForEachProduct();
}