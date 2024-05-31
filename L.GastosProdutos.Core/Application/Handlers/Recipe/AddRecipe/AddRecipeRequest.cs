using L.GastosProdutos.Core.Application.Handlers.Recipe.ValueObjects;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.AddRecipe
{
    public record AddRecipeRequest
    (
        string Name,
        string? Description,
        List<IngredientWriteDto> Ingredients,
        List<PackingWriteDto> Packings
    )
    : IRequest<AddRecipeResponse>;
}
