﻿using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Product.V1.GetProduct.All
{
    public record GetAllProductRequest : IRequest<IEnumerable<GetProductResponse>>;
}
