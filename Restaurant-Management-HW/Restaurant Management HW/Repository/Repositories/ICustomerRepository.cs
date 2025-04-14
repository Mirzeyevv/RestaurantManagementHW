using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories;

public interface ICustomerRepository
{
    Task AddAsync(Customer customer);
    Task Update(Customer customer);
    Task<bool> Remove(int id, int DeletedBy);
    IQueryable<Product> GetAll();

    Task<Product> GetByIdAsync(int id);

    Task<IEnumerable<Product>> GetByNameAsync(string name);
}
