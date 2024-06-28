using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;
using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.AddRecipe
{
    public record AddRecipeRequest
    (
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal? Quantity,
        decimal? SellingValue
    )
    : IRequest<AddRecipeResponse>;
}
