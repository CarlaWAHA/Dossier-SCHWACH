# Stage 1 - Build frontend
FROM node:22 AS frontend-build
WORKDIR /app
COPY frontend ./frontend
RUN cd frontend && npm install && npm run build

# Stage 2 - Build backend
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY backend/*.csproj ./backend/
RUN dotnet restore ./backend/backend.csproj
COPY backend/ ./backend/
COPY --from=frontend-build /app/frontend/dist ./backend/wwwroot/
RUN dotnet publish ./backend/backend.csproj -c Release -o /app/publish

# Stage 3 - Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "backend.dll"]
