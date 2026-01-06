# L.GastosProdutos

## Project Overview

**L.GastosProdutos** is a web application designed to help culinary businesses and individuals calculate the precise cost of their recipes. By managing the costs of raw ingredients (Products) and packaging materials (Packings), the system automatically calculates the total production cost of a Recipe.

This project is built using a modern stack with a **.NET 10** backend and a **Next.js 14** frontend, containerized with **Docker**.

## Technology Stack

### Backend
*   **Framework:** .NET 10.0
*   **Language:** C#
*   **Architecture:** Clean Architecture (Simplified)
*   **Database:** SQLite
*   **ORM:** Entity Framework Core 10.0.1
*   **API:** RESTful API with Swagger documentation

### Frontend
*   **Framework:** Next.js 14.2.3 (App Router)
*   **Language:** TypeScript
*   **UI Library:** Tailwind CSS 4.1.18
*   **HTTP Client:** Axios 1.7.2
*   **State Management:** React Hooks, SWR (for data fetching)
*   **Styling:** Tailwind CSS utility classes with clsx and tailwind-merge
*   **Icons:** Lucide React 0.562.0
*   **Notifications:** Sonner 2.0.7

### Infrastructure
*   **Containerization:** Docker & Docker Compose
*   **Orchestration:** `docker-compose.yml` defines `webapi` (Backend) and `front-end` services.

## Getting Started

### Prerequisites
*   Docker Desktop installed and running.

### Running the Application
1.  Navigate to the root directory of the project.
2.  Run the following command:
    ```bash
    docker-compose up --build
    ```
3.  Access the application:
    *   **Frontend:** [http://localhost:3000](http://localhost:3000)
    *   **Backend API (Swagger):** [http://localhost:8080/swagger](http://localhost:8080/swagger)

## Database Migrations

If you change domain entities (add/remove properties or entities), create and apply an EF Core migration before running the app in production.

Locally (recommended):

```bash
cd L.GastosProdutos.API
dotnet ef migrations add <MigrationName>
dotnet ef database update
```

When using Docker, migration files should be created on the host and included in the build context so the container can apply them on startup. This repository's `Program.cs` will call `Database.Migrate()` at startup to apply pending migrations automatically. To build and run with Docker Compose:

```bash
docker compose up --build
```

If you prefer running migrations explicitly inside the container (advanced), you can run:

```bash
docker compose run --rm webapi dotnet ef database update --startup-project L.GastosProdutos.API
```

Note: Ensure the `Microsoft.EntityFrameworkCore.Design` package is referenced in the startup project when creating migrations with `dotnet ef`.

## Project Structure

The workspace is organized into the following main directories:

*   **`L.GastosProdutos.API/`**: The entry point for the backend. Contains Controllers, DI configuration, and `Program.cs`.
*   **`L.GastosProdutos.Core/`**: Contains the business logic, Domain Entities, Interfaces, and Infrastructure implementation (EF Core Context).
*   **`Front-End/l-gastos-produtos/`**: The Next.js frontend application.
*   **`Documentation/`**: Detailed documentation files (you are here).

## Key Features
*   **Product Management:** Register raw ingredients with their bulk price and quantity.
*   **Packing Management:** Register packaging materials.
*   **Recipe Management:** Create recipes by combining Products and Packings. The system automatically calculates the cost based on the portion used.
*   **Group Management:** Create and manage recipe groups (Configurações → Grupos) to categorize recipes; groups support soft-delete and are used to filter recipes in the listing and require selection when creating/updating recipes.

## Resources
*   **API Documentation:** Available via Swagger when running the backend.
*   **Project Spec:** See `PROJECT_SPEC.md` in the root for detailed specifications.
