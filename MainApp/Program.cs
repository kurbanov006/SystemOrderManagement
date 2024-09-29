using Infrastructure.ExtentionMethods;
using Infrastructure.Models;
using Infrastructure.Models.Request;
using Infrastructure.Services.CustomerService;
using Infrastructure.Services.OrderItemService;
using Infrastructure.Services.OrderService;
using Infrastructure.Services.ProductService;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.RegisterService();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

// Общий 9 запросов сделано!


#region Customer
app.MapPost("/customers", async (Customer customer, ICustomerService customerService) =>
{
    bool res = await customerService.Create(customer);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось добавить!"}); 
    }
    return Results.Ok(new {message = "Успешно добавлено!"});
});

app.MapPut("/customers/{id}", async (Guid id, Customer customer, ICustomerService customerService) =>
{
    customer.Id = id;
    bool res = await customerService.Update(customer);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось изменить!"}); 
    }
    return Results.Ok(new {message = "Успешно изменено!"});
});

app.MapDelete("/customers/{id}", async (Guid id, ICustomerService customerService) =>
{
    bool res = await customerService.Delete(id);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось удалить!"}); 
    }
    return Results.Ok(new {message = "Успешно удалено!"});
});

app.MapGet("/customers/{id}", async (Guid id, ICustomerService customerService) =>
{
    CustomerFull? customer = await customerService.GetById(id);
    if (customer == null)
    {
        return Results.NotFound(new {message = "Не найдено!"}); 
    }
    return Results.Ok(customer);
});

app.MapGet("/customers", async (ICustomerService customerService) =>
{
    IEnumerable<CustomerFull?> customers = await customerService.GetAll();
    if (customers == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(customers);
});
// Запрос 1
app.MapGet("/customers/startDate={startDate}&endDate={endDate}",
    async (DateTime startDate, DateTime endDate, ICustomerService customerService) =>
    {
        IEnumerable<ReceivingCustomersWithOrdersForACertainPeriod?> customers =
            await customerService.GetReceivingCustomersWithOrdersForACertainPeriod(startDate, endDate);
        if (customers == null!)
        {
            return Results.NotFound(new {message = "Не удалось найти!"});
        }
        return Results.Ok(customers);
    });

// Запрос 3
app.MapGet("/customers/statistics", async (ICustomerService customerService) =>
{
    IEnumerable<GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient?> customer =
      await  customerService.GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient();
    if (customer == null!)
    {
        return Results.NotFound(new {message="Не удалось найти!"});
    }
    return Results.Ok(customer);
});
#endregion

#region Product
app.MapPost("/products", async (Product product, IProductService productService) =>
{
    bool res = await productService.Create(product);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось добавить!"}); 
    }
    return Results.Ok(new {message = "Успешно добавлено!"});
});

app.MapPut("/products{id}", async (Guid id, Product product, IProductService productService) =>
{
    product.Id = id;
    bool res = await productService.Update(product);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось изменить!"}); 
    }
    return Results.Ok(new {message = "Успешно изменено!"});
});

app.MapDelete("/products/{id}", async (Guid id, IProductService productService) =>
{
    bool res = await productService.Delete(id);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось удалить!"}); 
    }
    return Results.Ok(new {message = "Успешно удалено!"});
});

app.MapGet("/products/{id}", async (Guid id, IProductService productService) =>
{
    ProductFull? product = await productService.GetById(id);
    if (product == null)
    {
        return Results.NotFound(new {message = "Не найдено!"}); 
    }
    return Results.Ok(product);
});

app.MapGet("/products", async (IProductService productService) =>
{
    IEnumerable<ProductFull?> products = await productService.GetAll();
    if (products == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(products);
});

// Запрос 2
app.MapGet("/products/out-of-stock", async (IProductService productService) =>
{
    IEnumerable<ProductFull?> products = await productService.ReceivingProductsWithZeroQuantityInStock();
    if (products == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(products);
        
});

// Запрос 6
app.MapGet("/products/popular", async (IProductService productService) =>
{
    IEnumerable<GettingTheMostPopularProductBySales?> products = 
        await productService.GettingTheMostPopularProductBySales();
    if (products == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(products);    
});

// Запрос 10
app.MapGet("/orders/products-summary", async (IProductService productService) =>
{
    IEnumerable<GetOrderInformationWithTotalAmountForEachProduct?> product = await
        productService.GetOrderInformationWithTotalAmountForEachProduct();
    if (product == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(product);
});
#endregion

#region Order
app.MapPost("/orders", async (Order order, IOrderService orderService) =>
{
    bool res = await orderService.Create(order);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось добавить!"});
    }
    return Results.Ok(new {message = "Успешно добавлено!"});
});

app.MapPut("/orders{id}", async (Guid id, Order order, IOrderService orderService) =>
{
    order.Id = id;
    bool res = await orderService.Update(order);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось изменить!"});
    }
    return Results.Ok(new {message = "Успешно изменено!"});
});

app.MapDelete("/orders/{id}", async (Guid id, IOrderService orderService) =>
{
    bool res = await orderService.Delete(id);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось удалить!"});
    }
    return Results.Ok(new {message = "Успешно удалено!"});
});

app.MapGet("/orders", async (IOrderService orderService) =>
{
    IEnumerable<OrderFull?> orders = await orderService.GetAll();
    if (orders == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(orders);
});

app.MapGet("/orders/{id}", async (Guid id, IOrderService orderService) =>
{
    OrderFull? order = await orderService.GetById(id);
    if (order == null)
    {
        return Results.NotFound(new {message = "Не найдено!"});
    }
    return Results.Ok(order);
});

// Запрос 4
app.MapGet("/orders/details", async (IOrderService orderService) =>
{
    IEnumerable<ReceivingAllOrdersWithInformationAboutCustomersAndProducts?> orders =
        await orderService.GetReceivingAllOrdersWithInformationAboutCustomersAndProducts();
    if (orders == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(orders);
});

// Запрос 5
app.MapGet("/orders/{status}&startDate={startDate}&endDate={endDate}",
async (string status, DateTime startDate, DateTime endDate, IOrderService orderService) =>
{
    IEnumerable<ReceivingOrdersFilteredByOrderStatusAndDate?> orders = await
        orderService.ReceivingOrdersFilteredByOrderStatusAndDate(status, startDate, endDate);
    if (orders == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(orders);
});

// Запрос 9
app.MapGet("/products/{productId}/orders", async (Guid productId, IOrderService orderService) =>
{
    IEnumerable<GetAllOrdersForASpecificProduct?> order = await 
        orderService.GetAllOrdersForASpecificProduct(productId);
    if (order == null!)
    {
        return Results.NotFound(new {message = "Не найдено!"});
    }
    return Results.Ok(order);
});
#endregion

#region OrderItems
app.MapPost("/orderitems", async (OrderItem orderItem, IOrderItemService orderItemService) =>
{
    bool res = await orderItemService.Create(orderItem);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось добавить!"});
    }
    return Results.Ok(new {message = "Успешно добавлено!"});
});

app.MapPut("/orderitems{id}", async (Guid id, OrderItem orderItem, IOrderItemService orderItemService) =>
{
    orderItem.Id = id;
    bool res = await orderItemService.Update(orderItem);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось изменить!"});
    }
    return Results.Ok(new {message = "Успешно изменено!"});
});

app.MapDelete("/orderitems/{id}", async (Guid id, IOrderItemService orderItemService) =>
{
    bool res = await orderItemService.Delete(id);
    if (res == false)
    {
        return Results.BadRequest(new {message = "Не удалось удалить!"});
    }
    return Results.Ok(new {message = "Успешно удалено!"});
});

app.MapGet("/orderitems/{id}", async (Guid id, IOrderItemService orderItemService) =>
{
    OrderItemFull? orderItem = await orderItemService.GetOrderItemById(id); 
    if (orderItem == null)
    {
        return Results.NotFound(new {message = "Не найдено!"});
    }
    return Results.Ok(orderItem);
});

app.MapGet("/orderitems", async (IOrderItemService orderItemService) =>
{
    IEnumerable<OrderItemFull?> orderItems = await orderItemService.GetAll();
    if (orderItems == null!)
    {
        return Results.NotFound(new {message = "Не удалось найти!"});
    }
    return Results.Ok(orderItems);
});

// Запрос 7
app.MapGet("/orders/month={month}&year={year}",
    async (int month, int year, IOrderItemService orderItemService) =>
    {
        IEnumerable<GettingSalesStatisticsForTheMonth?> orders = await
            orderItemService.GettingSalesStatisticsForTheMonth(month, year);
        if (orders == null!)
        {
            return Results.NotFound(new {message = "Не удалось найти!"});
        }
        return Results.Ok(orders);
    });
#endregion






app.Run();
