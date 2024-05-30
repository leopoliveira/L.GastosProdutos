using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Product
{
    public class ProductEntity : BaseEntity
    {
        public ProductEntity(string name, decimal price, decimal quantity)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; set; } = null!;

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice => GetUnitPrice();

        private decimal GetUnitPrice() =>
            Price / Quantity;
    }
}
