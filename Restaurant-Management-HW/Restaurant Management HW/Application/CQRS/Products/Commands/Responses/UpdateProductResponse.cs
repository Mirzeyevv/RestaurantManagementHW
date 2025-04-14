using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Commands.Responses;

public class UpdateProductResponse
{
    public int Id { get; set; }
    public string Name { get; set; }

}
