using L.GastosProdutos.Core.Domain.Enums;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.AddPacking
{
    public record AddPackingRequest
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    )
    : IRequest<AddPackingResponse>;
}
