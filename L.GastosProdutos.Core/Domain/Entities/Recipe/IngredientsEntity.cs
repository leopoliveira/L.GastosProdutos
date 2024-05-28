﻿using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class IngredientsEntity : BaseEntity
    {
        public IngredientsEntity(string productId, string productName, float quantity, decimal price)
        {
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            Price = price;
        }

        public string ProductId { get; set; }

        public string ProductName { get; set; }

        public float Quantity { get; set; }

        public decimal Price { get; set; }
    }
}