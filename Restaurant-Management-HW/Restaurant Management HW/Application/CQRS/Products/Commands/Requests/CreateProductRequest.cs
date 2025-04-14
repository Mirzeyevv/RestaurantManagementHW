using MediatR;
using Common.GlobalResponse.Generics;
using Application.CQRS.Products.Commands.Responses;

namespace Application.CQRS.Products.Commands.Requests;

public class CreateProductRequest : IRequest<ResponseModel<CreateProductResponse>>
{
    public string Name { get; set; }
}
