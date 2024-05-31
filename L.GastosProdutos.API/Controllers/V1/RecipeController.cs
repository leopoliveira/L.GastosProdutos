using L.GastosProdutos.Core.Application.Handlers.Recipe.AddRecipe;

using MediatR;

using Microsoft.AspNetCore.Mvc;

namespace L.GastosProdutos.API.Controllers.V1
{
    public class RecipeController : CommonV1Controller
    {
        private readonly IMediator _mediator;

        public RecipeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<ActionResult<AddRecipeResponse>> CreateRecipe
        (
            AddRecipeRequest request,
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
    }
}
