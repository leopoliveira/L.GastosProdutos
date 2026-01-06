namespace L.GastosProdutos.Core.Application.Contracts.Group
{
    public record GroupResponse(
        string Id,
        string Name,
        string? Description
    );
}
