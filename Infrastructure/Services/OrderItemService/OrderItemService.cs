using Infrastructure.Models;
using Npgsql;
using Dapper;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.OrderItemService;

public class OrderItemService : IOrderItemService
{
    public async Task<bool> Create(OrderItem orderItem)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlCreateOrderItem, orderItem) > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<bool> Update(OrderItem orderItem)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlUpdateOrderItem, orderItem) > 0;
            }
        }
        catch (Exception e)
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
                
                return await connection.ExecuteAsync(SqlCommand.SqlDeleteOrderItem, new {Id = id}) > 0;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<OrderItemFull?> GetOrderItemById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryFirstOrDefaultAsync<OrderItemFull>(SqlCommand.SqlGetByOrderItemId, new {Id = id});
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<OrderItemFull?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<OrderItemFull>(SqlCommand.SqlGetAllOrderItems);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IEnumerable<GettingSalesStatisticsForTheMonth?>> GettingSalesStatisticsForTheMonth(int month, int year)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GettingSalesStatisticsForTheMonth>
                    (SqlCommand.GettingSalesStatisticsForTheMonth, new {Month = month, Year = year});
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

    public const string SqlCreateOrderItem = @"insert into Orderitems(OrderId, ProductId, Quantity, Price)
                                                values(@OrderId, @ProductId, @Quantity, @Price);";

    public const string SqlUpdateOrderItem = @"update Orderitems set OrderId = @OrderId, ProductId = @ProductId,
                                    Quantity = @Quantity, Price = @Price where Id = @id";
    public const string SqlDeleteOrderItem = @"delete from Orderitems where Id = @id";
    public const string SqlGetByOrderItemId = @"select * from Orderitems where Id = @Id";
    public const string SqlGetAllOrderItems = @"select * from Orderitems";

    public const string GettingSalesStatisticsForTheMonth = @"select sum(oi.quantity) as sumquantity
                                                            from orderitems oi
                                                            join orders o on oi.orderid = o.id
                                                            where
                                                            extract(month from o.orderdate) = @month 
                                                            and extract(year from o.orderdate) = @year";
}