namespace L.GastosProdutos.Core.Application.Handlers.Recipe.ValueObjects
{
    public record PackingWriteDto
    (
        string PackingId,
        string PackingName,
        decimal Value
    );
}
