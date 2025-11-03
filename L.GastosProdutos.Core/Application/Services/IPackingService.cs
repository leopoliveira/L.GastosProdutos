using L.GastosProdutos.Core.Application.Handlers.Packing.V1.AddPacking;
using L.GastosProdutos.Core.Application.Handlers.Packing.V1.GetPacking;
using L.GastosProdutos.Core.Application.Handlers.Packing.V1.UpdatePacking;

namespace L.GastosProdutos.Core.Application.Services
{
    public interface IPackingService
    {
        Task<IEnumerable<GetPackingResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<GetPackingResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<AddPackingResponse> AddAsync(AddPackingRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(string id, UpdatePackingDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}

