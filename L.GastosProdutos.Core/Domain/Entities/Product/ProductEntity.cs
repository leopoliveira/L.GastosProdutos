using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Product
{
    internal class ProductEntity : BaseEntity
    {
        public ProductEntity(string name, decimal price, float quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public float Quantity { get; set; }

        public decimal UnitPrice => GetUnitPrice();

        private decimal GetUnitPrice() =>
            Price / (decimal)Quantity;
    }
}
