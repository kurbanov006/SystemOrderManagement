using Infrastructure.Models;
using Npgsql;
using Dapper;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.OrderService;

public class OrderService : IOrderService
{
    public async Task<bool> Create(Order order)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreateOrder, order) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Update(Order order)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlUpdateOrder, order) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Delete(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlDeleteOrder, new {Id = id}) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OrderFull?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<OrderFull>(SqlCommand.SqlGetByOrderId, new {Id = id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<OrderFull?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<OrderFull>(SqlCommand.SqlGetAllOrders);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<ReceivingAllOrdersWithInformationAboutCustomersAndProducts?>> GetReceivingAllOrdersWithInformationAboutCustomersAndProducts()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<ReceivingAllOrdersWithInformationAboutCustomersAndProducts>(SqlCommand.ReceivingAllOrdersWithInformationAboutCustomersAndProducts);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ReceivingOrdersFilteredByOrderStatusAndDate?>> ReceivingOrdersFilteredByOrderStatusAndDate(string status, DateTime startDate, DateTime endDate)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<ReceivingOrdersFilteredByOrderStatusAndDate>(
                    SqlCommand.ReceivingOrdersFilteredByOrderStatusAndDate,
                    new {Status = status, StartDate = startDate, EndDate = endDate});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<GetAllOrdersForASpecificProduct?>> GetAllOrdersForASpecificProduct(Guid productId)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GetAllOrdersForASpecificProduct>(SqlCommand.GetAllOrdersForASpecificProduct, new {ProductId = productId});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }
}

file class SqlCommand
{
    public const string ConnectionString = @"Server=localhost; Port=5432; Database=OrderManagementSystems_db; 
                                            User Id=postgres; Password=12345";

    public const string SqlCreateOrder = @"insert into Orders(CustomerId, TotalAmount, OrderDate, Status)
                                                 values(@CustomerId, @TotalAmount, @OrderDate, @Status)";

    public const string SqlUpdateOrder = @"update Orders set CustomerId = @CustomerId, TotalAmount = @TotalAmount,
                                                            OrderDate = @OrderDate, Status = @Status where Id = @id";
    public const string SqlDeleteOrder = @"delete from Orders where Id = @Id";
    public const string SqlGetByOrderId = @"select * from Orders where Id = @Id";
    public const string SqlGetAllOrders = @"select * from Orders";

    public const string ReceivingAllOrdersWithInformationAboutCustomersAndProducts = @"
                                                                    select o.id as orderid, 
                                                                    o.totalamount, o.orderdate, 
                                                                    c.id as customerid, c.fullname, 
                                                                    p.id as productid, p.name
                                                                    from orders o 
                                                                    join customers c on o.customerid = c.id
                                                                    join orderitems oi on oi.orderid = o.id
                                                                    join products p on p.id = oi.productid";

    public const string ReceivingOrdersFilteredByOrderStatusAndDate = @"select o.id as orderid, o.totalamount, o.orderdate, o.status, 
                                                    c.id as customerid, c.fullname, c.email 
                                                    from orders o 
                                                    join customers c on c.id = o.customerid
                                                    where o.status = @Status
                                                    and o.orderdate between @startdate and @enddate";

    public const string GetAllOrdersForASpecificProduct = @"select o.id as orderid, o.customerid, o.totalamount, o.orderdate, o.status
                                                            from orders o
                                                            join orderitems oi on o.id = oi.orderid
                                                            where oi.productid = @Productid
                                                            ";


}