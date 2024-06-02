using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Packing
{
    public class PackingEntity : BaseEntity
    {
        public PackingEntity() { }

        public PackingEntity(string name, string? description, decimal price, decimal quantity)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public decimal UnitPrice => GetUnitPrice();

        private decimal GetUnitPrice() =>
            Price / Quantity;
    }
}
