using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.SqlServer.Context;
using DAL.SqlServer.Infrastructure;
using Repository.Common;
using Repository.Repositories;

namespace DAL.SqlServer.UnitOfWork;

public class SqlUnitOfWork(AppDbContext appDbContext, string connectionString) : IUnitOfWork
{

    private readonly string _connectionString = connectionString;

    private readonly AppDbContext _appDbContext = appDbContext;

    public SqlProductRepository _sqlProductRepository;
    public SqlCustomerRepository _sqlCustomerRepository;
    public IProductRepository ProductRepository => _sqlProductRepository ?? new SqlProductRepository(connectionString, appDbContext);
    public ICustomerRepository CustomerRepository => _sqlCustomerRepository ?? new SqlCustomerRepository(connectionString, appDbContext);

    Task<int> IUnitOfWork.SaveChanges()
    {
        throw new NotImplementedException();
    }
}
