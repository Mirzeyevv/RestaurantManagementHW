using Application.CQRS.Products.Queries.Requests;
using Application.CQRS.Products.Queries.Responses;
using Common.GlobalResponse.Generics;
using MediatR;
using Repository.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.CQRS.Products.Handlers.QueryHandlers;
public class GetByIdProductHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdProductRequest, ResponseModel<GetByIdProductResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<ResponseModel<GetByIdProductResponse>> Handle(GetByIdProductRequest request, CancellationToken cancellationToken)
    {
        var product = await _unitOfWork.ProductRepository.GetByIdAsync(request.id);

        if (product == null || product.IsDeleted)
        {
            return new ResponseModel<GetByIdProductResponse>(new List<string> { "Product not found." });
        }

        var response = new GetByIdProductResponse
        {
            Id = product.Id,
            Name = product.Name
        };

        return new ResponseModel<GetByIdProductResponse>
        {
            Data = response,
            IsSuccess = true
        };
    }
}
