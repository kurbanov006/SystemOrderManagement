using System.Diagnostics.SymbolStore;
using Infrastructure.Models;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.OrderService;

public interface IOrderService
{
    Task<bool> Create(Order order);
    Task<bool> Update(Order order);
    Task<bool> Delete(Guid id);
    Task<OrderFull?> GetById(Guid id);
    Task<IEnumerable<OrderFull?>> GetAll();

    Task<IEnumerable<ReceivingAllOrdersWithInformationAboutCustomersAndProducts?>>
        GetReceivingAllOrdersWithInformationAboutCustomersAndProducts();

    Task<IEnumerable<ReceivingOrdersFilteredByOrderStatusAndDate?>> 
        ReceivingOrdersFilteredByOrderStatusAndDate(string status, DateTime startDate, DateTime endDate);

    Task<IEnumerable<GetAllOrdersForASpecificProduct?>> GetAllOrdersForASpecificProduct(Guid productId);
}