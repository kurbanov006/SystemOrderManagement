using Infrastructure.Models;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.OrderItemService;

public interface IOrderItemService
{
    Task<bool> Create(OrderItem orderItem);
    Task<bool> Update(OrderItem orderItem);
    Task<bool> Delete(Guid id);
    Task<OrderItemFull?> GetOrderItemById(Guid id);
    Task<IEnumerable<OrderItemFull?>> GetAll();
    Task<IEnumerable<GettingSalesStatisticsForTheMonth?>> GettingSalesStatisticsForTheMonth(int month, int year);
}