namespace L.GastosProdutos.Core.Domain.Entities.Packing
{
    public record PackingValueObject
    (
        string PackingId,
        string PackingName,
        decimal Cost
    );
}
