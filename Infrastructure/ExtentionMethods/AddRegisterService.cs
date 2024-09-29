using Infrastructure.Services.CustomerService;
using Infrastructure.Services.OrderItemService;
using Infrastructure.Services.OrderService;
using Infrastructure.Services.ProductService;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.ExtentionMethods;

public static class AddRegisterService
{
    public static void RegisterService(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddTransient<ICustomerService, CustomerService>();
        serviceCollection.AddTransient<IProductService, ProductService>();
        serviceCollection.AddTransient<IOrderService, OrderService>();
        serviceCollection.AddTransient<IOrderItemService, OrderItemService>();
    }
}