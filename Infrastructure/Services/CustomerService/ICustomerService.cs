using Infrastructure.Models;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.CustomerService;

public interface ICustomerService
{
    Task<bool> Create(Customer customer);
    Task<bool> Update(Customer customer);
    Task<bool> Delete(Guid id);
    Task<CustomerFull?> GetById(Guid id);
    Task<IEnumerable<CustomerFull?>> GetAll();

    Task<IEnumerable<ReceivingCustomersWithOrdersForACertainPeriod?>>
        GetReceivingCustomersWithOrdersForACertainPeriod(DateTime startDate, DateTime endDate);

    Task<IEnumerable<GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient?>>
        GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient();
}