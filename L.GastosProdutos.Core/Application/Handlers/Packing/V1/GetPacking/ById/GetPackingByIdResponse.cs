﻿namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking.ById
{
    public record GetPackingByIdResponse
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity
    );
}