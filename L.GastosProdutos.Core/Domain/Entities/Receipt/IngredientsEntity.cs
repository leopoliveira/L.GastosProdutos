using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Receipt
{
    internal class IngredientsEntity : BaseEntity
    {
        public IngredientsEntity(string productId, float quantity, decimal price)
        {
            ProductId = productId;
            Quantity = quantity;
            Price = price;
        }

        public string ProductId { get; set; }

        public float Quantity { get; set; }

        public decimal Price { get; set; }
    }
}
