using Application.CQRS.Products.Queries.Responses;
using Common.GlobalResponse.Generics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Queries.Requests;

public class GetByIdProductRequest : IRequest<ResponseModel<GetByIdProductResponse>>
{
    public int id { get; set; }
}
