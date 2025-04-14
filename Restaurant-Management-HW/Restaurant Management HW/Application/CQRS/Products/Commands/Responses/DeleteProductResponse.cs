using Application.CQRS.Products.Commands.Requests;
using Common.GlobalResponse.Generics;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Commands.Responses;

public class DeleteProductResponse
{
    public string Message { get; set; }
}
