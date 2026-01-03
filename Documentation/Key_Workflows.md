# Key Workflows

This document explains the primary business processes and logic flows within the application.

## 1. Cost Calculation Logic

The core value of the application is accurately calculating the cost of a recipe. This happens dynamically as items are added to a recipe.

### The Formula
$$ \text{Total Cost} = \sum (\text{Ingredient Cost}) + \sum (\text{Packing Cost}) $$

#### Ingredient Cost Calculation
When adding an ingredient to a recipe:
1.  **Input:** User selects a `Product` and specifies the `Quantity Used`.
2.  **Base Price:** The system retrieves the `Product`'s bulk `Price` and bulk `Quantity`.
3.  **Unit Price:** $$ \text{Unit Price} = \frac{\text{Product Price}}{\text{Product Quantity}} $$
4.  **Cost:** $$ \text{Ingredient Cost} = \text{Unit Price} \times \text{Quantity Used} $$

#### Packing Cost Calculation
When adding packaging to a recipe:
1.  **Input:** User selects a `Packing` and specifies the `Quantity Used` (usually count).
2.  **Unit Price:** $$ \text{Unit Price} = \frac{\text{Packing Price}}{\text{Packing Quantity}} $$
3.  **Cost:** $$ \text{Packing Cost} = \text{Unit Price} \times \text{Quantity Used} $$

**********

## 2. Creating a Recipe

1.  **Initiation:** User navigates to the "Create Recipe" page.
2.  **Basic Info:** User enters Name, Description, Yield (Quantity), and Selling Price.
3.  **Add Ingredients:**
    *   User searches/selects from existing Products.
    *   User enters the amount used (e.g., 200g).
    *   System calculates the cost for that portion and adds it to the running total.
4.  **Add Packaging:**
    *   User selects from existing Packings.
    *   User enters the count (e.g., 1 box).
    *   System adds the cost.
5.  **Save:**
    *   The Frontend sends the complete object (Recipe info + list of ingredients + list of packings) to the Backend (`POST /api/v1/Recipe`).
    *   The Backend creates the `RecipeEntity`, validates the data, and saves it to the database.

**********

## 3. Updating a Recipe

1.  **Load:** User opens an existing recipe.
2.  **Modify:** User can change the name, selling price, or modify the list of ingredients/packings.
3.  **Recalculate:**
    *   If an ingredient is removed, its cost is subtracted from the Total Cost.
    *   If an ingredient's quantity is changed, the cost is recalculated.
    *   The domain entity maintains cost consistency through methods like `AddIngredient()`, `RemoveIngredient()`, `AddPacking()`, `RemovePacking()`.
4.  **Save:**
    *   The Backend (`PUT /api/v1/Recipe/{id}`) receives the updated recipe data.
    *   **Implementation Detail:** The current implementation uses a "clear and rebuild" strategy - it calls `RemoveAllIngredientsAndPackings()` to clear existing lists and reset `TotalCost` to 0, then re-adds all ingredients and packings from the request.
    *   This ensures consistency but means the relationship records are recreated on each update.
    *   The updated entity is saved to the database via Entity Framework Core.

**********

## 4. Soft Deletion

To preserve historical data and prevent accidental data loss, the system uses "Soft Deletes" for all main entities.

*   **Action:** When a user deletes a Product, Packing, or Recipe via the UI.
*   **Backend Logic:** 
    *   The record is **not** physically removed from the database.
    *   Instead, an `IsDeleted` flag on the entity is set to `true`.
    *   The updated entity is saved via `SaveChangesAsync()`.
*   **Visibility:** 
    *   Entity Framework Core's Global Query Filters automatically exclude all records where `IsDeleted == true` from normal queries.
    *   They disappear from the UI but remain in the database for data integrity and potential recovery.
*   **Accessing Deleted Records:** 
    *   If needed (e.g., for admin tools or audit features), deleted records can be accessed by adding `.IgnoreQueryFilters()` to LINQ queries.
