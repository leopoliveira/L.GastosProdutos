namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto
{
    public record PackingWriteDto
    (
        string PackingId,
        string PackingName,
        decimal Value
    );
}
