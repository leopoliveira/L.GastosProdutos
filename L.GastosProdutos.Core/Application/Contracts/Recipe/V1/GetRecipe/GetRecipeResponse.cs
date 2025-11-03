using System.Collections.Generic;
using L.GastosProdutos.Core.Application.Contracts.Recipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;

namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe
{
    public record GetRecipeResponse(
        string Id,
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal TotalCost,
        decimal? Quantity,
        decimal? SellingValue
    ) : RecipeResponse(Id, Name, Description, Ingredients, Packings, TotalCost, Quantity ?? 0, SellingValue ?? 0);
}

