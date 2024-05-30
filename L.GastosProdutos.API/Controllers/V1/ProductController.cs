using L.GastosProdutos.Core.Application.MediatR.Product.V1.AddProduct;
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
        public async Task<ActionResult> GetProductById(string id)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> CreateProduct(AddProductRequest request, CancellationToken cancellationToken)
        {
            var response = await _mediator.Send(request, cancellationToken);

            return CreatedAtAction(nameof(GetProductById), new { id = response.ProductId }, response);
        }
    }
}
