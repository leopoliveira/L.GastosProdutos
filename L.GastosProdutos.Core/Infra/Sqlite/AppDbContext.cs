using L.GastosProdutos.Core.Domain.Entities.Group;
using L.GastosProdutos.Core.Domain.Entities.Packing;
using L.GastosProdutos.Core.Domain.Entities.Product;
using L.GastosProdutos.Core.Domain.Entities.Recipe;
using Microsoft.EntityFrameworkCore;

namespace L.GastosProdutos.Core.Infra.Sqlite
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<ProductEntity> Products { get; set; } = null!;
        public DbSet<PackingEntity> Packings { get; set; } = null!;
        public DbSet<GroupEntity> Groups { get; set; } = null!;
        public DbSet<RecipeEntity> Recipes { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductEntity>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasQueryFilter(p => !p.IsDeleted);
            });

            modelBuilder.Entity<PackingEntity>(e =>
            {
                e.HasKey(p => p.Id);
                e.HasQueryFilter(p => !p.IsDeleted);
            });

            modelBuilder.Entity<GroupEntity>(e =>
            {
                e.HasKey(g => g.Id);
                e.HasQueryFilter(g => !g.IsDeleted);
            });

            modelBuilder.Entity<RecipeEntity>(e =>
            {
                e.HasKey(r => r.Id);
                e.HasQueryFilter(r => !r.IsDeleted);

                e.HasOne(r => r.Group)
                    .WithMany(g => g.Recipes)
                    .HasForeignKey(r => r.GroupId)
                    .IsRequired(false)
                    .OnDelete(DeleteBehavior.SetNull);

                e.OwnsMany(r => r.Ingredients, a =>
                {
                    a.ToTable("RecipeIngredients");
                    a.WithOwner().HasForeignKey("RecipeId");
                    a.HasKey("RecipeId", "ProductId");
                    a.Property(p => p.ProductId);
                    a.Property(p => p.ProductName);
                    a.Property(p => p.Quantity);
                    a.Property(p => p.IngredientPrice);
                });

                e.OwnsMany(r => r.Packings, a =>
                {
                    a.ToTable("RecipePackings");
                    a.WithOwner().HasForeignKey("RecipeId");
                    a.HasKey("RecipeId", "PackingId");
                    a.Property(p => p.PackingId);
                    a.Property(p => p.PackingName);
                    a.Property(p => p.Quantity);
                    a.Property(p => p.UnitPrice);
                });
            });
        }
    }
}
