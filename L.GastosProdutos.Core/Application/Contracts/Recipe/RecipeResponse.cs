using System.Collections.Generic;

namespace L.GastosProdutos.Core.Application.Contracts.Recipe
{
    public record RecipeResponse
    (
        string Id,
        string Name,
        string? Description,
        List<V1.Dto.IngredientDto> Ingredients,
        List<V1.Dto.PackingDto> Packings,
        decimal TotalCost,
        decimal? Quantity,
        decimal? SellingValue,
        string? GroupId,
        string? GroupName
    );
}

