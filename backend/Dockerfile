# Étape 1 : build frontend
FROM node:20-alpine AS frontend-builder
WORKDIR /app
COPY frontend/ ./frontend
WORKDIR /app/frontend
RUN npm install && npm run build

# Étape 2 : build backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/*.csproj ./backend/
RUN dotnet restore ./backend/backend.csproj
COPY backend/ ./backend/
COPY --from=frontend-builder /app/frontend/dist ./backend/wwwroot
WORKDIR /src/backend
RUN dotnet publish -c Release -o /app/publish

# Étape 3 : final runtime image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]
