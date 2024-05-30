using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Domain.Entities.Packing;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class RecipeEntity : BaseEntity
    {
        public RecipeEntity(string name, string? description, List<IngredientsEntity> ingredients, List<PackingValue> packings)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Packings = packings;
            TotalCost = ingredients.Sum(i => i.GetCost());
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<IngredientsEntity> Ingredients { get; } = null!;

        public decimal TotalCost { get; private set; }

        public List<PackingValue> Packings { get; set; } = null!;

        public void AddIngredient(IngredientsEntity ingredient)
        {
            Ingredients.Add(ingredient);
            TotalCost += ingredient.GetCost();
        }

        public void RemoveIngredient(IngredientsEntity ingredient)
        {
            Ingredients.Remove(ingredient);
            TotalCost -= ingredient.GetCost();
        }
    }
}
