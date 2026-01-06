# Project Specification: L.GastosProdutos

This document serves as a detailed specification and migration guide for the L.GastosProdutos application. It is designed to be used as a reference for AI coding assistants to understand the project context, domain logic, and architecture.

## 1. Project Overview
**L.GastosProdutos** is a web application designed to calculate the costs of culinary recipes or products. It manages the cost of raw materials (Products) and packaging (Packings) to determine the final cost of a Recipe.

## 2. Technology Stack

### Backend
- **Framework**: .NET 8.0
- **Language**: C#
- **Architecture**: Clean Architecture (Separation of concerns: API, Core/Domain, Infrastructure)
- **Database**: SQLite
- **ORM**: Entity Framework Core
- **API Style**: RESTful API

### Frontend
- **Framework**: Next.js 14+ (App Router)
- **Language**: TypeScript
- **UI Library**: Chakra UI
- **Styling**: Emotion / CSS Modules
- **HTTP Client**: Axios

### Infrastructure
- **Containerization**: Docker & Docker Compose

## 3. Domain Model

The core domain consists of three main entities: **Packing**, **Product**, and **Recipe**.

### 3.1. Entities

#### Packing (Embalagem)
Represents packaging materials used in the final product (e.g., boxes, bags, ribbons).
- **Attributes**:
  - `Id` (Guid/String): Unique identifier.
  - `Name` (String): Name of the packaging.
  - `Description` (String, Optional): Details about the packaging.
  - `Price` (Decimal): The price paid for a bulk quantity.
  - `Quantity` (Decimal): The quantity purchased for that price.
  - `UnitOfMeasure` (Enum): The unit of measure (e.g., 'un', 'package').
- **Business Logic**:
  - `UnitPrice`: Calculated as `Price / Quantity`. This is the cost of a single unit of packaging.

#### Product (Produto / Ingrediente)
Represents raw ingredients or materials used to create a recipe.
- **Attributes**:
  - `Id` (Guid/String): Unique identifier.
  - `Name` (String): Name of the ingredient.
  - `Price` (Decimal): The price paid for the product.
  - `Quantity` (Decimal): The quantity purchased for that price.
  - `UnitOfMeasure` (Enum): The unit of measure (e.g., 'kg', 'g', 'l', 'ml').
- **Business Logic**:
  - `UnitPrice`: Calculated as `Price / Quantity`. This is the cost per unit (e.g., cost per gram).

#### Recipe (Receita)
Represents the final product intended for sale, composed of ingredients and packaging.
- **Attributes**:
  - `Id` (Guid/String): Unique identifier.
  - `Name` (String): Name of the recipe.
  - `Description` (String, Optional): Preparation instructions or notes.
  - `Ingredients` (List<IngredientsValueObject>): List of ingredients used.
  - `Packings` (List<PackingValueObject>): List of packaging used.
  - `Quantity` (Decimal): The yield of the recipe (how many units it produces).
  - `SellingValue` (Decimal): The intended selling price.
  - `TotalCost` (Decimal): The calculated total cost of production.
- **Business Logic**:
  - `TotalCost` is the sum of the cost of all Ingredients and Packings.
  - **Cost Update Rule**: If the price of a `Product` or `Packing` changes, the `TotalCost` of all `Recipes` using them should ideally be updated (or recalculated on retrieval).

### 3.2. Value Objects

#### IngredientsValueObject
Represents the usage of a `Product` inside a `Recipe`.
- **Attributes**:
  - `ProductId`: Reference to the source Product.
  - `ProductName`: Name of the product (snapshot).
  - `Quantity`: Amount of the product used in the recipe.
  - `IngredientPrice`: The unit price of the product (snapshot or reference).
- **Logic**:
  - `Cost` = `Quantity` * `IngredientPrice`.

#### PackingValueObject
Represents the usage of a `Packing` inside a `Recipe`.
- **Attributes**:
  - `PackingId`: Reference to the source Packing.
  - `PackingName`: Name of the packing (snapshot).
  - `Quantity`: Amount of packing used.
  - `UnitPrice`: The unit price of the packing.
- **Logic**:
  - `Cost` = `Quantity` * `UnitPrice`.

### 3.3. Enums
**EnumUnitOfMeasure**:
`mg`, `g`, `kg`, `ml`, `l`, `un`, `box`, `package`, `bag`, `bottle`, `can`, `pot`, `tube`, `sachet`, `roll`, `sheet`, `pair`, `dozen`, `set`, `kit`, `other`.

## 4. API Specification

The API exposes endpoints for CRUD operations on the entities.

### Base URL: `/api/v1`

#### Packings
- `GET /packing`: List all packings.
- `GET /packing/{id}`: Get details of a specific packing.
- `POST /packing`: Create a new packing.
- `PUT /packing/{id}`: Update an existing packing.
- `DELETE /packing/{id}`: Delete a packing.

#### Products
- `GET /product`: List all products.
- `GET /product/{id}`: Get details of a specific product.
- `POST /product`: Create a new product.
- `PUT /product/{id}`: Update an existing product.
- `DELETE /product/{id}`: Delete a product.

#### Recipes
### Backend
- **Framework**: .NET 10.0
- **Language**: C#
- **Architecture**: Clean Architecture (Separation of concerns: API, Core/Domain, Infrastructure)
- **Database**: SQLite
- **ORM**: Entity Framework Core 10.0.1
- **API Style**: RESTful API
## 5. Frontend Structure

### Frontend
- **Framework**: Next.js 14.2.3 (App Router)
- **Language**: TypeScript
- **UI Library / Styling:** Tailwind CSS utility-first approach (components built with Tailwind classes)
- **HTTP Client**: Axios
- **`src/app/recipes/create/page.tsx`** (or modal): Form to create a recipe. This form is complex as it requires selecting multiple Products and Packings, specifying quantities for each, and calculating totals dynamically.
- **`src/common/services`**: Contains API calls (Axios) to the backend.
- **`src/common/interfaces`**: TypeScript interfaces matching the Domain Entities.


### 3.3. Group (Recipe Group / Grupo)
The system supports grouping recipes into categories for better organization and filtering.
* **Attributes:**
  * `Id` (Guid/String): Unique identifier.
  * `Name` (String): Group name (required).
  * `Description` (String, Optional): Additional notes.
  * `IsDeleted` (bool): Soft-delete flag inherited from `BaseEntity`.
* **Behavior:** Soft-deleted groups are hidden by global query filters. Deleting a group is prevented by service-level validation if any non-deleted recipes reference it.
* **Recipe relation:** Recipes have a nullable `GroupId` foreign key. Legacy records may have null `GroupId`, but the UI requires group selection when creating or editing recipes.
## 6. AI Prompts for Development

Use these prompts to guide an AI in generating or migrating code for this project.

### Prompt 1: Backend Entity Generation
> "Create a C# Domain Entity for a class named `[EntityName]` inheriting from `BaseEntity`. It should have the following properties: `[List Properties]`. Include a constructor to initialize these properties. Implement a method `GetUnitPrice()` that calculates price divided by quantity, handling division by zero."

### Prompt 2: Backend Service/Handler Generation
> "Create a Service class `[Entity]Service` implementing `I[Entity]Service`. It should use a Repository to perform CRUD operations. Ensure that the `Add` method maps the DTO to the Entity and saves it. The `Update` method should retrieve the existing entity, update its properties, and save changes."

### Prompt 3: Frontend List Page (Chakra UI)
> "Create a Next.js page using Chakra UI to display a list of `[EntityName]`. Use a `Table` component. Fetch data using a service function `get[EntityName]s`. Include 'Edit' and 'Delete' buttons for each row. Use a Modal for the 'Add New' form."

### Prompt 4: Recipe Calculation Logic (Frontend or Backend)
> "Implement a function to calculate the total cost of a recipe. It should accept a list of ingredients (each with quantity and unit price) and a list of packings (each with quantity and unit price). Iterate through both lists, summing `quantity * unitPrice` for each item to return the total cost."

### Prompt 5: Docker Setup
> "Create a `docker-compose.yml` file that sets up two services: `api` (building from the .NET API Dockerfile) and `web` (building from the Next.js Dockerfile). The `api` should expose port 8080 and `web` should expose port 3000. Ensure they share a network."

## 7. Migration/Refactoring Notes
- **Validation**: Ensure all inputs (Price, Quantity) are validated to be non-negative.
- **State Management**: When deleting a Product, ensure the UI updates immediately (Optimistic UI or re-fetch).
- **Cost Updates**: Consider implementing a domain event or a background job that updates Recipe costs when an underlying Product price changes.
