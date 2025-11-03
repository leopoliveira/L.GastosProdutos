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

    public record RecipeResponse
    (
        string Id,
        string Name,
        string? Description,
        List<V1.Dto.IngredientDto> Ingredients,
        List<V1.Dto.PackingDto> Packings,
        decimal TotalCost,
        decimal? Quantity,
        decimal? SellingValue
    );
}

namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto
{
    public record IngredientDto
    (
        string ProductId,
        string ProductName,
        decimal Quantity,
        decimal IngredientPrice
    );

    public record PackingDto
    (
        string PackingId,
        string PackingName,
        decimal Quantity,
        decimal PackingUnitPrice
    );
}

namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.AddRecipe
{
    using L.GastosProdutos.Core.Application.Contracts.Recipe;
    using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;

    public record AddRecipeRequest(string Name, string? Description, List<IngredientDto> Ingredients, List<PackingDto> Packings, decimal? Quantity, decimal? SellingValue)
        : RecipeWriteDto(Name, Description, Ingredients, Packings, Quantity, SellingValue);

    public record AddRecipeResponse(string RecipeId);
}

namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe
{
    using System.Collections.Generic;
    using L.GastosProdutos.Core.Application.Contracts.Recipe;
    using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;

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

namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.UpdateRecipe
{
    using System.Collections.Generic;
    using L.GastosProdutos.Core.Application.Contracts.Recipe;
    using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;

    public record UpdateRecipeRequest
    (
        string Id,
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal? Quantity,
        decimal? SellingValue
    );

    public record UpdateRecipeDto(string Name, string? Description, List<IngredientDto> Ingredients, List<PackingDto> Packings, decimal? Quantity, decimal? SellingValue)
        : RecipeWriteDto(Name, Description, Ingredients, Packings, Quantity, SellingValue);
}

