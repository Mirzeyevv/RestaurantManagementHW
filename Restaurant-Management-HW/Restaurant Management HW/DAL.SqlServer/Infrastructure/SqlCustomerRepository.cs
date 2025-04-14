using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlCustomerRepository : BaseSqlRepository, ICustomerRepository
{
    private readonly AppDbContext _appDbContext;
    public SqlCustomerRepository(string connectionString, AppDbContext appDbContext) : base(connectionString)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Customer customer)
    {
        var sql = @"INSERT INTO Customers ([Name], [CreatedBy])
            VALUES(@Name, @CreatedBy))";

        using var conn = OpenConnection();
        var generatedId = await conn.ExecuteScalarAsync<int>(sql, customer);
    }

    public IQueryable<Customer> GetAll()
    {
        return _appDbContext.Customers.OrderByDescending(p => p.CreatedDate).Where(p => p.IsDeleted == false);
    }

    public async Task<Customer> GetByIdAsync(int id)
    {
        var sql = @"SELECT p.* FROM Customers p WHERE p.Id = @Id AND p.IsDeleted = 0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<Customer>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Customer>> GetByNameAsync(string name)
    {
        var sql = @"DECLARE @searchText NVARCHAR(max)
                    SET @searchText = '%' + @name + '%'
                    SELECT c.* 
                    FROM Customers c
                    WHERE c.[Name] LIKE @searchText AND c.IsDeleted = 0 ";

        using var conn = OpenConnection();
        return await conn.QueryAsync<Customer>(sql, name);
    }

    public async Task<bool> Remove(int id, int DeletedBy)
    {
        var checkSql = @"SELECT Id FROM Customers WHERE Id=@id AND IsDeleted=0";
        var sql = @"UPDATE Customers
                    SET IsDeleted=1
                    DeletedBy=@deletedBy
                    DeletedDate=GETDATE()
                    WHERE Id = @id";

        using var conn = OpenConnection();
        using var transaction = conn.BeginTransaction();
        var categoryId = await conn.ExecuteScalarAsync<int?>(checkSql, new { id }, transaction);

        if (!categoryId.HasValue)
        {
            return false;
        }

        var affectedRow = await conn.ExecuteAsync(sql, new { id, DeletedBy }, transaction);

        transaction.Commit();

        return affectedRow > 0;

    }

    public async Task Update(Customer customer)
    {
        var sql = @"UPDATE Customers
                    SET Name=@Name,
                    UpdatedBy=@UpdatedBy,
                    UpdatedDate=GETDATE()
                    WHERE Id = @Id";

        using var conn = OpenConnection();

        await conn.QueryAsync<Customer>(sql, customer);
    }

    IQueryable<Product> ICustomerRepository.GetAll()
    {
        throw new NotImplementedException();
    }

    Task<Product> ICustomerRepository.GetByIdAsync(int id)
    {
        throw new NotImplementedException();
    }

    Task<IEnumerable<Product>> ICustomerRepository.GetByNameAsync(string name)
    {
        throw new NotImplementedException();
    }
}
