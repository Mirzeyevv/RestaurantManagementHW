using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResponse.Generics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Commands.Requests;

public class UpdateProductRequest : IRequest<ResponseModel<UpdateProductResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
}
