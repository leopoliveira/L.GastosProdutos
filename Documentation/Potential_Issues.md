# Potential Issues & Quirks

This document highlights known issues, technical debt, and specific behaviors of the codebase that developers should be aware of.

## Known Issues (from ToDo.txt)

### 1. Recipe Cost Updates (Backend)
*   **Issue:** Currently, if the price of a raw `Product` or `Packing` changes, the `TotalCost` of existing `Recipes` that use them is **not** automatically updated.
*   **Impact:** Recipe costs become stale and inaccurate over time.
*   **Proposed Solution:** Implement a domain event or service method that triggers a recalculation of all dependent recipes whenever a Product/Packing price is updated.

### 2. Frontend Deletion State
*   **Issue:** When an item is deleted in the frontend, the list does not automatically re-render to reflect the removal.
*   **Cause:** Likely due to the local state not being updated after the API call completes, or the data fetcher (SWR/React Query) not being revalidated.
*   **Workaround:** Users currently have to manually refresh the page.

**********

## Architectural Quirks

### 1. Service-Context Coupling
*   **Observation:** Application Services (e.g., `RecipeService`) inject `AppDbContext` directly.
*   **Standard Practice:** Clean Architecture typically recommends using a Repository Interface (e.g., `IRecipeRepository`) to decouple the application logic from the persistence mechanism.
*   **Implication:** Unit testing services is harder because you have to mock the DbContext, which is complex.

### 2. Value Object Snapshots
*   **Observation:** Ingredients in a recipe are stored as snapshots (Value Objects).
*   **Implication:** This is actually a good design for *historical* accuracy (knowing what a recipe cost *at the time it was made*). However, combined with the "Recipe Cost Updates" issue above, it creates ambiguity: Should a recipe reflect the *current* market cost or the *historical* cost? This business rule needs clarification.

### 3. "Remove and Re-add" Update Logic
*   **Observation:** The `RecipeService.UpdateAsync` method clears all ingredients and packings and re-adds them from the request.
    ```csharp
    recipe.RemoveAllIngredientsAndPackings();
    // ... add new ones
    ```
*   **Implication:** This is a simple way to handle updates but can be inefficient for large recipes. It also changes the internal IDs of the relationship records (if they have any), which might affect audit logs if implemented later.

**********

## Codebase Specifics

### 1. Global Query Filters
*   **Feature:** The `AppDbContext` applies a global filter `e.HasQueryFilter(p => !p.IsDeleted)` to all entities.
*   **Gotcha:** If you ever need to access deleted records (e.g., for an "Undo" feature or Admin audit), you must explicitly use `.IgnoreQueryFilters()` in your EF Core query.

### 2. Docker Networking
*   **Feature:** The frontend communicates with the backend using the service name `webapi` internally (if server-side rendering) or `localhost` (if client-side fetching).
*   **Configuration:** Pay attention to `NEXT_PUBLIC_...` environment variables in `docker-compose.yml`. They are set to `http://localhost:8080` which assumes the browser (client) can access the API on that port.
