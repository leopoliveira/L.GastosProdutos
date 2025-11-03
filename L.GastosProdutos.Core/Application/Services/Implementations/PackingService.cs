using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.AddPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.UpdatePacking;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Interfaces;
using L.GastosProdutos.Core.Application.Services.Mappers;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class PackingService : IPackingService
    {
        private readonly IPackingRepository _repository;

        public PackingService(IPackingRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<GetPackingResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var packings = await _repository.GetAllAsync(cancellationToken);
            return packings.Select(p => p.ToResponse());
        }

        public async Task<GetPackingResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var packing = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Packing not found.");
            return packing.ToResponse();
        }

        public async Task<AddPackingResponse> AddAsync(AddPackingRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var packing = new PackingEntity(
                request.Name,
                request.Description,
                request.Price,
                request.Quantity,
                (EnumUnitOfMeasure)request.UnitOfMeasure
            );

            await _repository.CreateAsync(packing, cancellationToken);

            return new AddPackingResponse(packing.Id);
        }

        public async Task UpdateAsync(string id, UpdatePackingDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existing = await _repository.GetByIdAsync(id, cancellationToken) ?? throw new NotFoundException("Packing not found.");

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;
            existing.UnitOfMeasure = (EnumUnitOfMeasure)dto.UnitOfMeasure;

            await _repository.UpdateAsync(id, existing, cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            await _repository.DeleteAsync(id, cancellationToken);
        }
    }
}
