# L.GastosProdutos

## Project Overview

**L.GastosProdutos** is a web application designed to help culinary businesses and individuals calculate the precise cost of their recipes. By managing the costs of raw ingredients (Products) and packaging materials (Packings), the system automatically calculates the total production cost of a Recipe.

This project is built using a modern stack with a **.NET 8** backend and a **Next.js 14** frontend, containerized with **Docker**.

## Technology Stack

### Backend
*   **Framework:** .NET 8.0
*   **Language:** C#
*   **Architecture:** Clean Architecture (Simplified)
*   **Database:** SQLite
*   **ORM:** Entity Framework Core
*   **API:** RESTful API with Swagger documentation

### Frontend
*   **Framework:** Next.js 14 (App Router)
*   **Language:** TypeScript
*   **UI Library:** Chakra UI
*   **State/Data Fetching:** Axios, React Hooks
*   **Styling:** Emotion / CSS Modules

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

## Resources
*   **API Documentation:** Available via Swagger when running the backend.
*   **Project Spec:** See `PROJECT_SPEC.md` in the root for detailed specifications.
