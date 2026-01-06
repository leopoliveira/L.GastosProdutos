using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Group;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class RecipeEntity : BaseEntity
    {
        public RecipeEntity() { }

        public RecipeEntity
        (
            string name,
            string? description,
            List<IngredientsValueObject> ingredients,
            List<PackingValueObject> packings,
            decimal? quantity,
            decimal? sellingValue
        )
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
            Packings = packings;
            Quantity = quantity ?? 0;
            SellingValue = sellingValue ?? 0;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<IngredientsValueObject> Ingredients { get; private set; } = null!;

        public List<PackingValueObject> Packings { get; private set; } = null!;

        public decimal? Quantity { get; set; }

        public decimal? SellingValue { get; set; }

        public string? GroupId { get; set; }

        public GroupEntity? Group { get; set; }

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

        public void RemoveAllIngredients()
        {
            decimal ingredientsTotalCost = Ingredients.Sum(i => i.GetCost());
            Ingredients.Clear();
            TotalCost -= ingredientsTotalCost;
        }

        public void AddPacking(PackingValueObject packing)
        {
            Packings.Add(packing);
            TotalCost += packing.GetCost();
        }

        public void RemovePacking(PackingValueObject packing)
        {
            Packings.Remove(packing);
            TotalCost -= packing.GetCost();
        }

        public void RemoveAllPackings()
        {
            decimal packingsTotalCost = Packings.Sum(i => i.GetCost());
            Packings.Clear();
            TotalCost -= packingsTotalCost;
        }

        public void RemoveAllIngredientsAndPackings()
        {
            Ingredients.Clear();
            Packings.Clear();
            TotalCost = 0;
        }
    }
}
