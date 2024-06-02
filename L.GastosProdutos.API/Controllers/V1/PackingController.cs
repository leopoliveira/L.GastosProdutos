using L.GastosProdutos.Core.Application.Handlers.Packing.V1.AddPacking;

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
    }
}
