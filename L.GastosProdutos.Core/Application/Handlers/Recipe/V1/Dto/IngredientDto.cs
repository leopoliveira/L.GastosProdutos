namespace L.GastosProdutos.Core.Application.Handlers.Recipe.V1.Dto
{
    public record IngredientDto
    (
        string ProductId,
        string ProductName,
        decimal Quantity,
        decimal ProductUnitPrice
    );
}
