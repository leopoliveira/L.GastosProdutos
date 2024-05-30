using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.DeleteProduct
{
    public class DeleteProductHandler : IRequestHandler<DeleteProductRequest, Unit>
    {
        private readonly IProductRepository _repository;

        public DeleteProductHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle
        (
            DeleteProductRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
