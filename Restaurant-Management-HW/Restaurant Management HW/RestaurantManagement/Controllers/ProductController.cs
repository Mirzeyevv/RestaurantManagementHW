using Application.CQRS.Products.Commands.Requests;
using Application.CQRS.Products.Queries.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace RestaurantManagement.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController(ISender sender) : ControllerBase
{
    private readonly ISender _sender = sender;

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateProductRequest request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpDelete("{id}")]

    public async Task<IActionResult> Delete(int id)
    {
        var request = new DeleteProductRequest() { Id = id };
        return Ok(await _sender.Send(request));
    }

    [HttpPut] 
    public async Task<IActionResult> Update([FromBody] UpdateProductRequest request)
    {
        return Ok(await _sender.Send(request));
    }

    [HttpGet("{id}")]

    public async Task<IActionResult> GetById(int id)
    {
        var request = new GetByIdProductRequest() { id = id };
        return Ok(await _sender.Send(request));
    }


}
