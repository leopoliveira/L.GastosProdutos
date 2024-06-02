using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.DeletePacking
{
    public class DeletePackingHandler : IRequestHandler<DeletePackingRequest, Unit>
    {
        private readonly IPackingRepository _repository;

        public DeletePackingHandler(IPackingRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle
        (
            DeletePackingRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
