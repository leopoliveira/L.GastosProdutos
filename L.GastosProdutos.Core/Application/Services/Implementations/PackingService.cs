using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.AddPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.GetPacking;
using L.GastosProdutos.Core.Application.Contracts.Packing.V1.UpdatePacking;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Enums;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Application.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class PackingService : IPackingService
    {
        private readonly AppDbContext _db;

        public PackingService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GetPackingResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var packings = await _db.Packings.AsNoTracking().ToListAsync(cancellationToken);
            return packings.Select(p => p.ToResponse());
        }

        public async Task<GetPackingResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var packing = await _db.Packings.AsNoTracking().FirstOrDefaultAsync(p => p.Id == id, cancellationToken)
                ?? throw new NotFoundException("Packing not found.");
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

            _db.Packings.Add(packing);
            await _db.SaveChangesAsync(cancellationToken);

            return new AddPackingResponse(packing.Id);
        }

        public async Task UpdateAsync(string id, UpdatePackingDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var existing = await _db.Packings.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Packing not found.");

            existing.Name = dto.Name;
            existing.Description = dto.Description;
            existing.Price = dto.Price;
            existing.Quantity = dto.Quantity;
            existing.UnitOfMeasure = (EnumUnitOfMeasure)dto.UnitOfMeasure;

            existing.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var entity = await _db.Packings.FirstOrDefaultAsync(p => p.Id == id && !p.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Packing not found. Nothing will be deleted.");

            entity.IsDeleted = true;
            entity.UpdatedAt = DateTime.UtcNow;
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
