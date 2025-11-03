using L.GastosProdutos.Core.Application.Services;

using Microsoft.AspNetCore.Mvc;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.AddRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.DeleteRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe.All;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe.ById;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe;

namespace L.GastosProdutos.API.Controllers.V1
{
    public class RecipeController : CommonV1Controller
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<ActionResult<GetRecipeResponse>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<GetRecipeResponse>> GetRecipeById
        (
            string id,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetByIdAsync(id, cancellationToken);

            return Ok(response);
        }

        [HttpPost]
        public async Task<ActionResult<AddRecipeResponse>> CreateRecipe
        (
            AddRecipeRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _service.AddAsync(request, cancellationToken);

            return CreatedAtAction(nameof(GetRecipeById), new { id = response.RecipeId }, response);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateRecipe
        (
            string id,
            UpdateRecipeDto dto,
            CancellationToken cancellationToken
        )
        {
            await _service.UpdateAsync(id, dto, cancellationToken);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteRecipe
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

