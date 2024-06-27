namespace L.GastosProdutos.Core.Domain.Entities.Packing
{
    public class PackingValueObject
    (
        string PackingId,
        string PackingName,
        decimal Quantity,
        decimal UnitPrice
    )
    {
        public string PackingId { get; } = PackingId;
        public string PackingName { get; } = PackingName;
        public decimal Quantity { get; } = Quantity;
        public decimal UnitPrice { get; } = UnitPrice;

        public decimal GetCost() =>
            Quantity * UnitPrice;
    }
}
