using L.GastosProdutos.Core.Domain.Entities.Base;
using L.GastosProdutos.Core.Domain.Entities.Recipe;

namespace L.GastosProdutos.Core.Domain.Entities.Group
{
    public class GroupEntity : BaseEntity
    {
        public GroupEntity() { }

        public GroupEntity(string name, string? description)
        {
            Name = name;
            Description = description;
        }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public ICollection<RecipeEntity> Recipes { get; set; } = new List<RecipeEntity>();
    }
}
