namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto
{
    public record PackingDto
    (
        string PackingId,
        string PackingName,
        decimal Quantity,
        decimal PackingUnitPrice
    );
}
