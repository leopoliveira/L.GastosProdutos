using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.DeleteRecipe
{
    public record DeleteRecipeRequest(string Id)
        : IRequest<Unit>;
}
