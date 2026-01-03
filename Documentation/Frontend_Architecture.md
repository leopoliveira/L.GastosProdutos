# Frontend Architecture

This document explains the structure and design of the **L.GastosProdutos** frontend application.

## Technology Stack

*   **Framework:** Next.js 14 (App Router)
*   **Language:** TypeScript
*   **UI Library:** Tailwind CSS
*   **HTTP Client:** Axios
*   **Icons:** Lucide React
*   **Notifications:** Sonner

**********

## Project Structure (`Front-End/l-gastos-produtos/src`)

The project uses the Next.js **App Router** directory structure, where the file system defines the routes.

### `app/` Directory (Routes)
*   **`page.tsx`**: The home page (`/`).
*   **`layout.tsx`**: The root layout, wrapping the application with global styles and providers.
*   **`providers.tsx`**: Client-side context providers (Toaster for notifications).
*   **`products/`**: Contains the route for Product management.
    *   `page.tsx`: The UI for listing/adding products.
*   **`packings/`**: Contains the route for Packing management.
*   **`recipes/`**: Contains the route for Recipe management.

### `common/` Directory (Shared Logic)
This directory holds code that is reused across the application, keeping the UI components clean.
*   **`services/`**: Contains API client logic.
    *   `recipe/`: Service functions for Recipe API calls.
    *   `product/`: Service functions for Product API calls.
    *   `packing/`: Service functions for Packing API calls.
    *   `http/`: Likely contains the configured Axios instance (base URL, interceptors).
*   **`interfaces/`**: TypeScript definitions for API responses and domain objects.
*   **`utils/`**: Helper functions.

**********

## Data Fetching & State

*   **API Integration:** The frontend communicates with the backend via REST API.
*   **Services:** API calls are encapsulated in service modules (e.g., `recipeService.ts`). This abstracts the HTTP details from the React components.
    *   *Example:* A component calls `RecipeService.getAll()` instead of `axios.get('/api/v1/Recipe')`.
*   **State Management:**
    *   Uses standard React hooks (`useState`, `useEffect`) for local state.
    *   Likely uses `SWR` or `React Query` (implied by `swr` in `package.json`) for server state management, caching, and revalidation, although simple `useEffect` fetching might also be present.

**********

## UI Components

*   **Tailwind CSS:** The application uses Tailwind CSS utility classes for styling, providing a consistent and customizable design system.
*   **Custom Components:** UI components are built using standard HTML elements styled with Tailwind utility classes.
*   **Styling:** Styling is done via Tailwind's utility-first approach (e.g., `<div className="p-4 bg-gray-100">`), with `clsx` and `tailwind-merge` utilities for conditional styling.
*   **Icons:** Lucide React provides a comprehensive icon library (replacing Chakra icons).
*   **Notifications:** Sonner is used for toast notifications.
*   **Responsiveness:** Tailwind's responsive utilities (e.g., `sm:`, `md:`, `lg:`) handle different screen sizes.

**********

## Configuration

*   **Environment Variables:**
    *   `NEXT_PUBLIC_PRODUCT_API_URL`
    *   `NEXT_PUBLIC_PACKING_API_URL`
    *   `NEXT_PUBLIC_RECIPE_API_URL`
    *   These are defined in `docker-compose.yml` and passed to the container at runtime.
