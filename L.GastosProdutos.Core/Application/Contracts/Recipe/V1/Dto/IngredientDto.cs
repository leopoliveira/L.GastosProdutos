namespace L.GastosProdutos.Core.Application.Contracts.Recipe.V1.Dto
{
    public record IngredientDto
    (
        string ProductId,
        string ProductName,
        decimal Quantity,
        decimal IngredientPrice
    );
}
