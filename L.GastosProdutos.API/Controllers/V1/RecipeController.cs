using L.GastosProdutos.Core.Application.Services;

using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.AddRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe;

namespace L.GastosProdutos.API.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces(MediaTypeNames.Application.Json)]
    [Consumes(MediaTypeNames.Application.Json)]
    public class RecipeController : ControllerBase
    {
        private readonly IRecipeService _service;

        public RecipeController(IRecipeService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns all recipes.
        /// </summary>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>List of recipes.</returns>
        [HttpGet]
        public async Task<ActionResult<GetRecipeResponse>> GetAll
        (
            CancellationToken cancellationToken
        )
        {
            var response = await _service.GetAllAsync(cancellationToken);

            return Ok(response);
        }

        /// <summary>
        /// Returns a recipe by its identifier.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Recipe data.</returns>
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

        /// <summary>
        /// Creates a new recipe.
        /// </summary>
        /// <param name="request">Recipe creation data.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Created recipe information.</returns>
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

        /// <summary>
        /// Updates an existing recipe.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <param name="dto">Recipe fields to update.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Status of the operation.</returns>
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

        /// <summary>
        /// Deletes a recipe.
        /// </summary>
        /// <param name="id">Recipe identifier.</param>
        /// <param name="cancellationToken">Request cancellation token.</param>
        /// <returns>Status of the operation.</returns>
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
