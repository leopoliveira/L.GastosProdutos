using System.Collections.Generic;

namespace L.GastosProdutos.Core.Application.Contracts.Recipe
{
    public record RecipeWriteDto
    (
        string Name,
        string? Description,
        List<V1.Dto.IngredientDto> Ingredients,
        List<V1.Dto.PackingDto> Packings,
        decimal? Quantity,
        decimal? SellingValue
    );
}

