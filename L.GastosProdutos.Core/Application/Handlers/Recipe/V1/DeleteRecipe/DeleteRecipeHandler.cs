using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.DeleteRecipe
{
    public class DeleteRecipeHandler : IRequestHandler<DeleteRecipeRequest, Unit>
    {
        private readonly IRecipeRepository _repository;

        public DeleteRecipeHandler(IRecipeRepository repository)
        {
            _repository = repository;
        }

        public async Task<Unit> Handle
        (
            DeleteRecipeRequest request,
            CancellationToken cancellationToken
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            await _repository.DeleteAsync(request.Id);

            return Unit.Value;
        }
    }
}
