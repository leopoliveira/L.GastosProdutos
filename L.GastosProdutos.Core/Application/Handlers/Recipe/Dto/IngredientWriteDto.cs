namespace L.GastosProdutos.Core.Application.Handlers.Recipe.ValueObjects
{
    public record IngredientWriteDto
    (
        string ProductId,
        string ProductName,
        decimal Quantity,
        decimal ProductUnitPrice
    );
}
