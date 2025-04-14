using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
using Azure;
using Common.GlobalResponse.Generics;
using Domain.Entities;
using MediatR;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Handlers.CommandHandlers;

public class UpdateProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateProductRequest, ResponseModel<UpdateProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseModel<UpdateProductResponse>> Handle(UpdateProductRequest request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
        {
            return new ResponseModel<UpdateProductResponse>
            {
                IsSuccess = false,
                Errors = new List<string> { "Product name cannot be empty." }
            };
        }

        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.Id);

        if (product == null)
        {
            return new ResponseModel<UpdateProductResponse>
            {
                IsSuccess = false,
                Errors = new List<string> { "Product not found." }
            };
        }

        product.Name = request.Name;

        await _unitOfWork.ProductRepository.Update(product);

        var response = new UpdateProductResponse
        {
            Id = product.Id,
            Name = product.Name
        };

        return new ResponseModel<UpdateProductResponse>
        {
            IsSuccess = true,
            Data = response
        };
    }
}
