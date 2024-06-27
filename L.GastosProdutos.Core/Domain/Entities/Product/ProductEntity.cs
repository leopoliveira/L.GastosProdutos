using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Domain.Enums;

namespace L.GastosProdutos.Core.Domain.Entities.Product
{
    public class ProductEntity : BaseEntity
    {
        public ProductEntity() { }

        public ProductEntity(string name, decimal price, decimal quantity, EnumUnitOfMeasure unitOfMeasure)
        {
            Name = name;
            Price = price;
            Quantity = quantity;
            UnitOfMeasure = unitOfMeasure;
        }

        public string Name { get; set; } = null!;

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
