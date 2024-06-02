﻿using L.GastosProdutos.Core.Application.Handlers.Packing.V1.AddPacking;
using L.GastosProdutos.Core.Application.Handlers.Packing.V1.DeletePacking;
using L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking.ById;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace L.GastosProdutos.API.Controllers.V1
{
    public class PackingController : CommonV1Controller
    {
        private readonly IMediator _mediator;

        public PackingController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetPackingByIdResponse>> GetById
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            var response = await _mediator.Send
            (
                new GetPackingByIdRequest(id),
                cancellationToken
            );

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult> CreatePacking
        (
            AddPackingRequest request,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send
            (
                request,
                cancellationToken
            );

            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeletePacking
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            await _mediator.Send
            (
                new DeletePackingRequest(id),
                cancellationToken
            );

            return Ok();
        }
    }
}