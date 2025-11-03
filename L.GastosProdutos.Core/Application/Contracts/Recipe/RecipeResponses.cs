using System.Collections.Generic;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;

namespace L.GastosProdutos.Core.Application.Contracts.Recipe
{
    public record RecipeResponse
    (
        string Id,
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal TotalCost,
        decimal? Quantity,
        decimal? SellingValue
    );
}
