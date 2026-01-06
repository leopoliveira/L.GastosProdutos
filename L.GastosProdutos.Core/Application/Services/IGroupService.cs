using L.GastosProdutos.Core.Application.Contracts.Group;
using L.GastosProdutos.Core.Application.Contracts.Group.V1.AddGroup;

namespace L.GastosProdutos.Core.Application.Services
{
    public interface IGroupService
    {
        Task<IEnumerable<GroupResponse>> GetAllAsync(CancellationToken cancellationToken);
        Task<GroupResponse> GetByIdAsync(string id, CancellationToken cancellationToken);
        Task<AddGroupResponse> AddAsync(AddGroupRequest request, CancellationToken cancellationToken);
        Task UpdateAsync(string id, GroupWriteDto dto, CancellationToken cancellationToken);
        Task DeleteAsync(string id, CancellationToken cancellationToken);
    }
}
