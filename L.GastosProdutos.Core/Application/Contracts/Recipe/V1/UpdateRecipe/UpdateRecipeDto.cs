using System.Collections.Generic;
using L.GastosProdutos.Core.Application.Contracts.Recipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;

namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe
{
    public record UpdateRecipeDto(
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal? Quantity,
        decimal? SellingValue)
        : RecipeWriteDto(Name, Description, Ingredients, Packings, Quantity, SellingValue);
}

