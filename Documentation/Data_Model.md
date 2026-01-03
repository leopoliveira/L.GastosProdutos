# Data Model

This document describes the core entities and database schema of the **L.GastosProdutos** application.

## Core Entities

The domain revolves around three main concepts: **Products** (Ingredients), **Packings** (Packaging), and **Recipes** (Final Product).

### 1. Product (Ingrediente)
Represents a raw material purchased in bulk.
*   **Table:** `Products`
*   **Key Attributes:**
    *   `Id`: Unique Identifier (Guid).
    *   `Name`: Name of the ingredient (e.g., "Flour").
    *   `Price`: The price paid for the bulk package.
    *   `Quantity`: The amount in the bulk package (e.g., 1000g).
    *   `UnitOfMeasure`: The unit (e.g., Grams, Liters, Units).
*   **Usage:** Used as a source for calculating the cost of ingredients in a recipe.

### 2. Packing (Embalagem)
Represents packaging materials.
*   **Table:** `Packings`
*   **Key Attributes:**
    *   `Id`: Unique Identifier.
    *   `Name`: Name (e.g., "Plastic Box").
    *   `Price`: Bulk price.
    *   `Quantity`: Bulk quantity.
*   **Usage:** Added to a recipe to account for packaging costs.

### 3. Recipe (Receita)
Represents the final culinary product produced by combining ingredients and packaging.
*   **Table:** `Recipes`
*   **Key Attributes:**
    *   `Id`: Unique Identifier.
    *   `Name`: Recipe name (e.g., "Chocolate Cake").
    *   `Description`: Optional description.
    *   `Quantity`: The yield of the recipe (e.g., 1 Cake, or 10 Slices).
    *   `SellingValue`: The price the recipe is sold for.
    *   `TotalCost`: **Calculated Field.** The sum of all ingredient costs + all packing costs.

**********

## Relationships & Value Objects

The relationship between Recipes and their components is modeled using **Value Objects** in the Domain and **Owned Types** in EF Core.

### Recipe -> Ingredients
*   **Relationship:** One-to-Many (A Recipe has many Ingredients).
*   **Implementation:**
    *   Stored in a separate table: `RecipeIngredients`.
    *   **Snapshot Concept:** When an ingredient is added to a recipe, the system stores a *snapshot* of its data (Name, Price at that moment) along with the quantity used.
    *   **Fields in `RecipeIngredients`:**
        *   `RecipeId` (FK)
        *   `ProductId` (Reference to original product)
        *   `ProductName` (Snapshot)
        *   `Quantity` (Amount used in recipe)
        *   `IngredientPrice` (Calculated cost for this specific quantity)

### Recipe -> Packings
*   **Relationship:** One-to-Many (A Recipe has many Packings).
*   **Implementation:**
    *   Stored in a separate table: `RecipePackings`.
    *   Similar snapshot concept as ingredients.
    *   **Fields in `RecipePackings`:**
        *   `RecipeId` (FK)
        *   `PackingId` (Reference)
        *   `PackingName` (Snapshot)
        *   `Quantity` (Amount used)
        *   `UnitPrice` (Cost per unit)

**********

## Database Schema (SQLite)

The database `gastos.db` is stored in the `App_Data` directory and contains the following tables:

### Main Tables
1.  **`Products`** - Stores raw ingredients/materials
    *   Primary Key: `Id` (Guid)
    *   Soft Delete: `IsDeleted` (Boolean)
    *   Global Query Filter: Automatically excludes deleted records

2.  **`Packings`** - Stores packaging materials
    *   Primary Key: `Id` (Guid)
    *   Soft Delete: `IsDeleted` (Boolean)
    *   Global Query Filter: Automatically excludes deleted records

3.  **`Recipes`** - Stores recipe information
    *   Primary Key: `Id` (Guid)
    *   Soft Delete: `IsDeleted` (Boolean)
    *   Global Query Filter: Automatically excludes deleted records
    *   Contains computed field `TotalCost` (updated via domain methods)

### Relationship Tables (Owned Types)
4.  **`RecipeIngredients`** - Links recipes to products (snapshot pattern)
    *   Composite Key: `(RecipeId, ProductId)`
    *   Foreign Key: `RecipeId` references `Recipes.Id`
    *   Configured as EF Core Owned Type via `OwnsMany`

5.  **`RecipePackings`** - Links recipes to packings (snapshot pattern)
    *   Composite Key: `(RecipeId, PackingId)`
    *   Foreign Key: `RecipeId` references `Recipes.Id`
    *   Configured as EF Core Owned Type via `OwnsMany`

### Soft Deletion Strategy
All main entity tables include an `IsDeleted` column to support soft deletion. The `AppDbContext` applies global query filters (`e.HasQueryFilter(p => !p.IsDeleted)`) ensuring that deleted records are automatically excluded from all queries unless explicitly included with `.IgnoreQueryFilters()`.
