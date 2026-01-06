using L.GastosProdutos.Core.Application.Exceptions;
using L.GastosProdutos.Core.Application.Contracts.Group;
using L.GastosProdutos.Core.Application.Contracts.Group.V1.AddGroup;
using L.GastosProdutos.Core.Domain.Entities.Group;
using L.GastosProdutos.Core.Infra.Sqlite;
using L.GastosProdutos.Core.Application.Services.Mappers;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Application.Services.Implementations
{
    public class GroupService : IGroupService
    {
        private readonly AppDbContext _db;

        public GroupService(AppDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<GroupResponse>> GetAllAsync(CancellationToken cancellationToken)
        {
            var groups = await _db.Groups.AsNoTracking().ToListAsync(cancellationToken);
            return groups.Select(g => g.ToResponse());
        }

        public async Task<GroupResponse> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();
            var group = await _db.Groups.AsNoTracking().FirstOrDefaultAsync(g => g.Id == id, cancellationToken)
                ?? throw new NotFoundException("Grupo não encontrado.");
            return group.ToResponse();
        }

        public async Task<AddGroupResponse> AddAsync(AddGroupRequest request, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(request.Name))
                throw new InvalidOperationException("Nome do grupo é obrigatório.");

            var group = new GroupEntity(request.Name, request.Description);

            _db.Groups.Add(group);
            await _db.SaveChangesAsync(cancellationToken);

            return new AddGroupResponse(group.Id);
        }

        public async Task UpdateAsync(string id, GroupWriteDto dto, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            if (string.IsNullOrWhiteSpace(dto.Name))
                throw new InvalidOperationException("Nome do grupo é obrigatório.");

            var group = await _db.Groups.FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Grupo não encontrado.");

            group.Name = dto.Name;
            group.Description = dto.Description;

            _db.Groups.Update(group);
            await _db.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteAsync(string id, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var group = await _db.Groups.FirstOrDefaultAsync(g => g.Id == id && !g.IsDeleted, cancellationToken)
                ?? throw new NotFoundException("Grupo não encontrado.");

            // Check if group is used by any recipes
            var recipesUsingGroup = await _db.Recipes
                .AsNoTracking()
                .AnyAsync(r => r.GroupId == id && !r.IsDeleted, cancellationToken);

            if (recipesUsingGroup)
                throw new InvalidOperationException("Não é possível deletar um grupo que está em uso por receitas.");

            group.IsDeleted = true;

            _db.Groups.Update(group);
            await _db.SaveChangesAsync(cancellationToken);
        }
    }
}
