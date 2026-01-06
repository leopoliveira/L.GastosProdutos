# Backend Architecture

This document outlines the architectural decisions and structure of the **L.GastosProdutos** backend.

## Architectural Pattern

The backend follows a **Clean Architecture** approach, separating concerns into distinct layers. However, it uses a simplified implementation where Services interact directly with the `DbContext` rather than through a strict Repository pattern.

### Layers

1.  **API Layer (`L.GastosProdutos.API`)**
    *   **Role:** Entry point of the application.
    *   **Components:**
        *   **Controllers:** Handle HTTP requests, validate inputs (basic), and call Application Services.
        *   **IOC (`ConfigureBindings.cs`):** Manages Dependency Injection setup.
        *   **Program.cs:** Configures Middleware, Swagger, and Database connections.
    *   **Dependencies:** Depends on `L.GastosProdutos.Core`.

2.  **Core Layer (`L.GastosProdutos.Core`)**
    *   **Role:** Contains the business logic and domain model. In this project, the "Application" and "Domain" layers are combined within the Core project.
    *   **Components:**
        *   **Domain Entities:** `Recipe`, `Product`, `Packing`, `Group`. These are the heart of the logic.
        *   **Application Services:** `RecipeService`, `ProductService`, `GroupService`, etc. Orchestrate business operations.
        *   **Contracts (DTOs):** Define the input/output structures for the API (e.g., `AddRecipeRequest`, `GetRecipeResponse`).
        *   **Infrastructure Implementation:** `AppDbContext` (EF Core) is located here (technically an Infrastructure concern, but placed in Core/Infra for simplicity in this solution).

**********

## Data Flow

A typical request flows through the system as follows:

1.  **HTTP Request:** The client (Frontend) sends a request (e.g., `POST /api/v1/Recipe`).
2.  **Controller:** `RecipeController` receives the request and maps the body to a DTO (`AddRecipeRequest`).
3.  **Service:** The controller calls `IRecipeService.AddAsync()`.
4.  **Domain Logic:**
    *   The Service creates a new `RecipeEntity`.
    *   It iterates through the requested ingredients and packings.
    *   It calls domain methods like `recipe.AddIngredient(...)`.
    *   **Crucial:** The `RecipeEntity` automatically calculates the `TotalCost` as items are added.
5.  **Persistence:** The Service adds the entity to `AppDbContext` and calls `SaveChangesAsync()`.
6.  **Database:** Entity Framework Core translates this into SQL and executes it against the SQLite database.
7.  **Response:** The Service returns a Response DTO, which the Controller sends back to the client.

**********

## Domain Logic & Entities

The domain model is "Rich", meaning entities contain behavior, not just data.

### RecipeEntity (`L.GastosProdutos.Core/Domain/Entities/Recipe/RecipeEntity.cs`)
This is the most complex entity.
*   **Structure:** It owns collections of `Ingredients` and `Packings`.
*   **Value Objects:** Ingredients and Packings within a Recipe are treated as Value Objects (snapshots of the product/packing at the time of addition). They are stored in separate tables (`RecipeIngredients`, `RecipePackings`) managed by EF Core's `OwnsMany` configuration.
*   **Cost Calculation:** The `TotalCost` property is **not** set directly. It is updated incrementally via methods like `AddIngredient`, `RemoveIngredient`, etc. This ensures the cost is always consistent with the contents of the recipe.
*   **Group Association:** Recipes now include a nullable `GroupId` foreign key for associating recipes with groups.

### Soft Deletes
All major entities (`Product`, `Packing`, `Recipe`, `Group`) inherit from `BaseEntity` which includes an `IsDeleted` flag.
*   **Implementation:** `AppDbContext` configures a Global Query Filter (`e.HasQueryFilter(p => !p.IsDeleted)`) to automatically exclude deleted items from queries.

**********

## Database & Persistence

*   **Database Engine:** SQLite (`gastos.db`).
*   **ORM:** Entity Framework Core 10.0.1.
*   **Database Location:** Stored in `App_Data/gastos.db` directory (created automatically).
*   **Configuration:**
    *   Located in `L.GastosProdutos.Core/Infra/Sqlite/AppDbContext.cs`.
    *   Uses `OnModelCreating` to define relationships and table mapping.
    *   **Migrations:** Managed via EF Core tools. The `Program.cs` automatically applies migrations on startup if migrations exist (`db.Database.Migrate()`), or creates the database from the model if no migrations are found (`db.Database.EnsureCreated()`).

**********

## Dependency Injection

Dependencies are configured in `L.GastosProdutos.API/IOC/ConfigureBindings.cs`.
*   **Services:** Registered as Scoped:
    *   `IProductService` → `ProductService`
    *   `IRecipeService` → `RecipeService`
    *   `IPackingService` → `PackingService`
    *   `IGroupService` → `GroupService`
*   **Database:** SQLite registered with `AddDbContext<AppDbContext>`.
*   **CORS:** Configured with an "AllowAll" policy for development.
*   **Note:** MediatR was removed; the application now uses direct service calls instead of command/query handlers.

**********

## Recipe Groups Feature

**Purpose:** Enable grouping of recipes into categories for better organization and filtering.

**Key Components:**
*   **GroupEntity** (`Domain/Entities/Group/GroupEntity.cs`): Represents a recipe group with `Name` and `Description` properties. Inherits from `BaseEntity` for soft delete support.
*   **IGroupService & GroupService** (`Application/Services/IGroupService.cs` and `Implementations/GroupService.cs`): Provides CRUD operations with business logic validation to prevent deletion of groups that are referenced by recipes.
*   **GroupController** (`API/Controllers/V1/GroupController.cs`): Exposes REST endpoints for group management (`GET`, `POST`, `PUT`, `DELETE`).
*   **RecipeEntity Integration:** The `RecipeEntity` includes a nullable `GroupId` foreign key. Legacy data remains supported with null values, while new/updated recipes require group assignment in the UI.

**Soft Delete Integration:** Groups follow the same soft delete pattern as other entities, with global query filters automatically excluding deleted groups from queries.

**Deletion Validation:** Attempting to delete a group that is referenced by any recipe results in an `InvalidOperationException` with message "Não é possível deletar um grupo que está em uso por receitas."

**Database Relationship:** Configured with `DeleteBehavior.SetNull` so that if a group were somehow deleted directly at the database level, associated recipes would have their `GroupId` set to null (though the application prevents this through service-level validation).
