using L.GastosProdutos.Core.Application.MediatR.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.MediatR.Product.V1.GetProduct.ById;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace L.GastosProdutos.API.Controllers.V1
{
    public class ProductController : CommonV1Controller
    {
        private readonly IMediator _mediator;

        public ProductController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById(string id, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send
            (
                new GetProductByIdRequest(id),
                cancellationToken
            );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> CreateProduct(AddProductRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send
            (
                request,
                cancellationToken
            );

            return StatusCode(StatusCodes.Status201Created);
        }
    }
}
