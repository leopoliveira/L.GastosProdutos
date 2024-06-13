using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking.All
{
    public record GetAllPackingRequest : IRequest<IEnumerable<GetPackingResponse>>;
}
