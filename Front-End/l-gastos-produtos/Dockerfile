# Stage 1: Build the application
FROM node:18-alpine AS builder

# Accept build-time API URLs and expose them as env for Next.js build
ARG NEXT_PUBLIC_PRODUCT_API_URL
ARG NEXT_PUBLIC_PACKING_API_URL
ARG NEXT_PUBLIC_RECIPE_API_URL
ENV NEXT_PUBLIC_PRODUCT_API_URL=$NEXT_PUBLIC_PRODUCT_API_URL \
    NEXT_PUBLIC_PACKING_API_URL=$NEXT_PUBLIC_PACKING_API_URL \
    NEXT_PUBLIC_RECIPE_API_URL=$NEXT_PUBLIC_RECIPE_API_URL

# Set the working directory
WORKDIR /app

# Copy the package.json and package-lock.json files
COPY package*.json ./

# Install dependencies
RUN npm install

# Copy the rest of the application code
COPY . .

# Ensure a public directory exists even if absent in repo
RUN mkdir -p public

# Build the application
RUN npm run build

# Remove development dependencies to reduce size
RUN npm prune --production

# Stage 2: Run the application
FROM node:18-alpine

# Set the working directory
WORKDIR /app

# Copy the built files and necessary dependencies from the builder stage
COPY --from=builder /app/.next ./.next
COPY --from=builder /app/node_modules ./node_modules
COPY --from=builder /app/package*.json ./
## Some projects don't include a /public folder; ensure it exists in builder so copy doesn't fail
RUN mkdir -p public
COPY --from=builder /app/public ./public

# Expose the port the app runs on
EXPOSE 3000

# Command to run the application
CMD ["npm", "start"]
