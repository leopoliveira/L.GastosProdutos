using System.Collections.Generic;
using System.Linq;
using L.GastosProdutos.Core.Application.Contracts.Product.V1.GetProduct;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.GetRecipe;
using L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Recipe;

namespace L.GastosProdutos.Core.Application.Services.Mappers
{
    public static class EntityToResponseMapper
    {
        public static GetProductResponse ToResponse(this ProductEntity p) => new(
            p.Id,
            p.Name,
            p.Price,
            p.Quantity,
            p.UnitPrice,
            p.UnitOfMeasure
        );

        public static GetPackingResponse ToResponse(this PackingEntity p) => new(
            p.Id,
            p.Name,
            p.Description,
            p.Price,
            p.Quantity,
            p.UnitPrice,
            p.UnitOfMeasure
        );

        public static GetRecipeResponse ToResponse(this RecipeEntity recipe)
        {
            return new GetRecipeResponse(
                recipe.Id,
                recipe.Name,
                recipe.Description,
                recipe.Ingredients.ConvertAll(i => new IngredientDto(
                    i.ProductId,
                    i.ProductName,
                    i.Quantity,
                    i.IngredientPrice
                )),
                recipe.Packings.ConvertAll(p => new PackingDto(
                    p.PackingId,
                    p.PackingName,
                    p.Quantity,
                    p.UnitPrice
                )),
                recipe.TotalCost,
                recipe.Quantity ?? 0,
                recipe.SellingValue ?? 0
            );
        }
    }
}

