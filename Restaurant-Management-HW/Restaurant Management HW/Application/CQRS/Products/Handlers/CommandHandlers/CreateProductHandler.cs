using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;
using Domain.Entities;


namespace Application.CQRS.Products.Handlers.CommandHandlers;

public class CreateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<CreateProductRequest, ResponseModel<CreateProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseModel<CreateProductResponse>> Handle(CreateProductRequest request, CancellationToken cancellationToken)
    {
        Product newProduct = new()
        {
            Name = request.Name,
            
        };

        if(string.IsNullOrEmpty (request.Name))
        {
            return new ResponseModel<CreateProductResponse>
            {
                Data = null,
                Errors = new List<string> { "Product name cannot be empty." },
                IsSuccess = false
            };
        }

        await _unitOfWork.ProductRepository.AddAsync(newProduct);

        CreateProductResponse response = new()
        {
            Id = newProduct.Id,
            Name = request.Name

        };

        return new ResponseModel<CreateProductResponse>
        {
            Data = response

        };
    }
}
