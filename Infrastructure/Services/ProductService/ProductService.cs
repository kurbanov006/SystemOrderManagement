using Dapper;
using Infrastructure.Models;
using Infrastructure.Models.Request;
using Npgsql;

namespace Infrastructure.Services.ProductService;

public class ProductService : IProductService
{
    public async Task<bool> Create(Product product)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlCreateProduct, product) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Product product)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.ExecuteAsync(SqlCommand.SqlUpdateProduct, product) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
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
                
                return await connection.ExecuteAsync(SqlCommand.SqlDeleteProduct, new {Id = id}) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<ProductFull?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryFirstOrDefaultAsync<ProductFull>(SqlCommand.SqlGetByProductId, new {Id = id});
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ProductFull?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<ProductFull>(SqlCommand.SqlGetAllProducts);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ProductFull?>> ReceivingProductsWithZeroQuantityInStock()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<ProductFull>(SqlCommand.ReceivingProductsWithZeroQuantityInStock);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<GettingTheMostPopularProductBySales?>> GettingTheMostPopularProductBySales()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GettingTheMostPopularProductBySales>(SqlCommand.GettingTheMostPopularProductBySales);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<GetOrderInformationWithTotalAmountForEachProduct?>> GetOrderInformationWithTotalAmountForEachProduct()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<GetOrderInformationWithTotalAmountForEachProduct>(SqlCommand
                    .GetOrderInformationWithTotalAmountForEachProduct);
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
    public const string SqlCreateProduct = @"insert into products(Name, Price, StockQuantity, CreatedAt)
                                            values(@Name, @Price, @StockQuantity, @CreatedAt);";
    public const string SqlUpdateProduct = @"update products set Name = @Name, Price = @Price, StockQuantity = @StockQuantity
                                               where Id = @Id";

    public const string SqlDeleteProduct = @"delete from products where Id = @Id";
    public const string SqlGetByProductId = @"select * from products where Id = @Id";
    public const string SqlGetAllProducts = @"select * from products";

    public const string ReceivingProductsWithZeroQuantityInStock = @"select id, name, price, stockquantity, createdat
                                                from products
                                                where stockquantity = 0";

    public const string GettingTheMostPopularProductBySales = @"select p.id, p.name, count(oi.id) as countproduct
                            from products p 
                            join orderitems oi on p.id = oi.productid
                            group by p.id
                            order by count(oi.id) desc
                            limit 1";

    public const string GetOrderInformationWithTotalAmountForEachProduct = @"select p.id, p.name, sum(oi.quantity * oi.price) as allsum
                                                        from products p
                                                        join orderitems oi on p.id = oi.productid 
                                                        group by p.id";
}