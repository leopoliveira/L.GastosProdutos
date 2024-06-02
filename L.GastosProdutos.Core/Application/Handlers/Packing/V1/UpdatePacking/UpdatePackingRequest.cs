using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.UpdatePacking
{
    public record UpdatePackingRequest
    (
        string Id,
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity
    )
    : IRequest<Unit>;

    public record UpdatePackingDto
    (
        string Name,
        string? Description,
        decimal Price,
        decimal Quantity
    );
}
