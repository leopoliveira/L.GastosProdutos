using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Domain.Entities.Packing
{
    public class PackingEntity : BaseEntity
    {
        public PackingEntity() { }

        public PackingEntity(string name, string? description, decimal price, decimal quantity, EnumUnitOfMeasure unitOfMeasure)
        {
            Name = name;
            Description = description;
            Price = price;
            Quantity = quantity;
            UnitOfMeasure = unitOfMeasure;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public decimal Price { get; set; }

        public decimal Quantity { get; set; }

        public EnumUnitOfMeasure UnitOfMeasure { get; set; }

        public decimal UnitPrice => GetUnitPrice();

        private decimal GetUnitPrice()
        {
            if (Quantity == 0)
            {
                return 0;
            }

            return Price / Quantity;
        }
    }
}
