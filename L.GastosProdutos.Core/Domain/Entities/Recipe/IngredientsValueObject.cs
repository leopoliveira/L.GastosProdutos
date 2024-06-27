using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class IngredientsValueObject
    {
        public IngredientsValueObject(string productId, string productName, decimal quantity, decimal ingredientPrice)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            IngredientPrice = ingredientPrice;
        }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal IngredientPrice { get; set; }

        public decimal GetCost() =>
            Quantity * IngredientPrice;
    }
}
