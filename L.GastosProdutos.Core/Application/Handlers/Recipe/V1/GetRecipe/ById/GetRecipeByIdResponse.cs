using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.GetRecipe.ById
{
    public record GetRecipeByIdResponse
    (
        string Id,
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal TotalCost
    );
}
