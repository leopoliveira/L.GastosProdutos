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

**********

## 5. Creating and Managing Groups

**Purpose:** Organize recipes into logical categories (e.g., "Bolos", "Cookies", "Brigadeiros").

### Group Creation Workflow
1.  **Initiation:** User navigates to `/configuration/groups` or clicks "Criar Grupo" button while creating/editing a recipe.
2.  **Form Input:** User enters:
    *   **Name** (required): The group identifier (e.g., "Bolos").
    *   **Description** (optional): Detailed description of the group.
3.  **Submission:** Frontend calls `GroupService.CreateGroup(name, description)`.
4.  **Backend Processing:**
    *   `GroupController` receives the request via `POST /api/v1/Group`.
    *   `GroupService.AddAsync()` validates that name is not empty.
    *   A new `GroupEntity` is created and persisted to the database.
5.  **Success Response:**
    *   Backend returns `201 Created` with the new group's ID and data.
    *   Frontend displays a success toast notification.
    *   If the creation was triggered from the recipe form, the form automatically refetches the groups list and pre-selects the newly created group.

### Group Filtering in Recipe Listing
1.  **Display:** The recipe listing page shows two filters side-by-side (20% width each):
    *   Name filter (flexible width).
    *   Group filter dropdown (20% width).
2.  **Default:** "Todos os Grupos" option shows all recipes regardless of group.
3.  **Filtering:** Selecting a specific group filters the recipe list to show only recipes assigned to that group.
4.  **Combination:** Name and group filters work with AND logic (both must match).

### Group Assignment During Recipe Creation/Editing
1.  **No Groups Scenario:** If no groups exist in the system, a teal "Criar Grupo" button is displayed instead of a select dropdown.
2.  **With Groups Scenario:** 
    *   A dropdown select allows choosing an existing group.
    *   A "Novo" button allows creating a new group inline.
3.  **Mandatory Selection:** Group assignment is required for all new and updated recipes (enforced via form validation).
4.  **Legacy Data:** Existing recipes in the database may have null GroupId values from before the feature was implemented, but such recipes cannot be edited or created without selecting a group.

### Group Deletion Protection
1.  **Deletion Attempt:** User tries to delete a group from `/configuration/groups`.
2.  **Validation Check:** Backend queries all recipes to see if any have `GroupId == [group_id]` and `IsDeleted == false`.
3.  **Prevention:** If recipes are found using the group, deletion is rejected with HTTP 400 and error message: "Não é possível deletar um grupo que está em uso por receitas."
4.  **Success:** If the group is not referenced by any recipe, the group is soft-deleted (`IsDeleted = true`).

```
