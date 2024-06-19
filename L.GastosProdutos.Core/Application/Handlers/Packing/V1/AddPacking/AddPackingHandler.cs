using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.AddPacking
{
    public class AddPackingHandler : IRequestHandler<AddPackingRequest, AddPackingResponse>
    {
        private readonly IPackingRepository _repository;

        public AddPackingHandler(IPackingRepository repository)
        {
            _repository = repository;
        }

        public async Task<AddPackingResponse> Handle
        (
            AddPackingRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            var packing = new PackingEntity
            (
                request.Name,
                request.Description,
                request.Price,
                request.Quantity,
                (EnumUnitOfMeasure)request.UnitOfMeasure
            );

            await _repository.CreateAsync(packing);

            return new AddPackingResponse(packing.Id);
        }
    }
}
