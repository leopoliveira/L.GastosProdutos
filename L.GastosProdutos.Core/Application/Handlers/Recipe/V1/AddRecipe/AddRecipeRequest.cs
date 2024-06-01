using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;
using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.AddRecipe
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
