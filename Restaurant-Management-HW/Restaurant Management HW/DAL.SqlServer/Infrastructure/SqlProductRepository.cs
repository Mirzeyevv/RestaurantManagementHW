using DAL.SqlServer.Context;
using Dapper;
using Domain.Entities;
using Repository.Repositories;

namespace DAL.SqlServer.Infrastructure;

public class SqlProductRepository : BaseSqlRepository, IProductRepository
{
    private readonly AppDbContext _appDbContext;
    public SqlProductRepository(string connectionString, AppDbContext appDbContext) : base(connectionString)
    {
        _appDbContext = appDbContext;
    }

    public async Task AddAsync(Product product)
    {
        var sql = @"INSERT INTO Products ([Name], [CreatedBy])
            VALUES(@Name, @CreatedBy))";

        using var conn = OpenConnection();
        var generatedId = await conn.ExecuteScalarAsync<int>(sql, product);
    }

    public IQueryable<Product> GetAll()
    {
        return _appDbContext.Products.OrderByDescending(p => p.CreatedDate).Where(p=> p.IsDeleted == false);
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        var sql = @"SELECT p.* FROM Products p WHERE p.Id = @Id AND p.IsDeleted = 0";
        using var conn = OpenConnection();
        return await conn.QueryFirstOrDefaultAsync<Product>(sql, new { Id = id });
    }

    public async Task<IEnumerable<Product>> GetByNameAsync(string name)
    {
        var sql = @"DECLARE @searchText NVARCHAR(max)
                    SET @searchText = '%' + @name + '%'
                    SELECT c.* 
                    FROM Products c
                    WHERE c.[Name] LIKE @searchText AND c.IsDeleted = 0 ";

        using var conn = OpenConnection();
        return await conn.QueryAsync<Product>(sql, name);
    }

    public async Task<bool> Remove(int id, int DeletedBy)
    {
        var checkSql = @"SELECT Id FROM Products WHERE Id=@id AND IsDeleted=0";
        var sql = @"UPDATE Products
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

    public async Task Update(Product product)
    {
        var sql = @"UPDATE Products
                    SET Name=@Name,
                    UpdatedBy=@UpdatedBy,
                    UpdatedDate=GETDATE()
                    WHERE Id = @Id";

        using var conn = OpenConnection();

        await conn.QueryAsync<Product>(sql,product);
    }
}
