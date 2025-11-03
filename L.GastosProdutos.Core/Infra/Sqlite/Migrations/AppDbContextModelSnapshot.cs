using System;
using L.GastosProdutos.Core.Infra.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace L.GastosProdutos.Core.Infra.Sqlite.Migrations
{
    [DbContext(typeof(AppDbContext))]
    public class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "8.0.10");

            modelBuilder.Entity("L.GastosProdutos.Core.Domain.Entities.Product.ProductEntity", b =>
            {
                b.Property<string>("Id").HasColumnType("TEXT");
                b.Property<DateTime>("CreatedAt").HasColumnType("TEXT");
                b.Property<bool>("IsDeleted").HasColumnType("INTEGER");
                b.Property<string>("Name").HasColumnType("TEXT");
                b.Property<decimal>("Price").HasColumnType("TEXT");
                b.Property<decimal>("Quantity").HasColumnType("TEXT");
                b.Property<int>("UnitOfMeasure").HasColumnType("INTEGER");
                b.Property<DateTime>("UpdatedAt").HasColumnType("TEXT");
                b.HasKey("Id");
                b.ToTable("Products");
            });

            modelBuilder.Entity("L.GastosProdutos.Core.Domain.Entities.Packing.PackingEntity", b =>
            {
                b.Property<string>("Id").HasColumnType("TEXT");
                b.Property<DateTime>("CreatedAt").HasColumnType("TEXT");
                b.Property<string>("Description").HasColumnType("TEXT");
                b.Property<bool>("IsDeleted").HasColumnType("INTEGER");
                b.Property<string>("Name").HasColumnType("TEXT");
                b.Property<decimal>("Price").HasColumnType("TEXT");
                b.Property<decimal>("Quantity").HasColumnType("TEXT");
                b.Property<int>("UnitOfMeasure").HasColumnType("INTEGER");
                b.Property<DateTime>("UpdatedAt").HasColumnType("TEXT");
                b.HasKey("Id");
                b.ToTable("Packings");
            });

            modelBuilder.Entity("L.GastosProdutos.Core.Domain.Entities.Recipe.RecipeEntity", b =>
            {
                b.Property<string>("Id").HasColumnType("TEXT");
                b.Property<DateTime>("CreatedAt").HasColumnType("TEXT");
                b.Property<string>("Description").HasColumnType("TEXT");
                b.Property<bool>("IsDeleted").HasColumnType("INTEGER");
                b.Property<string>("Name").HasColumnType("TEXT");
                b.Property<decimal?>("Quantity").HasColumnType("TEXT");
                b.Property<decimal?>("SellingValue").HasColumnType("TEXT");
                b.Property<decimal>("TotalCost").HasColumnType("TEXT");
                b.Property<DateTime>("UpdatedAt").HasColumnType("TEXT");
                b.HasKey("Id");
                b.ToTable("Recipes");

                b.OwnsMany("L.GastosProdutos.Core.Domain.Entities.Recipe.IngredientsValueObject", "Ingredients", b1 =>
                {
                    b1.Property<string>("RecipeId").HasColumnType("TEXT");
                    b1.Property<decimal>("IngredientPrice").HasColumnType("TEXT");
                    b1.Property<string>("ProductId").HasColumnType("TEXT");
                    b1.Property<string>("ProductName").HasColumnType("TEXT");
                    b1.Property<decimal>("Quantity").HasColumnType("TEXT");
                    b1.HasKey("RecipeId", "ProductId");
                    b1.HasIndex("RecipeId");
                    b1.ToTable("RecipeIngredients");
                });

                b.OwnsMany("L.GastosProdutos.Core.Domain.Entities.Packing.PackingValueObject", "Packings", b2 =>
                {
                    b2.Property<string>("RecipeId").HasColumnType("TEXT");
                    b2.Property<string>("PackingId").HasColumnType("TEXT");
                    b2.Property<string>("PackingName").HasColumnType("TEXT");
                    b2.Property<decimal>("Quantity").HasColumnType("TEXT");
                    b2.Property<decimal>("UnitPrice").HasColumnType("TEXT");
                    b2.HasKey("RecipeId", "PackingId");
                    b2.HasIndex("RecipeId");
                    b2.ToTable("RecipePackings");
                });
            });
        }
    }
}
