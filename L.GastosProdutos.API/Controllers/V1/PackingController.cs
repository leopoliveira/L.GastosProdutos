using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Application.Services;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.AddPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.UpdatePacking;

namespace L.GastosProdutos.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class PackingController : ControllerBase
    {
        private readonly IPackingService _service;

        public PackingController(IPackingService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all packings.
        /// </summary>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>List of packings.</returns>
        [HttpGet()]
        public async Task<ActionResult<GetPackingResponse>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Returns a packing by its identifier.
        /// </summary>
        /// <param name="id">Packing identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Packing data.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<GetPackingResponse>> GetById
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Creates a new packing.
        /// </summary>
        /// <param name="request">Packing creation data.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Created packing information.</returns>
        [HttpPost]
        public async Task<ActionResult> CreatePacking
        (
            AddPackingRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.AddAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetById), new { id = response.PackingID }, response);
        }

        /// <summary>
        /// Updates an existing packing.
        /// </summary>
        /// <param name="id">Packing identifier.</param>
        /// <param name="dto">Packing fields to update.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Status of the operation.</returns>
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdatePacking
        (
            string id,
            UpdatePackingDto dto,
            CancellationToken cancellationToken
        )
        {
            await _service.UpdateAsync(id, dto, cancellationToken);

            return Ok();
        }

        /// <summary>
        /// Deletes a packing.
        /// </summary>
        /// <param name="id">Packing identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Status of the operation.</returns>
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePacking
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
