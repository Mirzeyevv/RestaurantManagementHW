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

public class SqlUnitOfWork(AppDbContext context, string connectionString) : IUnitOfWork
{

    private readonly string _connectionString = connectionString;

    private readonly AppDbContext _context = context;

    public SqlProductRepository _sqlProductRepository;
    public SqlCustomerRepository _sqlCustomerRepository;
    public IProductRepository ProductRepository => _sqlProductRepository ?? new SqlProductRepository(_connectionString, _context);
    public ICustomerRepository CustomerRepository => _sqlCustomerRepository ?? new SqlCustomerRepository(_connectionString, _context);

    Task<int> IUnitOfWork.SaveChanges()
    {
        throw new NotImplementedException();
    }
}
