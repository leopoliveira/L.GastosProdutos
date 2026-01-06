# Frontend Architecture

This document explains the structure and design of the **L.GastosProdutos** frontend application.

## Technology Stack

*   **Framework:** Next.js 14.2.3 (App Router)
*   **Language:** TypeScript
*   **UI Library:** Tailwind CSS 4.1.18
*   **HTTP Client:** Axios 1.7.2
*   **Data Fetching:** SWR 2.2.5 (for caching and revalidation)
*   **Icons:** Lucide React 0.562.0
*   **Notifications:** Sonner 2.0.7
*   **Styling Utilities:** clsx and tailwind-merge for conditional class composition

**********

## Project Structure (`Front-End/l-gastos-produtos/src`)

The project uses the Next.js **App Router** directory structure, where the file system defines the routes.

### `app/` Directory (Routes)
*   **`page.tsx`**: The home page (`/`).
*   **`layout.tsx`**: The root layout, wrapping the application with global styles and providers. The layout includes a fixed vertical sidebar navigation.
*   **`providers.tsx`**: Client-side context providers (Toaster for notifications).
*   **`components/`**: Reusable UI components organized by feature and shared utilities.
    *   **`header/`**: Contains the Sidebar component (vertical navigation menu, fixed on the left side).
    *   **`product/`**: Product-specific components (data grids, forms, modals).
    *   **`packing/`**: Packing-specific components.
    *   **`recipe/`**: Recipe-specific components.
    *   **`group/`**: Group-specific components (data grids, forms, modals).
    *   **`shared/`**: Shared components used across the application (e.g., DataGrid, modals).
*   **`products/`**: Contains the route for Product management.
    *   `page.tsx`: The UI for listing/adding products.
*   **`packings/`**: Contains the route for Packing management.
*   **`recipes/`**: Contains the route for Recipe management.
*   **`configuration/`**: Contains the route for Application Configuration.
    *   `page.tsx`: Overview page for configuration options.
    *   **`groups/`**: Nested route for Group management.
        *   `page.tsx`: UI for listing/adding groups.

### `common/` Directory (Shared Logic)
This directory holds code that is reused across the application, keeping the UI components clean.
*   **`services/`**: Contains API client logic.
    *   `recipe/`: Service functions for Recipe API calls.
    *   `product/`: Service functions for Product API calls.
    *   `packing/`: Service functions for Packing API calls.
    *   `group/`: Service functions for Group API calls.
    *   `http/`: Contains the configured Axios instance (base URL, interceptors).
    *   `utils/`: Service-level utility functions.
*   **`interfaces/`**: TypeScript definitions for API responses and domain objects.
*   **`enums/`**: Enumeration types (e.g., UnitOfMeasure).
*   **`utils/`**: General helper functions.

**********

## Data Fetching & State

*   **API Integration:** The frontend communicates with the backend via REST API.
*   **Services:** API calls are encapsulated in service modules (e.g., `recipeService.ts`). This abstracts the HTTP details from the React components.
    *   *Example:* A component calls `RecipeService.getAll()` instead of `axios.get('/api/v1/Recipe')`.
*   **State Management:**
    *   Uses standard React hooks (`useState`, `useEffect`) for local state.
    *   Uses `SWR` for server state management, caching, and automatic revalidation of remote data.
    *   Components can trigger revalidation after mutations to keep data in sync.

**********

## UI Components

### Layout & Navigation
*   **Sidebar Navigation:** A fixed vertical sidebar on the left side of the application (256px width) providing navigation to all main sections (Home, Materia Prima, Embalagens, Receitas). The active route is highlighted.
*   **Layout:** The main content area occupies the full remaining screen width with a left margin to accommodate the fixed sidebar.

### Styling & Components
*   **Tailwind CSS:** The application uses Tailwind CSS utility classes for styling, providing a consistent and customizable design system.
*   **Custom Components:** UI components are built using standard HTML elements styled with Tailwind utility classes.
*   **Styling:** Styling is done via Tailwind's utility-first approach (e.g., `<div className="p-4 bg-gray-100">`), with `clsx` and `tailwind-merge` utilities for conditional styling.
*   **Icons:** Lucide React provides a comprehensive icon library (replacing Chakra icons).
*   **Notifications:** Sonner is used for toast notifications.
*   **Responsiveness:** Tailwind's responsive utilities (e.g., `sm:`, `md:`, `lg:`) handle different screen sizes.
*   **Interactive Elements:** Buttons use hover effects (e.g., darker background on hover) and cursor pointer for better UX.

**********

## Configuration

*   **Environment Variables:**
    *   `NEXT_PUBLIC_PRODUCT_API_URL`
    *   `NEXT_PUBLIC_PACKING_API_URL`
    *   `NEXT_PUBLIC_RECIPE_API_URL`
    *   These are defined in `docker-compose.yml` and passed to the container at runtime.

**********

## Recipe Groups Feature

**Purpose:** Enable grouping of recipes into categories for better organization and filtering.

**Key Frontend Components:**
*   **GroupService** (`common/services/group/GroupService.ts`): TypeScript service encapsulating all group-related API calls (`GetAllGroups`, `GetGroupById`, `CreateGroup`, `UpdateGroup`, `DeleteGroup`).
*   **Group Interfaces** (`common/interfaces/group/`): TypeScript definitions for group data structures (`ICreateGroup`, `IReadGroup`, `IUpdateGroup`).
*   **Configuration Pages:**
    *   **Configuration Overview** (`app/configuration/page.tsx`): Main configuration hub with navigation to sub-pages.
    *   **Groups Management** (`app/configuration/groups/page.tsx`): Full CRUD interface for managing groups.
*   **Group Components** (`app/components/group/`):
    *   **GroupDataGrid:** Displays groups in a filterable, sortable table with Edit/Delete actions.
    *   **GroupFormModal:** Create/Edit form modal with name (required) and description (optional) fields and validation.
    *   **GroupDeleteModal:** Delete confirmation modal with API error handling.

**Recipe Integration:**
*   **RecipeGrid Filter:** The recipe listing page includes a dropdown select (20% width) to filter recipes by group, positioned alongside the name filter.
*   **RecipeForm Group Selection:**
    *   If no groups exist: Display a teal "Criar Grupo" button to create a group inline.
    *   If groups exist: Display a dropdown select with all groups plus a "Novo" button for creating new groups.
    *   Group selection is mandatory for new/updated recipes (required field validation).
    *   After creating a group via modal, the form automatically fetches the updated group list and pre-selects the newly created group.

**Navigation:** A "Configurações" link has been added to the sidebar navigation, providing easy access to the configuration section and its subpages.
