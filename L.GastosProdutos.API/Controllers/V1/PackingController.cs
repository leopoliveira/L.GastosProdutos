using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Application.Services;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.AddPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.DeletePacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking.All;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking.ById;
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

        [HttpGet()]
        public async Task<ActionResult<GetPackingResponse>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetAllAsync(cancellationToken);

            return Ok(response);
        }

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

