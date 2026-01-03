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
4.  **Save:**
    *   The Backend (`PUT /api/v1/Recipe/{id}`) typically replaces the ingredient/packing lists.
    *   **Implementation Detail:** The current implementation clears the existing lists (`RemoveAllIngredientsAndPackings`) and re-adds the new ones from the request to ensure consistency.

**********

## 4. Soft Deletion

To preserve historical data or prevent accidental data loss, the system uses "Soft Deletes".
*   **Action:** When a user deletes a Product, Packing, or Recipe.
*   **Logic:** The record is **not** removed from the database. Instead, an `IsDeleted` flag is set to `true`.
*   **Visibility:** The application filters out all records where `IsDeleted == true`, so they disappear from the UI but remain in the database.
