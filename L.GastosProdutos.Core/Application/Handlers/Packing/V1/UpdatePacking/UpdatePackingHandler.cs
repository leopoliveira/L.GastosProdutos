using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

using MongoDB.Driver;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.UpdatePacking
{
    public class UpdatePackingHandler : IRequestHandler<UpdatePackingRequest, Unit>
    {
        private readonly IPackingRepository _repository;
        private readonly IRecipeRepository _recipeRepository;

        public UpdatePackingHandler(IPackingRepository repository, IRecipeRepository recipeRepository)
        {
            _repository = repository;
            _recipeRepository = recipeRepository;
        }

        public async Task<Unit> Handle
        (
            UpdatePackingRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var packing = await _repository.GetByIdAsync(request.Id)
                ?? throw new NotFoundException("Packing not found");

            MapRequestToEntity(request, packing);

            await _repository.UpdateAsync(request.Id, packing);

            await UpdateIngredientsPackingUnitPrice(request.Id, packing.UnitPrice);

            return Unit.Value;
        }

        private static void MapRequestToEntity(UpdatePackingRequest request, PackingEntity packing)
        {
            packing.Name = request.Name;
            packing.Description = request.Description;
            packing.Price = request.Price;
            packing.Quantity = request.Quantity;
            packing.UnitOfMeasure = request.UnitOfMeasure;
        }

        private async Task UpdateIngredientsPackingUnitPrice(string id, decimal packingUnitPrice)
        {
            var filter = Builders<RecipeEntity>
                .Filter
                .ElemMatch
                (
                    r => r.Packings,
                    p => p.PackingId == id
                );

            var update = Builders<RecipeEntity>
                .Update
                .Set
                (
                    "Packings.$[].Cost",
                    packingUnitPrice
                );

            await _recipeRepository.UpdateMany(filter, update);
        }
    }
}
