using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

using MongoDB.Driver;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.UpdateProduct
{
    public class UpdateProductHandler : IRequestHandler<UpdateProductRequest, Unit>
    {
        private readonly IProductRepository _repository;
        private readonly IRecipeRepository _recipeRepository;

        public UpdateProductHandler
        (
            IProductRepository repository,
            IRecipeRepository recipeRepository
        )
        {
            _repository = repository;
            _recipeRepository = recipeRepository;
        }

        public async Task<Unit> Handle
        (
            UpdateProductRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var product = await _repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Product not found.");

            MapRequestToEntity(request, product);

            await _repository.UpdateAsync(request.Id, product);

            await UpdateIngredientsPrice(request.Id, product.UnitPrice);

            return Unit.Value;
        }

        private static void MapRequestToEntity
        (
            UpdateProductRequest request,
            ProductEntity product
        )
        {
            product.Name = request.Name;
            product.Price = request.Price;
            product.Quantity = request.Quantity;
            product.UnitOfMeasure = request.UnitOfMeasure;
        }

        private async Task UpdateIngredientsPrice(string id, decimal ingredientPrice)
        {
            var filter = Builders<RecipeEntity>
                .Filter
                .ElemMatch
                (
                    r => r.Ingredients,
                    i => i.ProductId == id
                );

            var update = Builders<RecipeEntity>
                .Update
                .Set
                (
                    "Ingredients.$[].IngredientPrice",
                    ingredientPrice
                );

            await _recipeRepository.UpdateMany(filter, update);
        }
    }
}
