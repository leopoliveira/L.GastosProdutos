using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;



namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.UpdateRecipe
{
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

    public record UpdateRecipeDto
    (
        string Name,
        string? Description,
        List<IngredientDto> Ingredients,
        List<PackingDto> Packings,
        decimal? Quantity,
        decimal? SellingValue
    );
}
