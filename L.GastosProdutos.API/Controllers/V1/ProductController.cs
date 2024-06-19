using L.GastosProdutos.Core.Application.Handlers.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.DeleteProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.All;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.ById;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.UpdateProduct;
using L.GastosProdutos.Core.Domain.Enums;

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

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _mediator.Send
            (
                new GetAllProductRequest(),
                cancellationToken
            );

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            var response = await _mediator.Send
            (
                new GetProductByIdRequest(id),
                cancellationToken
            );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> CreateProduct
        (
            AddProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _mediator.Send
            (
                request,
                cancellationToken
            );

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateProduct
        (
            string id,
            UpdateProductDto dto,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send
            (
                new UpdateProductRequest
                (
                    id,
                    dto.Name,
                    dto.Price,
                    dto.Quantity,
                    (EnumUnitOfMeasure)dto.UnitOfMeasure
                ),
                cancellationToken
            );

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send
            (
                new DeleteProductRequest(id),
                cancellationToken
            );

            return Ok();
        }
    }
}
