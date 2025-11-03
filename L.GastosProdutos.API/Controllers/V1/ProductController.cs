using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Application.Services;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.AddProduct;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.UpdateProduct;

namespace L.GastosProdutos.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _service;

        public ProductController(IProductService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all products.
        /// </summary>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>List of products.</returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GetProductResponse>>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Returns a product by its identifier.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Product data.</returns>
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

        /// <summary>
        /// Creates a new product.
        /// </summary>
        /// <param name="request">Product creation data.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Created product information.</returns>
        [HttpPost]
        public async Task<ActionResult<AddProductResponse>> CreateProduct
        (
            AddProductRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.AddAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetProductById), new { id = response.ProductId }, response);
        }

        /// <summary>
        /// Updates an existing product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="dto">Product fields to update.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Status of the operation.</returns>
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

        /// <summary>
        /// Deletes a product.
        /// </summary>
        /// <param name="id">Product identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Status of the operation.</returns>
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
