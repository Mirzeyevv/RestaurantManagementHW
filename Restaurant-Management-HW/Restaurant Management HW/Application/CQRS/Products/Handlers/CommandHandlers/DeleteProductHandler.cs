using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Commands.Responses;
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

public class DeleteProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductRequest, ResponseModel<DeleteProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<ResponseModel<DeleteProductResponse>> Handle(DeleteProductRequest request, CancellationToken cancellationToken)
    {
        bool isTrue = await _unitOfWork.ProductRepository.Remove(request.Id, 0);

        if (!isTrue)
        {
            return new ResponseModel<DeleteProductResponse>
            {
                Data = new DeleteProductResponse { Message= "Couldnt delete"},
                Errors = ["Couldnt Delete"],
                IsSuccess = false
            };
        }

        return new ResponseModel<DeleteProductResponse>
        {
            Data = new DeleteProductResponse { Message = "Deleted Successfully!" },
            Errors = [],
            IsSuccess = true
        };
    }
}
