using L.GastosProdutos.Core.Application.Handlers.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.DeleteProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.All;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.ById;
using L.GastosProdutos.Core.Application.Handlers.Product.V1.UpdateProduct;
using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Application.Services;

using Microsoft.AspNetCore.Mvc;

namespace L.GastosProdutos.API.Controllers.V1
{
    public class ProductController : CommonV1Controller
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetProductById
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> CreateProduct
        (
            AddProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.AddAsync(request, cancellationToken);

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
            await _service.UpdateAsync(id, dto, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteProduct
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            await _service.DeleteAsync(id, cancellationToken);

            return Ok();
        }
    }
}
