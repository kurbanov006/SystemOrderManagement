using Infrastructure.Models;
using Npgsql;
using Dapper;
using Infrastructure.Models.Request;

namespace Infrastructure.Services.CustomerService;

public class CustomerService : ICustomerService
{
    public async Task<bool> Create(Customer customer)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlCreateCustomer, customer) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<bool> Update(Customer customer)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.ExecuteAsync(SqlCommand.SqlUpdateCustomer, customer) > 0;
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

                return await connection.ExecuteAsync(SqlCommand.SqlDeleteCustomer, new {Id = id}) > 0;
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<CustomerFull?> GetById(Guid id)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryFirstOrDefaultAsync<CustomerFull>(SqlCommand.SqlGetByCustomerId,
                    new { Id = id });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<CustomerFull?>> GetAll()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<CustomerFull>(SqlCommand.SqlGetAllCustomers);
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<ReceivingCustomersWithOrdersForACertainPeriod?>> GetReceivingCustomersWithOrdersForACertainPeriod(DateTime startDate, DateTime endDate)
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();

                return await connection.QueryAsync<ReceivingCustomersWithOrdersForACertainPeriod>(SqlCommand.ReceivingCustomersWithOrdersForACertainPeriod,
                    new { StartDate = startDate, EndDate = endDate });
            }
        }
        catch (NpgsqlException e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    public async Task<IEnumerable<GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient?>> GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient()
    {
        try
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(SqlCommand.ConnectionString))
            {
                await connection.OpenAsync();
                
                return await connection.QueryAsync<GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient>(SqlCommand.GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient);
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

    public const string SqlCreateCustomer = @"insert into customers(FullName, Email, Phone)
                                            values(@FullName, @Email, @Phone);";

    public const string SqlUpdateCustomer = @"update customers set FullName = @FullName, Email = @Email, Phone = @Phone
                                               where Id = @Id";
    public const string SqlDeleteCustomer = @"delete from customers where Id = @Id";
    public const string SqlGetByCustomerId = @"select * from customers where Id = @Id";
    public const string SqlGetAllCustomers = @"select * from customers";
    
    public const string ReceivingCustomersWithOrdersForACertainPeriod = @"select c.id, c.fullname, c.email,
                                                o.id as orderid, o.totalAmount, o.orderdate
                                                from customers c
                                                join orders o on c.id = o.customerid
                                              where o.orderdate between @StartDate and @EndDate";

    public const string GetTheTotalNumberOfOrdersAndTheTotalAmountForEachClient = 
        @"select c.id, c.fullname, c.email, c.phone, c.createdat, count(o.id) as countorder,
          sum(o.totalamount) as sumorder
          from customers c
          join orders o on c.id = o.customerid
          group by c.id";
    
}



