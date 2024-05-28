using L.GastosProdutos.Core.Domain.Entities.Base;

namespace L.GastosProdutos.Core.Domain.Entities.Recipe
{
    public class RecipeEntity : BaseEntity
    {
        public RecipeEntity(string name, string? description, List<IngredientsEntity> ingredients)
        {
            Name = name;
            Description = description;
            Ingredients = ingredients;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public List<IngredientsEntity> Ingredients { get; } = null!;

        public void AddIngredient(IngredientsEntity ingredient) =>
            Ingredients.Add(ingredient);

        public void RemoveIngredient(IngredientsEntity ingredient) =>
            Ingredients.Remove(ingredient);
    }
}
