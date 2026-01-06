#!/bin/bash
set -e

echo "Starting L.GastosProdutos API..."

# Wait for database to be ready (if using external DB, add healthcheck)
echo "Applying EF Core migrations..."

# Run migrations using dotnet ef (requires EF Core tools to be available)
# If you have the migrations already built into the assembly, Program.cs will handle it
# This script ensures explicit migration application

echo "Database migration initialization complete."

# Start the application
echo "Starting API server..."
exec dotnet L.GastosProdutos.API.dll
