using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Domain.Entities.Packing;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class RecipeEntity : BaseEntity
    {
        public RecipeEntity() { }

        public RecipeEntity(string name, string? description, List<IngredientsValueObject> ingredients, List<PackingValueObject> packings)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Packings = packings;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<IngredientsValueObject> Ingredients { get; set; } = null!;

        public List<PackingValueObject> Packings { get; set;  } = null!;

        public decimal TotalCost { get; private set; }

        public void AddIngredient(IngredientsValueObject ingredient)
        {
            Ingredients.Add(ingredient);
            TotalCost += ingredient.GetCost();
        }

        public void RemoveIngredient(IngredientsValueObject ingredient)
        {
            Ingredients.Remove(ingredient);
            TotalCost -= ingredient.GetCost();
        }

        public void AddPacking(PackingValueObject packing)
        {
            Packings.Add(packing);
            TotalCost += packing.Cost;
        }

        public void RemovePacking(PackingValueObject packing)
        {
            Packings.Remove(packing);
            TotalCost -= packing.Cost;
        }
    }
}
