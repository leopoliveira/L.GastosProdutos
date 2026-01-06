namespace L.GastosProdutos.Core.Application.Contracts.Group.V1.AddGroup
{
    public record AddGroupRequest(
        string Name,
        string? Description
    );

    public record AddGroupResponse(
        string Id
    );
}
