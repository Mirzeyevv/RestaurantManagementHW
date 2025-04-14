using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Queries.Responses;

public class GetByIdProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    
}
