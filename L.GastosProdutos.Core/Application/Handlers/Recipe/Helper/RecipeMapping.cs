using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto;
using L.GastosProdutos.Core.Application.Handlers.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Domain.Entities.Recipe;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.Helper
{
    public static class RecipeMapping
    {
        public static GetRecipeResponse MapEntityToResponse(RecipeEntity recipe)
        {
            return new GetRecipeResponse
            (
                recipe.Id,
                recipe.Name,
                recipe.Description,
                recipe.Ingredients.ConvertAll(i =>
                new IngredientDto
                (
                    i.ProductId,
                    i.ProductName,
                    i.Quantity,
                    i.IngredientPrice
                )),
                recipe.Packings.ConvertAll(p =>
                new PackingDto
                (
                    p.PackingId,
                    p.PackingName,
                    p.Quantity,
                    p.UnitPrice
                )),
                recipe.TotalCost
            );
        }
    }
}