using L.GastosProdutos.Core.Interfaces;

using MediatR;

namespace L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking.All
{
    public class GetAllPackingHandler : IRequestHandler<GetAllPackingRequest, IEnumerable<GetPackingResponse>>
    {
        private readonly IPackingRepository _packingRepository;

        public GetAllPackingHandler(IPackingRepository packingRepository)
        {
            _packingRepository = packingRepository;
        }

        public async Task<IEnumerable<GetPackingResponse>> Handle
        (
            GetAllPackingRequest request,
            CancellationToken cancellationToken
        )
        {
            var packings = await _packingRepository.GetAllAsync();

            return packings.Select
            (p =>
                new GetPackingResponse
                (
                    p.Id,
                    p.Name,
                    p.Description,
                    p.Price,
                    p.Quantity,
                    p.UnitPrice,
                    p.UnitOfMeasure
                )
            );
        }
    }
}
