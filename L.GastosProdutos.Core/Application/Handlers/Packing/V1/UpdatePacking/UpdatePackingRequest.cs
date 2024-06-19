using L.GastosProdutos.Core.Domain.Enums;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.UpdatePacking
{
    public record UpdatePackingRequest
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        EnumUnitOfMeasure UnitOfMeasure
    )
    : IRequest<Unit>;

    public record UpdatePackingDto
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity,
        int UnitOfMeasure
    );
}
