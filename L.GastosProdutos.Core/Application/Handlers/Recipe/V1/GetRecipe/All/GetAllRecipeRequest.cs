﻿using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.GetRecipe.All
{
    public record GetAllRecipeRequest : IRequest<IEnumerable<GetRecipeResponse>>;
}
