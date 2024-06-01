using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class IngredientsValueObject
    {
        public IngredientsValueObject(string productId, string productName, decimal quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            ProductUnitPrice = price;
        }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public decimal Quantity { get; set; }

        public decimal ProductUnitPrice { get; set; }

        public decimal GetCost() =>
            Quantity * ProductUnitPrice;
    }
}
